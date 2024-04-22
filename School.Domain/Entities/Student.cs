using System.ComponentModel.DataAnnotations;

namespace School.Domain.Entities
{
	public class Student : BaseAuditableEntity
	{
		[Required]
		[MinLength(3)]
		public string Name {  get; set; } = string.Empty;

		[Required]
		[MinLength(3)]
		public string LastName { get; set; } = string.Empty;

		[Required]
		public int Age { get; set; }

		public string Code { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime DateOfBirth { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		[MaxLength(9)]
		public string Phone { get; set; } = string.Empty;
		public string Image { get; set; } = string.Empty;
		public IEnumerable<Enrollment>? Enrollments { get; set; }
	}
}
