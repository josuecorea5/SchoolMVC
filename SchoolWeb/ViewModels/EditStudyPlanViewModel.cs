using Microsoft.AspNetCore.Mvc.Rendering;
using School.Domain.Entities;
using SchoolWeb.ViewModels.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.ViewModels
{
	public class EditStudyPlanViewModel
	{
		public int Id { get; set; }

		[Required]
		public int GradeId { get; set; }
		public SelectListItem? Grade { get; set; }
		public string? SubjectIds { get; set; }
		public IEnumerable<Subject>? ListSubject { get; set; }
		public IEnumerable<StudyPlanSubject>? ListStudyPlanSubject { get;set; }
	}
}
