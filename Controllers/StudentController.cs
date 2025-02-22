using StudentsInfo.Service.IService;
using StudentsInfo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsInfo.Controllers
{
    public class StudentController : Controller
    {
		private readonly IStudentService _Service;
		public StudentController(IStudentService Service)
		{
            _Service = Service;
		}
        [HttpGet]
        public ActionResult GetAllStudents()
        {
            StudentListViewModel viewModel = _Service.GetAllService();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UpdateStudents(StudentListViewModel model)
        {
            _Service.UpdateStudents(model);
            return RedirectToAction("GetAllStudents"); 
        }

        [HttpGet]
        public ActionResult EditStudentById(int id)
        {
            StudentDetail student = _Service.GetStudentById(id);
            ViewBag.Classes = _Service.GetSchoolClasses();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(StudentDetail model)
        {
            if (ModelState.IsValid)
            {
                _Service.EditStudent(model);
            }
            return RedirectToAction("GetAllStudents");
        }


    }
}