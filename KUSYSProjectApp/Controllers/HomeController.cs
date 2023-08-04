using KUSYSProjectApp.Data;
using KUSYSProjectApp.Models;
using KUSYSProjectApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KUSYSProjectApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var students = _context.Students.Where(x => x.IsDeleted == false).ToList();

            // Öğrenci verilerini StudentViewModel nesnesine dönüştürme
            var studentViewModels = students.Select(student => new StudentViewModel
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                CourseId = student.CourseId
            }).ToList();

            return View(studentViewModels);
        }

        public IActionResult CreateStudent()
        {
            ViewBag.Courses = _context.Courses;
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                var course = _context.Courses.FirstOrDefault(c => c.CourseId == student.CourseId);
                Student newStudent = new Student()
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    BirthDate = student.BirthDate,
                    CourseId = student.CourseId

                };
                newStudent.Course = course;
                _context.Students.Add(newStudent);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(student);

        }

        public IActionResult UpdateStudent(int id)
        {
            var updateStudent = _context.Students.Find(id);
            ViewBag.Courses = _context.Courses;
            return View(updateStudent);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudentAsync(StudentViewModel student)
        {
            var updateStudent = await _context.Students.FindAsync(student.StudentId);

            updateStudent.FirstName = student.FirstName;
            updateStudent.LastName = student.LastName;
            updateStudent.BirthDate = student.BirthDate;
            updateStudent.CourseId = student.CourseId;
            var course = await _context.Courses.FindAsync(student.CourseId);

            updateStudent.Course = course;

            _context.Update(updateStudent);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> DeleteStudent(int ID)
        {
            var deleteStudent = await _context.Students.FindAsync(ID);
            deleteStudent.IsDeleted = true;
            _context.Update(deleteStudent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ///////////////course/////////////

        public IActionResult CreateCourse()

        {
            ViewBag.CourseList = _context.Courses.Where(d => d.IsDeleted == false).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult CreateCourse(Course course)
        {
            _context.Add(course);
            _context.SaveChanges();

            return RedirectToAction(nameof(CreateCourse));      
        }


        public IActionResult UpdateCourse(int id)
        {
            var updateCourse = _context.Courses.Find(id);
            return View(updateCourse);
        }

        [HttpPost]
        public IActionResult UpdateCourse(Course course)
        {
            _context.Update(course);
            _context.SaveChanges();

            return RedirectToAction(nameof(CreateCourse));
        }


        public async Task<IActionResult> DeleteCourse(string Id)
        {
            var deleteCourse = await _context.Courses.FindAsync(Id);

            deleteCourse.IsDeleted = true;
            _context.Update(deleteCourse);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(CreateCourse));
        }


            public JsonResult GetStudentDetails(int id)
        {
            var student = _context.Students.FirstOrDefault(student => student.StudentId == id);

            return Json(student);
        }




    }
}
