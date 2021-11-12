using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineersBlog.Entities.Concrete;
using SoftwareEngineersBlog.Shared.Entities.Abstract;
using SoftwareEngineersBlog.Shared.Utilities.Results.ComplexTypes;

namespace SoftwareEngineersBlog.Entities.Dtos
{
    public class ArticleListDto : DtoGetBase
    {
        public IList<Article> Articles { get; set; }
        
    }
}
