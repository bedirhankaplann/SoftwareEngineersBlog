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
    public class EFRoleRepository:EFEntityRepositoryBase<Role>,IRoleRepository
    {
        public EFRoleRepository(DbContext context) : base(context)
        {
        }
    }
}
