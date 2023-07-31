using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MVCDemoProject.Models;

namespace MVCDemoProject.Controllers
{
    public class StudentController : Controller
    {
        private StudentDBContext _dataBase = new StudentDBContext();

        // GET: Student
        public ActionResult Index(string searchBy, string search, int? page)
        {
            var stdList = _dataBase.studentList
                .OrderBy(s => s.StudentId)
                .ToList()
                .ToPagedList(page ?? 1, 5);

            if (searchBy == "Gender")
            {
                stdList = _dataBase.studentList
                    .Where(s => s.Gender == search || search == null)
                    .ToList()
                    .ToPagedList(page ?? 1, 5);
            }
            else if (searchBy == "Name")
            {
                stdList = _dataBase.studentList
                    .Where(s => s.StudentName
                    .StartsWith(search) || search == null)
                    .ToList()
                    .ToPagedList(page ?? 1, 5);
            }

            return View(stdList);
        }

        // POST: Student
        [HttpPost]
        public ActionResult Index(string search)
        {
            List<Student> students;
            if (string.IsNullOrEmpty(search))
            {
                students = _dataBase.studentList.ToList();
            }
            else
            {
                students = _dataBase.studentList
                    .Where(s => s.StudentName
                    .StartsWith(search))
                    .ToList();
            }

            return View(students);
        }

        public JsonResult GetStudents(string term)
        {
            List<string> students = _dataBase.studentList
                .Where(s => s.StudentName.StartsWith(term))
                .Select(t => t.StudentName)
                .ToList();

            return Json(students, JsonRequestBehavior.AllowGet);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Create(Student std)
        {
            if (_dataBase.studentList.Any(s => s.StudentId == std.StudentId))
            {
                ModelState.AddModelError("StudentId", "A student with this ID already exists.");
            }

            if (_dataBase.studentList.Any(s => s.StudentName == std.StudentName))
            {
                ModelState.AddModelError("StudentName", "A student with this name already exists.");
            }

            if (ModelState.IsValid)
            {
                std.JoiningDate = DateTime.Now;
                _dataBase.studentList.Add(std);
                _dataBase.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(std);
        }

        // GET: Student/Edit/name
        public ActionResult Edit(string name)
        {
            var student = _dataBase.studentList.FirstOrDefault(s => s.StudentName == name);
            return View(student);
        }

        // POST: Student/Edit/name
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                _dataBase.Entry(std).State = EntityState.Modified;
                _dataBase.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(std);
        }

        // GET: Student/Details/name
        public ActionResult Details(string name)
        {
            var student = _dataBase.studentList.FirstOrDefault(s => s.StudentName == name);

            return View(student);
        }

        // GET: Student/Delete/name
        public ActionResult Delete(string name)
        {
            var student = _dataBase.studentList.FirstOrDefault(s => s.StudentName == name);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/name
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string name)
        {
            var student = _dataBase.studentList.FirstOrDefault(s => s.StudentName == name);

            if (student == null)
            {
                return HttpNotFound();
            }

            _dataBase.studentList.Remove(student);
            _dataBase.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _dataBase.Dispose();
            base.Dispose(disposing);
        }
    }
}