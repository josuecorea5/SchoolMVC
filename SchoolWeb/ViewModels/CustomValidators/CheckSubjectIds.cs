using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.ViewModels.CustomValidators
{
	public class CheckSubjectIds : ValidationAttribute
	{
		public override bool IsValid(object? value)
		{
			string idsList = (string)value;
			
			if(idsList is null || string.IsNullOrEmpty(idsList))
			{
				return false;			
			}

			return true;
		}
	}
}
