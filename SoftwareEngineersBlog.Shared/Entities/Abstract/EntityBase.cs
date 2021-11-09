using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineersBlog.Shared.Entities.Abstract
{
    /// <summary>
    /// Base değerlerin diğer sınıflarda değişikliğe uğrayabileceği için
    /// ABSTRACT bir sınıf olmasını istiyoruz.
    /// Bu şekilde Override edebileceğiz.
    /// Bundan dolayı buradaki değerleri VIRTUAL olarak tanımlayacağız.
    /// </summary>
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual DateTime ModifiedDate { get; set; } = DateTime.Now;
        public virtual bool IsDeleted { get; set; } = false;
        public virtual bool IsActive { get; set; } = true;
        public virtual string CreatedByName { get; set; } = "Admin";
        public virtual string ModifiedByName { get; set; } = "Admin";
        public virtual string Note { get; set; }
    }
}