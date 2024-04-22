using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Domain.Entities;
using SchoolWeb.ViewModels.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.ViewModels
{
	public class CreateStudyPlanViewModel
	{
		[Required]
		public int GradeId { get; set; }

		[CheckSubjectIds(ErrorMessage = "You need to add at least one subject to the study plan")]
		public string? SubjectIds { get; set; }

		public IEnumerable<Subject>? ListSubject {  get; set; }
		public ICollection<SelectListItem>? Grades { get; set; }
	}
}
