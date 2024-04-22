using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using School.Domain.Entities;
using School.Infrastructure.Services;

namespace School.Infrastructure.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        public AuditableEntitySaveChangesInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext is null) return;

            foreach(var entity in dbContext.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if(entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedBy = _currentUserService.GetUserId();
                    entity.Entity.Created = DateTime.UtcNow;
                }

                if(entity.State == EntityState.Modified)
                {
                    entity.Entity.LastModifiedBy = _currentUserService.GetUserId();
                    entity.Entity.LastModified = DateTime.UtcNow;
                }
            }
        }
    }
}
