using StudentsInfo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentsInfo.Service.IService
{
	public interface IStudentService
	{
		StudentListViewModel GetAllService();
		void UpdateStudents(StudentListViewModel model);
		StudentDetail GetStudentById(int id);
		List<SelectListItem> GetSchoolClasses();
		void EditStudent(StudentDetail model);
	}
}
