using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineersBlog.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IArticleRepository Articles { get; } //_unitOfWork.Articles
        ICategoryRepository Categories { get; } // _unifOfWork.Categories.AddAsync()
        ICommentRepository Comments { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        Task<int> SaveAsync();

        //_unifOfWork.Categories.AddAsync(categories)
        //_unifOfWork.Users.AddAsync(users)
        //_unifOfWork.SaveAsync();
    }
}
