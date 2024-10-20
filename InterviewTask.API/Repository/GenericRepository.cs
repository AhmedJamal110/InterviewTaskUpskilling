using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Threading;

namespace InterviewTask.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IQueryable<T> GetAllAsync()
        {
            return _context.Set<T>().Where(x => x.IsDeleted == false);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.ID == id && x.IsDeleted == false);
        }

        public async Task HardDelte(int id)
        {
            await _context.Set<T>().Where(x => x.ID == id).ExecuteDeleteAsync();
        }

        public async Task<bool> IsEntityExsit(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task SoftDelte(int id)
        {
             await _context.Set<T>()
            .Where(x => x.ID == id)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true));
        }

        public void Update(T entity)
        {
             _context.Set<T>().Update(entity)
          
         ;
        }

        public void UpdateInclude(T entity, params string[] updatedProp)
        {
            var local = _context.Set<T>().Local.FirstOrDefault(x => x.ID == entity.ID);

            EntityEntry entityEntry;
            if (local is null)
            {
                entityEntry = _context.Entry(entity);
            }
            else
            {
                entityEntry =  _context.ChangeTracker.Entries<BaseEntity>().FirstOrDefault(x => x.Entity.ID == entity.ID);
            }

            foreach (var prop in entityEntry.Properties)
            {
                if (updatedProp.Contains(prop.Metadata.Name))
                {
                    prop.CurrentValue = entity.GetType().GetProperty(prop.Metadata.Name).GetValue(entity);
                    prop.IsModified = true;
                }
            }
        
        }
    }
}
