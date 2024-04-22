using School.Domain.CustomValidators;
using SchoolWeb.ViewModels.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.ViewModels
{
	public class CreateStudentViewModel
	{
		[MinLength(3)]
		public string Name { get; set; } = string.Empty;

		[MinLength(3)]
		public string LastName { get; set; } = string.Empty;

		[DateOfBirthValidator(ErrorMessage = "Date of birth should not be greater than today")]
		public DateTime DateOfBirth { get; set; }

		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "Invalid number format")]
		public string Phone { get; set; } = string.Empty;

		[Required]
		[ExtensionImageValidator(ErrorMessage = "File extension not allowed")]
		public IFormFile? Image { get; set; }
	}
}
