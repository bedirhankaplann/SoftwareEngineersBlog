using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineersBlog.Entities.Concrete;
using SoftwareEngineersBlog.Shared.Data.Abstract;

namespace SoftwareEngineersBlog.Data.Abstract
{
    public interface ICommentRepository:IEntityRepository<Comment>
    {
    }
}
