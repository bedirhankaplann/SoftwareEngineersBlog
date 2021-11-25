using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SoftwareEngineersBlog.Shared.Entities.Abstract;

namespace SoftwareEngineersBlog.Shared.Data.Abstract
{
    /// <summary>
    /// ASYNC kodlama yapacağız.
    /// DAL Classlarında kullanacağımız metotları tanımladım.
    /// Soyut haldeler somut halleri yazılacak.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityRepository<T> where T:class, IEntity, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T,object>>[]includeProperties);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includeProperties);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
