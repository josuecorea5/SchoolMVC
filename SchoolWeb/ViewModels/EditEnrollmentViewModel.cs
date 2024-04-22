using Microsoft.AspNetCore.Mvc.Rendering;
using School.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.ViewModels
{
	public class EditEnrollmentViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public int GradeId { get; set; }

		[Required]
		public EnrollmentStatus EnrollmentStatus { get; set; }

		public ICollection<SelectListItem>? StudentLists { get; set; }
		public ICollection<SelectListItem>? GradeLists { get; set; }
	}
}
