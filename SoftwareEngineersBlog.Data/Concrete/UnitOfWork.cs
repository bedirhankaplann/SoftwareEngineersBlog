using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineersBlog.Data.Abstract;
using SoftwareEngineersBlog.Data.Concrete.EntityFramework.Contexts;
using SoftwareEngineersBlog.Data.Concrete.EntityFramework.Repositories;

namespace SoftwareEngineersBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SoftwareEngineersBlogContext _context;
        private EFArticleRepository _articleRepository;
        private EFCategoryRepository _categoryRepository;
        private EFCommentRepository _commentRepository;
        private EFRoleRepository _roleRepository;
        private EFUserRepository _userRepository;

        public UnitOfWork(SoftwareEngineersBlogContext context)
        {
            _context = context;
        }

        public IArticleRepository Articles => _articleRepository ?? new EFArticleRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ?? new EFCategoryRepository(_context);

        public ICommentRepository Comments => _commentRepository ?? new EFCommentRepository(_context);

        public IRoleRepository Roles => _roleRepository ?? new EFRoleRepository(_context);

        public IUserRepository Users => _userRepository ?? new EFUserRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
