using System.ComponentModel.DataAnnotations;

namespace School.Domain.CustomValidators
{
	public class DateOfBirthValidator : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			DateTime dateOfBirth = (DateTime)value;
			var limitDate = DateTime.Now.Year;
			return limitDate - dateOfBirth.Year >= 6;
		}
	}
}
