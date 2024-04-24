using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Domain.Entities;
using School.Infrastructure.Repositories;
using School.Infrastructure.Services;
using School.Utility;
using SchoolWeb.ViewModels;

namespace SchoolWeb.Controllers
{
    [Authorize(Roles = StaticDetails.ROLE_ADMIN)]
    public class TeacherController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;

		public TeacherController(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
		{
			_unitOfWork = unitOfWork;
			_cloudinaryService = cloudinaryService;
		}

		public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            if(id <= 0)
            {
                TempData["error"] = "Teacher not found";
                return RedirectToAction("Index");
            }

            var teacher = _unitOfWork.TeacherRepository.Get(teacher => teacher.Id == id);

            if(teacher is null)
            {
                TempData["error"] = "Teacher not found";
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        public IActionResult Edit(int id)
        {
            if(id <= 0)
            {
                TempData["error"] = "Teacher not found";
                return RedirectToAction("Index");
            }

            var teacher = _unitOfWork.TeacherRepository.Get(teacher => teacher.Id == id);

            if(teacher is null)
            {
                TempData["error"] = "Teacher not found";
                return RedirectToAction("Index");
            }

            var teacherViewModel = new EditTeacherViewModel
            {
                Name = teacher.Name,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Title = teacher.Title,
            };

            return View(teacherViewModel);
        } 

        public IActionResult Delete(int id)
        {
            if(id <= 0)
            {
				TempData["error"] = "Teacher not found";
				return RedirectToAction("Index");
			}

			var teacher = _unitOfWork.TeacherRepository.Get(teacher => teacher.Id == id);

			if (teacher is null)
			{
				TempData["error"] = "Teacher not found";
				return RedirectToAction("Index");
			}

            return View(teacher);
		}

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeacherViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var uploadImage = await _cloudinaryService.AddPhoto(model.Image);

                    var newTeacher = new Teacher
                    {
                        Name = model.Name,
                        LastName = model.LastName,
                        Email = model.Email,
                        Title = model.Title,
                        Image = uploadImage.SecureUrl.ToString()
                    };

                    _unitOfWork.TeacherRepository.Add(newTeacher);
                    _unitOfWork.Save();

                    TempData["success"] = "Teacher created successfully";
                    return RedirectToAction("Index");
                }
            }catch (Exception)
            {
                TempData["error"] = "Error while creating register";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditTeacherViewModel model)
        {
            try
            {
				if (ModelState.IsValid)
				{
					var teacher = _unitOfWork.TeacherRepository.Get(s => s.Id == model.Id);
					teacher.Name = model.Name;
					teacher.LastName = model.LastName;
					teacher.Email = model.Email;
					teacher.Title = model.Title;

					_unitOfWork.TeacherRepository.Update(teacher);
					_unitOfWork.Save();
					TempData["success"] = "Teacher updated successfully";
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
            {
				TempData["error"] = "Something failed while updating register";
				return RedirectToAction("Index");
			}

			return View(model);
		}

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
			var teacher = _unitOfWork.TeacherRepository.Get(teacher => teacher.Id == id);

			if (teacher is null)
			{
				TempData["error"] = "Student not found";
				return RedirectToAction("Index");
			}

			try
            {
                var deleteImage = await _cloudinaryService.DeletePhoto(teacher.Image);
                _unitOfWork.TeacherRepository.Remove(teacher);
                _unitOfWork.Save();

				TempData["success"] = "Teacher deleted successfully";
				return RedirectToAction("Index");
			}
			catch (Exception)
            {
				TempData["error"] = "Something failed while deleting register";
				return RedirectToAction("Index");
			}
        }

		#region
		[HttpGet]
		public IActionResult GetAll()
		{
			List<Teacher> teachers = _unitOfWork.TeacherRepository.GetAll().ToList();
			return Json(new { data = teachers });
		}
		#endregion
	}
}
