using InterviewTask.API.Domain.Entities;
using System.Linq.Expressions;

namespace InterviewTask.API.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        //Task<T?> GetByIdWithSpecfic(int id , Expression<Func<T, bool>> expression , CancellationToken cancellationToken = default);
        Task<bool> IsEntityExsit(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void UpdateInclude(T entity , params string[] updatedProp);

        Task HardDelte(int id);
        Task SoftDelte(int id);
    }
}
