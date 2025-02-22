using GIFStudents.EFModel;
using StudentsInfo.Service.IService;
using StudentsInfo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentsInfo.Service
{
    public class StudentService : IStudentService
    {
        GIFDBEntities _db = new GIFDBEntities();
        public StudentListViewModel GetAllService()
        {
            List<StudentDetail> studentDetails = (from student in _db.Users
                                                  join parentStudent in _db.ParentStudents on student.Id equals parentStudent.StudentId
                                                  join parent in _db.Users on parentStudent.ParentId equals parent.Id
                                                  join studentClass in _db.StudentClasses on student.Id equals studentClass.StudentId
                                                  join schoolClass in _db.SchoolClasses on studentClass.ClassId equals schoolClass.Id
                                                  where student.UserType == (int)UserType.Student
                                                  select new StudentDetail
                                                  {
                                                      StudentId = student.Id,
                                                      StudentName = student.FirstName + " " + student.LastName,
                                                      ParentName = parent.FirstName + " " + parent.LastName,
                                                      StudentClass = schoolClass.Name,
                                                      ParentEmail = parent.Email,
                                                      ParentMobile = parent.Phone,
                                                      IsActive = student.Active
                                                  }).ToList();

            StudentListViewModel model = new StudentListViewModel();
            model.StudentDetail = studentDetails;
            return model;

        }

        public StudentDetail GetStudentById(int id)
        {

            StudentDetail student = (from s in _db.Users
                                     join ps in _db.ParentStudents on s.Id equals ps.StudentId
                                     join p in _db.Users on ps.ParentId equals p.Id
                                     join sc in _db.StudentClasses on s.Id equals sc.StudentId
                                     join cls in _db.SchoolClasses on sc.ClassId equals cls.Id
                                     where s.Id == id
                                     select new StudentDetail
                                     {
                                         StudentId = s.Id,
                                         StudentName = s.FirstName + " " + s.LastName,
                                         ParentName = p.FirstName + " " + p.LastName,
                                         ParentEmail = p.Email,
                                         ParentMobile = p.Phone,
                                         StudentClass = cls.Id.ToString(), // Store Class ID
                                         IsActive = s.Active
                                     }).FirstOrDefault();

            return student;
        }

        public List<SelectListItem> GetSchoolClasses()
        {
            return _db.SchoolClasses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }


        public void UpdateStudents(StudentListViewModel model)
        {
            if (model.StudentDetail != null)
            {
                foreach (var student in model.StudentDetail)
                {
                    UpdateStudentStatus(student.StudentId, student.IsActive);
                }
            }
        }

        public void UpdateStudentStatus(int studentId, bool isActive)
        {
            var student = _db.Users.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                student.Active = isActive;
                _db.SaveChanges();
            }
        }

        public void EditStudent(StudentDetail model)
        {
            if (model is null) return;

            var student = _db.Users.FirstOrDefault(s => s.Id == model.StudentId);
            var parent = _db.Users.FirstOrDefault(p => p.Email == model.ParentEmail);

            if (student != null && parent != null)
            {
                string[] nameParts = model.StudentName.Split(' ');
                student.FirstName = nameParts[0];
                student.LastName = nameParts.Length > 1 ? nameParts[1] : "";
                student.Active = model.IsActive;
                parent.Email = model.ParentEmail;
                parent.Phone = model.ParentMobile;
                var studentClass = _db.StudentClasses.FirstOrDefault(sc => sc.StudentId == student.Id);
                if (studentClass != null)
                {
                    studentClass.ClassId = int.Parse(model.StudentClass);
                }
                _db.SaveChanges();
            }

        }

        public enum UserType
        {
            Student = 1,
            Parent = 2
        }

    }
}