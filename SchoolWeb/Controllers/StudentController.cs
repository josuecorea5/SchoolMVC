using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.DependencyResolver;
using School.Domain.Entities;
using School.Infrastructure.Repositories;
using School.Infrastructure.Services;
using School.Utility;
using SchoolWeb.ViewModels;
using System.Numerics;

namespace SchoolWeb.Controllers
{
	[Authorize(Roles = StaticDetails.ROLE_ADMIN)]
	public class StudentController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICloudinaryService _cloudinaryService;

		public StudentController(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
		{
			_unitOfWork = unitOfWork;
			_cloudinaryService = cloudinaryService;
		}

		public IActionResult Index()
		{
			List<Student> students = _unitOfWork.StudentRepository.GetAll().ToList();
			return View(students);
		}

		public IActionResult Create()
		{
			return View();
		}

		public IActionResult Detail(int id)
		{
            if (id <= 0)
            {
                TempData["error"] = "Student not found";
                return RedirectToAction("Index");
            }
            var student = _unitOfWork.StudentRepository.Get(s => s.Id == id);

			if(student is null)
			{
                TempData["error"] = "Student not found";
                return RedirectToAction("Index");
            }

			return View(student);
		}

		public IActionResult Edit(int id)
		{
			if (id <= 0)
			{
				TempData["error"] = "Teacher not found";
				return RedirectToAction("Index");
			}
			var student = _unitOfWork.StudentRepository.Get(s =>s.Id == id);


			if (student is null)
			{
				TempData["error"] = "Student not found";
				return RedirectToAction("Index");
			}

			var editStudentViewModel = new EditStudentViewModel
			{
				Id = student.Id,
				DateOfBirth = student.DateOfBirth,
				Email = student.Email,
				LastName = student.LastName,
				Name = student.Name,
				Phone = student.Phone
			};

			return View(editStudentViewModel);
		}

		public IActionResult Delete(int id)
		{
			var student = _unitOfWork.StudentRepository.Get(s => s.Id==id);
			if (student is null)
			{
				TempData["error"] = "Student not found";
				return RedirectToAction("Index");
			}

			return View(student);

		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateStudentViewModel createStudentViewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var uploadImage = await _cloudinaryService.AddPhoto(createStudentViewModel.Image);

					var newStudent = new Student
					{
						Name = createStudentViewModel.Name,
						LastName = createStudentViewModel.LastName,
						Age = DateTime.Now.Year - createStudentViewModel.DateOfBirth.Year,
						DateOfBirth = createStudentViewModel.DateOfBirth,
						Email = createStudentViewModel.Email,
						Phone = createStudentViewModel.Phone,
						Image = uploadImage.SecureUrl.ToString(),
						Code = Guid.NewGuid().ToString(),
					};

					_unitOfWork.StudentRepository.Add(newStudent);
					_unitOfWork.Save();
					TempData["success"] = "Student created successfully";
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				TempData["error"] = "Error while creating register";
				return RedirectToAction("Index");
			}

			return View(createStudentViewModel);
		}

		[HttpPost]
		public IActionResult Edit(EditStudentViewModel editStudentViewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var student = _unitOfWork.StudentRepository.Get(s => s.Id == editStudentViewModel.Id);
					student.Name = editStudentViewModel.Name;
					student.LastName = editStudentViewModel.LastName;
					student.Age = DateTime.Now.Year - editStudentViewModel.DateOfBirth.Year;
					student.DateOfBirth = editStudentViewModel.DateOfBirth;
					student.Email = editStudentViewModel.Email;
					student.Phone = editStudentViewModel.Phone;

					_unitOfWork.StudentRepository.Update(student);
					_unitOfWork.Save();
					TempData["success"] = "Student updated successfully";
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				TempData["error"] = "Something failed while updating register";
				return RedirectToAction("Index");
			}

			return View(editStudentViewModel);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteStudent(int id)
		{
			var student = _unitOfWork.StudentRepository.Get(s => s.Id == id);
			if(student is  null)
			{
				TempData["error"] = "Student not found";
				return RedirectToAction("Index");
			}

			try
			{
				var deleteImage = await _cloudinaryService.DeletePhoto(student.Image);
				_unitOfWork.StudentRepository.Remove(student);
				_unitOfWork.Save();
				TempData["success"] = "Student deleted successfully";
				return RedirectToAction("Index");
			}catch (Exception ex)
			{
				TempData["error"] = ex.Message;
				return RedirectToAction("Index");
			}
		}

		#region
		[HttpGet]
		public IActionResult GetAll()
		{
			List<Student> students = _unitOfWork.StudentRepository.GetAll().ToList();
			return Json(new { data = students });
		}
        #endregion
    }
}
