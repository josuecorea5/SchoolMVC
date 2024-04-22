using SchoolWeb.ViewModels.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.ViewModels
{
	public class CreateTeacherViewModel
	{
		[Required]
		[MinLength(3)]
		public string Name { get; set; } = string.Empty;

		[Required]
		[MinLength(5)]
		public string Title { get; set; } = string.Empty;

		[Required]
		[MinLength(3)]
		public string LastName { get; set; } = string.Empty;
		
		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		
		[Required]
		[ExtensionImageValidator(ErrorMessage = "File extension not allowed")]
		public IFormFile? Image { get; set; }
	}
}
