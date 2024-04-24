using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Domain.Entities;
using School.Infrastructure.Repositories;
using School.Utility;
using SchoolWeb.ViewModels;

namespace SchoolWeb.Controllers
{
    [Authorize(Roles = StaticDetails.ROLE_ADMIN)]
    public class StudyPlanController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public StudyPlanController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			List<Subject> subjects = _unitOfWork.SubjectRepository.GetAll().ToList();
			List<Grade> grades = _unitOfWork.GradeRepository.GetAll().ToList();
			CreateStudyPlanViewModel viewModel = new CreateStudyPlanViewModel
			{
				ListSubject = subjects,
				Grades = grades.Select(grade => new SelectListItem { Text = grade.Name, Value = grade.Id.ToString()}).ToList(),
			};

			return View(viewModel);
		}

		public IActionResult Edit(int id)
		{
			List<Subject> subjects = _unitOfWork.SubjectRepository.GetAll().ToList();
			List<Grade> grades = _unitOfWork.GradeRepository.GetAll().ToList();
			StudyPlan studyPlan = _unitOfWork.StudyPlanRepository.Get(studyPlan => studyPlan.Id == id, "Subjects,Grade");
			var subjectSelectedIds = studyPlan.Subjects.Select(x => x.SubjectId.ToString()).ToList();

			EditStudyPlanViewModel editStudyPlanViewModel = new EditStudyPlanViewModel
			{
				ListSubject = subjects.Where(subject => !subjectSelectedIds.Contains(subject.Id.ToString())),
				Grade = new SelectListItem { Text = studyPlan.Grade.Name, Value = studyPlan.Grade.Id.ToString() },
				ListStudyPlanSubject = studyPlan.Subjects,
				GradeId = studyPlan.GradeId,
				Id = studyPlan.Id,
				SubjectIds = string.Join(",", subjectSelectedIds)
			};

			return View(editStudyPlanViewModel);
		}

		[HttpPost]
		public IActionResult Create(CreateStudyPlanViewModel createStudyPlanViewModel)
		{
			if(!ModelState.IsValid)
			{
				List<Subject> subjects = _unitOfWork.SubjectRepository.GetAll().ToList();
				List<Grade> grades = _unitOfWork.GradeRepository.GetAll().ToList();
				CreateStudyPlanViewModel viewModel = new CreateStudyPlanViewModel
				{
					ListSubject = subjects,
					Grades = grades.Select(grade => new SelectListItem { Text = grade.Name, Value = grade.Id.ToString() }).ToList(),
				};

				return View(viewModel);
			}

			var existStudyPlan = _unitOfWork.StudyPlanRepository.Get(studyPlan => studyPlan.GradeId == createStudyPlanViewModel.GradeId);

			if(existStudyPlan is not null)
			{
				TempData["error"] = "There already is a study plan for this grade";
				return RedirectToAction("Index");
			}

			var subjectIds = createStudyPlanViewModel.SubjectIds.Split(",").Select(id => Convert.ToInt32(id));

			foreach (var id in subjectIds)
			{
				var existSubject = _unitOfWork.SubjectRepository.Get(subject => subject.Id == id);

				if(existSubject is null)
				{
					TempData["error"] = "Subject was not found";
					return RedirectToAction("Index");
				}
			}

			using var transaction = _unitOfWork.BeginTransaction();

			try
			{
				var studyPlan = new StudyPlan
				{
					GradeId = createStudyPlanViewModel.GradeId
				};

				_unitOfWork.StudyPlanRepository.Add(studyPlan);
				_unitOfWork.Save();

				foreach (var subject in subjectIds)
				{
					var studyPlanSubject = new StudyPlanSubject
					{
						StudyPlanId = studyPlan.Id,
						SubjectId = subject
					};

					_unitOfWork.StudyPlanSubjectRepository.Add(studyPlanSubject);
					_unitOfWork.Save();
				}

				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				TempData["error"] = "Something failed while creating the study plan";
				return RedirectToAction("Index");
			}
			TempData["success"] = "studyplan created successfully";
			return RedirectToAction("Index");
		}

		#region
		[HttpGet]
		public IActionResult GetAll()
		{
			var studyPlans = _unitOfWork.StudyPlanRepository.GetAll("Subjects,Grade,Subjects.Subject").ToList();
			return Json(new { data = studyPlans });
		}

		[HttpDelete, ActionName("DeleteSubject")]
		public IActionResult Delete(int id)
		{
			try
			{
				var studyPlan = _unitOfWork.StudyPlanSubjectRepository.Get(sp => sp.Id == id);
				_unitOfWork.StudyPlanSubjectRepository.Remove(studyPlan);
				_unitOfWork.Save();
				return Json(new { success = true, message = "Subject deleted successfully", subjectId = studyPlan.SubjectId });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = "Something failed while deleting subject" });
			}
		}

		[HttpDelete, ActionName("Delete")]
		public IActionResult DeleteStudyPlan(int id)
		{
			try
			{
				var studyPlan = _unitOfWork.StudyPlanRepository.Get(sp => sp.Id == id);
				if(studyPlan is null)
				{
					return Json(new { sucess = false, message = "study plan was not found" });
				}
				_unitOfWork.StudyPlanRepository.Remove(studyPlan);
				_unitOfWork.Save();
				return Json(new { success = true, message = "study plan deleted successfully" });
			}catch (Exception ex)
			{
				return Json(new { success = false, message = "something failed while deleting this study plan" });
			}
		}
		#endregion
	}
}
