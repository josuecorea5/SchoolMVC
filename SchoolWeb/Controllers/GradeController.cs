using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Domain.Entities;
using School.Infrastructure.Repositories;
using School.Infrastructure.Services;
using School.Utility;

namespace SchoolWeb.Controllers
{
    [Authorize(Roles = StaticDetails.ROLE_ADMIN)]
    public class GradeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

        public GradeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
		{
			List<Grade> grades = _unitOfWork.GradeRepository.GetAll().ToList();

			return View(grades);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Grade grade)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_unitOfWork.GradeRepository.Add(grade);
					_unitOfWork.Save();
					TempData["success"] = "Grade created successfully";
					return RedirectToAction("Index");
				}
			}
			catch (Exception)
			{
				TempData["error"] = "Error while creating register";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Edit(int? id)
		{
			if(id is null || id <= 0)
			{
				return NotFound();
			}

			var grade = _unitOfWork.GradeRepository.Get(grade => grade.Id == id);

			if(grade is null)
			{
				return NotFound();
			}

			return View(grade);
		}

		[HttpPost]
		public IActionResult Edit(Grade grade)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_unitOfWork.GradeRepository.Update(grade);
					_unitOfWork.Save();
					TempData["success"] = "Grade updated successfully";
					return RedirectToAction("Index");
				}
			}
			catch (Exception)
			{
				TempData["error"] = "Something failed while updateding register";
				return RedirectToAction("Index");
			}

			return View();
		}

		public IActionResult Delete(int? id)
		{
			if(id is null || id <= 0)
			{
				return NotFound();
			}

			var grade = _unitOfWork.GradeRepository.Get(grade => grade.Id == id);

			if(grade is null)
			{
				return NotFound();
			}

			return View(grade);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteGrade(int? id)
		{
			var grade = _unitOfWork.GradeRepository.Get(grade => grade.Id == id);
			if(grade is null)
			{
				return NotFound();
			}
			_unitOfWork.GradeRepository.Remove(grade);
			_unitOfWork.Save();
			TempData["success"] = "Grade deleted successfully";
			return RedirectToAction("Index");
		}
	}
}
