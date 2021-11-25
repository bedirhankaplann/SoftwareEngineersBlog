using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<CategoryDto>(ResultStatus.Error, "Category Not Found.",new CategoryDto
                {
                    Category = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Category Not Found."
                } );
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);

            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<CategoryListDto>(ResultStatus.Error, "Categories Not Found.", new CategoryListDto
                {
                    Categories = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Categories Not Found."
                });
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAllNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<CategoryListDto>(ResultStatus.Error, "Categories Not Found.", null);
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAllNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<CategoryListDto>(ResultStatus.Error, "Categories Not Found.", null);
            }
        }

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createByName;
            category.ModifiedByName = createByName;
            var addedCategory = await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();

            //.ContinueWith(t => _unitOfWork.SaveAsync());
            //ContinueWith ile işlerken bitirdiği gibi zincirleme şekilde yapıyor. Çok hızlı işlem yapıyor.
            //Ancak yönetimi biraz daha zorlaştırıyor.
;
            return new DataResult<CategoryDto>(ResultStatus.Success,
                $"The Category Named {categoryAddDto.Name} Has Been Successfully Added.", new CategoryDto
                {
                    Category = addedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = $"The Category Named {categoryAddDto.Name} Has Been Successfully Added."
                });
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {

            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifiedByName = modifiedByName;
            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.Success,
                    $"The Category Named {categoryUpdateDto.Name} Has Been Successfully Added.", new CategoryDto
                    {
                        Category = updatedCategory,
                        ResultStatus = ResultStatus.Success,
                        Message = $"The Category Named {categoryUpdateDto.Name} Has Been Successfully Added."
                    });
        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();

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
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();

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
