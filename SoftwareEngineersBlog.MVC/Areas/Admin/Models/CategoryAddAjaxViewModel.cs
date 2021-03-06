using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftwareEngineersBlog.Entities.Dtos;

namespace SoftwareEngineersBlog.MVC.Areas.Admin.Models
{
    public class CategoryAddAjaxViewModel
    {
        public CategoryAddDto CategoryAddDto { get; set; }
        public string CategoryAddPartial { get; set; }
        public CategoryDto CategoryDto { get; set; }
    }
}
