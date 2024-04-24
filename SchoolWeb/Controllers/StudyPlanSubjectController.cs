using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Domain.Entities;
using School.Infrastructure.Repositories;
using School.Utility;
using SchoolWeb.ViewModels;

namespace SchoolWeb.Controllers
{
    [Authorize(Roles = StaticDetails.ROLE_ADMIN)]
    public class StudyPlanSubjectController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public StudyPlanSubjectController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreateStudyPlanSubjectViewModel createStudyPlanSubjectViewModel)
		{
			try
			{

				var existStudyPlan = _unitOfWork.StudyPlanRepository.Get(sp => sp.Id == createStudyPlanSubjectViewModel.StudyPlanId);

				if(existStudyPlan is null)
				{
					return Json(new { success = false, message = "Study plan does not exist" });
				}

				var newPlanSubject = new StudyPlanSubject
				{
					StudyPlanId = createStudyPlanSubjectViewModel.StudyPlanId,
					SubjectId = createStudyPlanSubjectViewModel.SubjectId,
				};
				_unitOfWork.StudyPlanSubjectRepository.Add(newPlanSubject);
				_unitOfWork.Save();

				return Json(new { success = true, message = "Subject added successfully", id = newPlanSubject.Id });

			}catch (Exception ex)
			{
				return Json(new { success = false, message = "Something failed while adding subject to study plan" });
			}
		}
	}
}
