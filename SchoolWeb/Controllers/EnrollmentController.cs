using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Domain.Entities;
using School.Domain.Enums;
using School.Infrastructure.Repositories;
using SchoolWeb.ViewModels;
using System.Diagnostics;

namespace SchoolWeb.Controllers
{
	public class EnrollmentController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public EnrollmentController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			List<Student> students = _unitOfWork.StudentRepository.GetAll().ToList();
			List<Grade> grades = _unitOfWork.GradeRepository.GetAll().ToList();

			CreateEnrollmentViewModel createEnrollmentViewModel = new CreateEnrollmentViewModel
			{
				StudentLists = students.Select(student => new SelectListItem { Text = $"{student.Name} {student.LastName}", Value = student.Id.ToString() }).ToList(),
				GradeLists = grades.Select(grade => new SelectListItem { Text = grade.Name, Value = grade.Id.ToString() }).ToList(),
			};

			return View(createEnrollmentViewModel);
		}

		public IActionResult Edit(int id)
		{
			var enrollment = _unitOfWork.EnrollmentRepository.Get(enrollment => enrollment.Id == id);

			if(enrollment is null)
			{
					TempData["error"] = "Enrollment was not found";
					return RedirectToAction("Index");
			}

			List<Student> students = _unitOfWork.StudentRepository.GetAll().ToList();
			List<Grade> grades = _unitOfWork.GradeRepository.GetAll().ToList();
			EditEnrollmentViewModel editEnrollmentViewModel = new EditEnrollmentViewModel
			{
				StudentLists = students.Select(student => new SelectListItem { Text = $"{student.Name} {student.LastName}", Value = student.Id.ToString()}).ToList(),
				GradeLists = grades.Select(grade => new SelectListItem { Text = grade.Name, Value = grade.Id.ToString() }).ToList(),
				GradeId = enrollment.GradeId,
				EnrollmentStatus = enrollment.EnrollmentStatus,
			};

			return View(editEnrollmentViewModel);
		}

		public IActionResult Delete(int id)
		{
			var enrollment = _unitOfWork.EnrollmentRepository.Get(enrollment => enrollment.Id == id, "Student,Grade");

			if(enrollment is null)
			{
				TempData["error"] = "Enrollment was not found";
				return RedirectToAction("Index");
			}

			return View(enrollment);
		}

		[HttpPost]
		public IActionResult Create(CreateEnrollmentViewModel createEnrollmentViewModel)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var studentEnrollments = _unitOfWork.StudentRepository.Get(student => student.Id == createEnrollmentViewModel.StudentId, "Enrollments");

					if (studentEnrollments.Enrollments.Any(student => student.EnrollmentStatus != EnrollmentStatus.PASSED))
					{
						TempData["error"] = "Student is already enrolled in other grade";
						return RedirectToAction("Index");
					}

					var newStundentEnrollment = new Enrollment
					{
						StudentId = createEnrollmentViewModel.StudentId,
						GradeId = createEnrollmentViewModel.GradeId,
						EnrollmentStatus = EnrollmentStatus.CURRENTLY_ENROLLED,
					};

					_unitOfWork.EnrollmentRepository.Add(newStundentEnrollment);
					_unitOfWork.Save();
					TempData["success"] = "Enrollment created successfully";
					return RedirectToAction("Index");
				}
			}catch (Exception ex)
			{
				TempData["error"] = "Something went wrong while creating the register";
				return RedirectToAction("Index");
			}
			return View(createEnrollmentViewModel);
		}

		[HttpPost]
		public IActionResult Edit(EditEnrollmentViewModel enrollmentViewModel)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var enrollment = _unitOfWork.EnrollmentRepository.Get(enrollment => enrollment.Id == enrollmentViewModel.Id);
					if(enrollment is null)
					{
						TempData["error"] = "Enrollment was not found";
						return RedirectToAction("Index");
					}

					var studentEnrollments = _unitOfWork.StudentRepository.Get(student => student.Id == enrollment.StudentId, "Enrollments");

					var checkPassedGrades = studentEnrollments.Enrollments.
						Where(enrollment => enrollment.Id != enrollmentViewModel.Id).ToList();
					
					if(checkPassedGrades.Any(student => student.EnrollmentStatus != EnrollmentStatus.PASSED))
					{
						TempData["error"] = "You cannot update the state of a course while other course is not in passed state";
						return RedirectToAction("Index");
					}

					enrollment.GradeId = enrollmentViewModel.GradeId;
					enrollment.EnrollmentStatus = enrollmentViewModel.EnrollmentStatus;

					_unitOfWork.EnrollmentRepository.Update(enrollment);
					_unitOfWork.Save();

					TempData["success"] = "Grade updated successfully";

					return RedirectToAction("Index");

				}
			}catch (Exception ex)
			{
				TempData["error"] = "Something went wrong while updating the register";
				return RedirectToAction("Index");
			}

			return View(enrollmentViewModel);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteEnrollment(int? id)
		{
			try
			{
				var enrollment = _unitOfWork.EnrollmentRepository.Get(enrollment => enrollment.Id == id);
				if (enrollment is null)
				{
					TempData["error"] = "Enrollment was not found";
					return RedirectToAction("Index");
				}

				_unitOfWork.EnrollmentRepository.Remove(enrollment);
				_unitOfWork.Save();
				TempData["success"] = "Enrollment deleted successfully";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["error"] = "Something failed while deleting enrollment";
				return RedirectToAction("Index");
			}
		}

		#region
		[HttpGet]
		public IActionResult GetAll()
		{
			List<Enrollment> enrollments = _unitOfWork.EnrollmentRepository.GetAll("Student,Grade").ToList();
			return Json(new { data = enrollments });
		}
		#endregion
	}
}
