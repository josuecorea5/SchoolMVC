using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;

namespace SchoolWeb.Seeds
{
	public static class GradeSeed
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Grade>().HasData(
				new Grade { Id = 1, Name = "1° Grade", Description = "7-year-old children"},
				new Grade { Id = 2, Name = "2° Grade", Description = "8-year-old children"},
				new Grade { Id = 3, Name = "3° Grade", Description = "9-year-old children" }
			);
		}
	}
}
