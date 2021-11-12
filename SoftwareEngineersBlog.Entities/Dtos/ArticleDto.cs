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
    public class ArticleDto : DtoGetBase
    {
        public Article Article { get; set; }
        //public override ResultStatus ResultStatus { get; set; } = ResultStatus.Success;
    }
}
