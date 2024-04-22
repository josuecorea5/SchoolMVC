using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.ViewModels
{
	public class CreateStudyPlanSubjectViewModel
	{
		[Required]
		public int StudyPlanId { get; set; }

		[Required]
		public int SubjectId { get; set; }
	}
}
