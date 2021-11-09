using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineersBlog.Data.Abstract;
using SoftwareEngineersBlog.Entities.Concrete;
using SoftwareEngineersBlog.Shared.Data.Concrete.EntityFramework;

namespace SoftwareEngineersBlog.Data.Concrete.EntityFramework.Repositories
{
    public class EFArticleRepository:EFEntityRepositoryBase<Article>,IArticleRepository
    {
        public EFArticleRepository(DbContext context) : base(context)
        {
            
        }
    }
}
