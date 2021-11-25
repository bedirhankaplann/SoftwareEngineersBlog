using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineersBlog.Entities.Concrete;
using SoftwareEngineersBlog.Entities.Dtos;
using SoftwareEngineersBlog.Shared.Utilities.Results.Abstract;

namespace SoftwareEngineersBlog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> Get(int categoryId);
        Task<IDataResult<CategoryListDto>> GetAll();
        Task<IDataResult<CategoryListDto>> GetAllNonDeleted();
        Task<IDataResult<CategoryListDto>> GetAllNonDeletedAndActive();
        Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createByName);
        Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        Task<IResult> Delete(int categoryId, string modifiedByName);

        Task<IResult> HardDelete(int categoryId);
    }
}
