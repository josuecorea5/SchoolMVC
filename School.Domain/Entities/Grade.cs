using School.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Domain.Entities
{
	public class Grade : BaseAuditableEntity
	{
		[Required]
		[MaxLength(30)]
		public string Name { get; set; } = string.Empty;
		[Required]
		[MaxLength(100)]
		public string Description { get; set; } = string.Empty;

		public ICollection<Enrollment>? Enrollments { get; set; }
		public StudyPlan StudyPlan { get; set; } = null!;

		//[ForeignKey(nameof(Name))]
	}
}
