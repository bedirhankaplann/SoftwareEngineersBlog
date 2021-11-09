using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineersBlog.Shared.Utilities.Results.ComplexTypes;

namespace SoftwareEngineersBlog.Shared.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; } //ResultStatus.Success gibi
        public string Message { get;  }
        public Exception Exception { get; }
    }
}
