using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineersBlog.Entities.Dtos
{
    public class CategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        [MaxLength(70, ErrorMessage = "{0} Should Not Be More Than {1} Characters.")]
        [MinLength(3, ErrorMessage = "{0} Should Not Be Less Than {1} Characters.")]
        public string Name { get; set; }

        [DisplayName("Category Description")]
        [MaxLength(500, ErrorMessage = "{0} Should Not Be More Than {1} Characters.")]
        [MinLength(3, ErrorMessage = "{0} Should Not Be Less Than {1} Characters.")]
        public string Description { get; set; }

        [DisplayName("Category Notes Area")]
        [MaxLength(500, ErrorMessage = "{0} Should Not Be More Than {1} Characters.")]
        [MinLength(3, ErrorMessage = "{0} Should Not Be Less Than {1} Characters.")]
        public string Note { get; set; }

        [DisplayName("Is The Category Active?")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        public bool IsActive { get; set; }

        [DisplayName("The Category Deleted?")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        public bool IsDeleted { get; set; }  
    }
}
