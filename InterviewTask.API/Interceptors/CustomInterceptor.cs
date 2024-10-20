using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace InterviewTask.API.Interceptors
{
    public class CustomInterceptor : DbCommandInterceptor
    {
        private readonly CanceletionState _canceletionState;

        public CustomInterceptor(CanceletionState canceletionState)
        {
            _canceletionState = canceletionState;
        }
        public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
                if (context is null)
                  return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);

           var entities = context.ChangeTracker.Entries<BaseEntity>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Property(x => x.CreatedOn).CurrentValue = DateTime.UtcNow;
                    entity.Property(x => x.CreatedBy).CurrentValue = "";
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Property(x => x.LastModifiedOn).CurrentValue = DateTime.UtcNow;
                    entity.Property(x => x.LastModifiedBy).CurrentValue = "";
                }
            }
            return base.NonQueryExecutingAsync(command, eventData, result, _canceletionState.Token);
        }


        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            return base.ReaderExecutingAsync(command, eventData, result, _canceletionState.Token);
        }

    }

}
