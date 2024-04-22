using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Infrastructure.Interceptors;
using SchoolWeb.Seeds;
using System.Reflection;

namespace School.Infrastructure.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		private readonly AuditableEntitySaveChangesInterceptor _saveChangesInterceptor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditableEntitySaveChangesInterceptor saveChangesInterceptor) : base(options)
        {
            _saveChangesInterceptor = saveChangesInterceptor;
        }

        public DbSet<Grade> Grades { get; set; }
		public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<StudyPlan> StudyPlans { get; set; }
        public DbSet<StudyPlanSubject> StudyPlanSubjects {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_saveChangesInterceptor);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Seed();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
	}
}
