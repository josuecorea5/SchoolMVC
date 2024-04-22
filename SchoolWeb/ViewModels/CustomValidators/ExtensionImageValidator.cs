using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.ViewModels.CustomValidators
{
	public class ExtensionImageValidator : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			var allowedExtensions = new[] { ".jpg", ".png", ".gif", ".jpeg" };
			var file = value as IFormFile;
			var extension = Path.GetExtension(file.FileName);
			return allowedExtensions.Contains(extension);
		}
	}
}
