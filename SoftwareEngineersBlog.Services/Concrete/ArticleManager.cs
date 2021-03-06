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
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId, 
                a => a.User, a => a.Category);
            if (article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<ArticleDto>(ResultStatus.Error, "Article Not Found.", null);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Articles Not Found.", null);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAllNonDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Articles Not Found.", null);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Articles Not Found.", null);
            }
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
               var article =
                await _unitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
               
               if (article.Count > -1)
               {
                   return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                   {
                       Articles = article,
                       ResultStatus = ResultStatus.Success
                   });
               }
               else
               {
                   return new DataResult<ArticleListDto>(ResultStatus.Error, "Articles Not Found.", null);
               }

            }
            else
            {
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Category Not Found.", null);
            }
        }

        public async Task<IResult> Add(ArticleAddDto articleAddDto, string createByName)
        {
            var article =  _mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createByName;
            article.ModifiedByName = createByName;
            article.UserId = 1;
            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"The Article Named {articleAddDto.Title} Added With Successful. ");
        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var article = _mapper.Map<Article>(articleUpdateDto);
            article.ModifiedByName = modifiedByName;
            await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"The Article Named {articleUpdateDto.Title} Updated With Successful. ");
        }

        public async Task<IResult> Delete(int articleId, string modifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"The Article Named {article.Title} Deleted With Successful. ");
            }
            else
            {
                return new DataResult<ArticleListDto>(ResultStatus.Error, "The Article Could Not Found.", null);
            }
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"The Article Named {article.Title} Deleted With Successful From The Database. ");
            }
            else
            {
                return new DataResult<ArticleListDto>(ResultStatus.Error, "The Article Could Not Found.", null);
            }
        }
    }
}
