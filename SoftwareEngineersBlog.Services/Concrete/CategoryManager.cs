using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineersBlog.Data.Abstract;
using SoftwareEngineersBlog.Entities.Concrete;
using SoftwareEngineersBlog.Entities.Dtos;
using SoftwareEngineersBlog.Services.Abstract;
using SoftwareEngineersBlog.Shared.Utilities.Results.Abstract;
using SoftwareEngineersBlog.Shared.Utilities.Results.ComplexTypes;
using SoftwareEngineersBlog.Shared.Utilities.Results.Concrete;

namespace SoftwareEngineersBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<Category>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<Category>(ResultStatus.Success, category);
            }
            else
            {
                return new DataResult<Category>(ResultStatus.Error, "Category Not Found.",null );
            }
        }

        public async Task<IDataResult<IList<Category>>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);

            if (categories.Count > -1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Success, categories);
            }
            else
            {
                return new DataResult<IList<Category>>(ResultStatus.Error, "Categories Not Found.", null);
            }
        }

        public async Task<IDataResult<IList<Category>>> GetAllNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Success, categories);
            }
            else
            {
                return new DataResult<IList<Category>>(ResultStatus.Error, "Categories Not Found.", null);
            }
        }

        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createByName)
        {
            await _unitOfWork.Categories.AddAsync(new Category()
            {
                Name = categoryAddDto.Name,
                Description = categoryAddDto.Description,
                Note = categoryAddDto.Note,
                IsActive = categoryAddDto.IsActive,
                CreatedByName = createByName,
                CreatedDate = DateTime.Now,
                ModifiedByName = createByName,
                ModifiedDate = DateTime.Now,
                IsDeleted = false
            }).ContinueWith(t => _unitOfWork.SaveAsync());
            //ContinueWith ile işlerken bitirdiği gibi zincirleme şekilde yapıyor. Çok hızlı işlem yapıyor.
            //Ancak yönetimi biraz daha zorlaştırıyor.

            //await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success,
                $"The Category Named {categoryAddDto.Name} Has Been Successfully Added.", null);
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            if (category != null)
            {
                category.Name = categoryUpdateDto.Name;
                category.Description = categoryUpdateDto.Description;
                category.Note = categoryUpdateDto.Note;
                category.IsActive = categoryUpdateDto.IsActive;
                category.CreatedDate = DateTime.Now;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                category.IsDeleted = categoryUpdateDto.IsDeleted;
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success,
                    $"The Category Named {categoryUpdateDto.Name} Has Been Successfully Added.", null);
            }
            else
            {
                return new Result(ResultStatus.Error, "Categories Not Found.", null);
            }
        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success,
                    $"The Category Named {category.Name} Has Been Successfully Deleted.", null);
            }
            else
            {
                return new Result(ResultStatus.Error, $"Category #{categoryId} Could Not Be Found To Delete.", null);
            }
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success,
                    $"The Category Named {category.Name} Has Been Successfully Deleted From Database.", null);
            }
            else
            {
                return new Result(ResultStatus.Error, $"Category #{categoryId} Could Not Be Found To Delete.", null);
            }
        }
    }
}
