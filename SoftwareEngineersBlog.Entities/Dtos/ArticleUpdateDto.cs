using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineersBlog.Entities.Concrete;

namespace SoftwareEngineersBlog.Entities.Dtos
{
    public class ArticleUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        [MaxLength(100, ErrorMessage = "{0} Should Not Be More Than {1} Characters.")]
        [MinLength(5, ErrorMessage = "{0} Should Not Be Less Than {1} Characters.")]
        public string Title { get; set; }

        [DisplayName("Content")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        [MinLength(20, ErrorMessage = "{0} Should Not Be Less Than {1} Characters.")]
        public string Content { get; set; }

        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        [MaxLength(250, ErrorMessage = "{0} Should Not Be More Than {1} Characters.")]
        [MinLength(5, ErrorMessage = "{0} Should Not Be Less Than {1} Characters.")]
        public string Thumbnail { get; set; }

        [DisplayName("Date")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Seo Author")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        [MaxLength(50, ErrorMessage = "{0} Should Not Be More Than {1} Characters.")]
        [MinLength(0, ErrorMessage = "{0} Should Not Be Less Than {1} Characters.")]
        public string SeoAuthor { get; set; }

        [DisplayName("Seo Description")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        [MaxLength(150, ErrorMessage = "{0} Should Not Be More Than {1} Characters.")]
        [MinLength(0, ErrorMessage = "{0} Should Not Be Less Than {1} Characters.")]
        public string SeoDescription { get; set; }

        [DisplayName("Seo Tags")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        [MaxLength(70, ErrorMessage = "{0} Should Not Be More Than {1} Characters.")]
        [MinLength(5, ErrorMessage = "{0} Should Not Be Less Than {1} Characters.")]
        public string SeoTags { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("Is The Article Active?")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        public bool IsActive { get; set; }

        [DisplayName("Will The Article Deleted?")]
        [Required(ErrorMessage = "{0} Should Not Be Blank.")]
        public bool IsDeleted { get; set; }
    }
}
