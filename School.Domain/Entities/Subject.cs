using System.ComponentModel.DataAnnotations.Schema;

namespace School.Domain.Entities
{
	public class Subject : BaseAuditableEntity
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty;

		[ForeignKey("Teacher")]
		public int TeacherId { get; set; }

		public Teacher Teacher { get; set; } = null!;
	}
}
