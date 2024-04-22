using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Domain.Entities;
using School.Infrastructure.Repositories;
using SchoolWeb.ViewModels;

namespace SchoolWeb.Controllers
{
	public class SubjectController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public SubjectController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create()
		{
			List<Teacher> teachers = _unitOfWork.TeacherRepository.GetAll().ToList();
			List<SelectListItem> teacherList = [];

			foreach(var teacher in teachers)
			{
				teacherList.Add(new SelectListItem { Text = $"{teacher.Name} {teacher.LastName}", Value = teacher.Id.ToString() });
			}

			CreateSubjectViewModel subjectVM = new()
            {
				TeacherLists = teacherList
			};

			return View(subjectVM);
		}

		public IActionResult Detail(int id)
		{
			Subject subject = _unitOfWork.SubjectRepository.Get(subject => subject.Id == id, "Teacher");

			if(subject is null)
			{
				TempData["error"] = "Subject was not found";
				return RedirectToAction("Index");
			}

			return View(subject);
		}

		public IActionResult Edit(int id)
		{
			Subject subject = _unitOfWork.SubjectRepository.Get(subject => subject.Id == id, "Teacher");

			if(subject is null)
			{
                TempData["error"] = "Subject was not found";
                return RedirectToAction("Index");
            }

			List<Teacher> teachers = _unitOfWork.TeacherRepository.GetAll().ToList();
			List<SelectListItem> teacherList = [];

			foreach(var teacher in teachers)
			{
				teacherList.Add(new SelectListItem { Text = $"{teacher.Name} {teacher.LastName}", Value = teacher.Id.ToString() });
			}

			var editSubjectVM = new EditSubjectViewModel
			{
				Id = subject.Id,
				Description = subject.Description,
				Name = subject.Name,
				TeacherId = subject.Teacher.Id,
				TeacherLists = teacherList
			};

			return View(editSubjectVM);
		}

		public IActionResult Delete(int id)
		{
			Subject subject = _unitOfWork.SubjectRepository.Get(subject => subject.Id == id, "Teacher");

			if (subject is null)
			{
				TempData["error"] = "Subject was not found";
				return RedirectToAction("Index");
			}

			return View(subject);
		}

		[HttpPost]
		public IActionResult Create(CreateSubjectViewModel subjectVM)
		{
			try
			{
				if(ModelState.IsValid)
				{
					string code = String.Join("", subjectVM.Name.Split(" "));
					var newSubject = new Subject
					{
						Name = subjectVM.Name,
						Description = subjectVM.Description,
						Code = $"{code.ToUpper()}{DateTime.Now.Year}",
						TeacherId = subjectVM.TeacherId,
					};
					_unitOfWork.SubjectRepository.Add(newSubject);
					_unitOfWork.Save();
					TempData["success"] = "Subject created successfully";
					return RedirectToAction("Index");
				}
			}catch (Exception)
			{
				TempData["error"] = "Error while creating register";
				return RedirectToAction("Index");
			}

			return View(subjectVM);
		}

		[HttpPost]
		public IActionResult Edit(EditSubjectViewModel subjectVM)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var subject = _unitOfWork.SubjectRepository.Get(subject => subject.Id == subjectVM.Id);
					if (subject is null)
					{
						TempData["error"] = "Student not found";
						return RedirectToAction("Index");
					}
					string code = String.Join("", subjectVM.Name.Split(" "));
					subject.Name = subjectVM.Name;
					subject.Description = subjectVM.Description;
					subject.TeacherId = subjectVM.TeacherId;
					subject.Code = $"{code.ToUpper()}{DateTime.Now.Year}";

					_unitOfWork.SubjectRepository.Update(subject);
					_unitOfWork.Save();
					TempData["success"] = "subject updated successfully";
					return RedirectToAction("Index");
				}
			}catch (Exception)
			{
				TempData["error"] = "Error while editing register";
				return RedirectToAction("Index");
			}

			return View(subjectVM);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteSubject(int id)
		{
			var subject = _unitOfWork.SubjectRepository.Get(s => s.Id == id);
			if (subject is null)
			{
				TempData["error"] = "Subject not found";
				return RedirectToAction("Index");
			}

			try
			{
				_unitOfWork.SubjectRepository.Remove(subject);
				_unitOfWork.Save();
				TempData["success"] = "Subject deleted successfully";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;
				return RedirectToAction("Index");
			}
		}


		#region
		[HttpGet]
		public IActionResult GetAll()
		{
			List<Subject> subjects = _unitOfWork.SubjectRepository.GetAll("Teacher").ToList();

			return Json(new { data = subjects });
		}
		#endregion
	}
}
