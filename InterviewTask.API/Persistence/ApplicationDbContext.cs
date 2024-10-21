using InterviewTask.API.Domain.Entities;
using InterviewTask.API.Interceptors;
using InterviewTask.API.Shared;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InterviewTask.API.Persistence
{
    public class ApplicationDbContext:DbContext

    {
        private readonly CanceletionState _canceletionState;
        private readonly UserState _userState;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option 
            , CanceletionState canceletionState , UserState userState) : base(option)
        {
            _canceletionState = canceletionState;
            _userState = userState;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFk = modelBuilder.Model
                                                    .GetEntityTypes()
                                                    .SelectMany(x => x.GetForeignKeys())
                                                    .Where(x => x.DeleteBehavior == DeleteBehavior.Cascade && !x.IsOwnership);

            foreach (var FK in cascadeFk)
                FK.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.AddInterceptors(new CustomInterceptor(_canceletionState , _userState ));
            }

            base.OnConfiguring(optionsBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleFeautre> RoleFeautres { get; set; }

    }
}
