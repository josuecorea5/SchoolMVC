using Microsoft.AspNetCore.Mvc;
using School.Domain.Entities;
using School.Infrastructure.Repositories;
using SchoolWeb.ViewModels;
using System.Diagnostics;

namespace SchoolWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
		{
			var teachers = _unitOfWork.TeacherRepository.GetAll();
			var studyPlans = _unitOfWork.StudyPlanRepository.GetAll();
			var students = _unitOfWork.StudentRepository.GetAll();
			var grades = _unitOfWork.GradeRepository.GetAll();

			DashboardInfoViewModel dashboardInfoViewModel = new DashboardInfoViewModel
			{
				TotalGrades = grades.Count(),
				TotalStudents = students.Count(),
				TotalStudyPlans = studyPlans.Count(),
				TotalTeachers = teachers.Count(),
			};

			return View(dashboardInfoViewModel);
		}

		[HttpGet]
		public IActionResult EnrollmentsInformation()
		{
            var enrrolements = _unitOfWork.EnrollmentRepository.GetAll("Grade");
				
            var enrollmentsByGrade = from e in enrrolements
                                     group e by e.GradeId into grade
                                     select new EnrollmentStudentsInformation
                                     {
                                         GradeId = grade.Key,
                                         TotalEnrollment = grade.Count(),
                                         GradeName = grade.FirstOrDefault()?.Grade.Name,
                                     };
			return Json(new { data = enrollmentsByGrade });
        }

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
