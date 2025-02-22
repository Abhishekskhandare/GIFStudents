using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIFStudents.ViewModel
{
	public class StudentListViewModel
	{
        public List<StudentDetail> StudentDetail {  get; set; }
    }

	public class StudentDetail
	{
		public int StudentId { get; set; }
		public string StudentName { get; set; }
		public string ParentName { get; set; }
		public string StudentClass { get; set; }
		public string ParentEmail { get; set; }
		public string ParentMobile { get; set; }
		public bool IsActive { get; set; }
	}
}