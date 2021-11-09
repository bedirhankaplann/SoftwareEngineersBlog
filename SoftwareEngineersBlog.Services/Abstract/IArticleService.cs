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
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> Get(int articleId);
        Task<IDataResult<ArticleListDto>> GetAll();
        Task<IDataResult<ArticleListDto>> GetAllNonDeleted();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId);
        Task<IResult> Add(ArticleAddDto articleAddDto, string createByName);
        Task<IResult> Update(ArticleUpdateDto articleAddDtoUpdateDto, string modifiedByName);
        Task<IResult> Delete(int articleId, string modifiedByName);

        Task<IResult> HardDelete(int articleId);
    }
}
