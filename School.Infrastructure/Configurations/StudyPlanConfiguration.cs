using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain.Entities;

namespace School.Infrastructure.Configurations
{
	public class StudyPlanConfiguration : IEntityTypeConfiguration<StudyPlan>
	{
		public void Configure(EntityTypeBuilder<StudyPlan> builder)
		{
			builder.HasMany(sp => sp.Subjects)
				.WithOne(sps => sps.StudyPlan)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
