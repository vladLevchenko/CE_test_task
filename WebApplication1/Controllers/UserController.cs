using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Interfaces;
using System.Globalization;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly IStudentOperations _studentService;

        public UserController(IStudentOperations studentService)
        {
            _studentService = studentService;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var dalModel = _studentService.GetByName(searchString);
                if (dalModel != null)
                {
                    var viewModel = new Student
                    {
                        Email = dalModel.Email,
                        Name = dalModel.Name,
                        StartDate = dalModel.StartDate.ToString("MM/dd/yyyy"),
                        EndDate = dalModel.EndDate.ToString("MM/dd/yyyy")
                    };
                    return View("Update",viewModel);
                }
                else
                    ViewBag.ErrorMessage = "No such student";
            }
            return View();
        }

        public ActionResult Update(Student viewModel)
        {
            var oldData = _studentService.GetByName(viewModel.Name);
            var hStartDate = DateTime.ParseExact(viewModel.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var hEndDate = DateTime.ParseExact(viewModel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var daysToAdd = (hEndDate - hStartDate).Days % 7 == 0 ? (hEndDate - hStartDate).Days : (((hEndDate - hStartDate).Days) / 7) * 7 + 7;

            oldData.EndDate = oldData.EndDate.AddDays(daysToAdd);

            _studentService.EditStudent(oldData);
            return RedirectToAction("Index", "User");
        }
        public ActionResult SaveStudentCourse(Student viewModel)
        {
            _studentService.SaveStudent(new DAL.StudentDAL
            {
                Email = viewModel.Email,
                Name = viewModel.Name,
                StartDate = DateTime.ParseExact(viewModel.StartDate, "MM/dd/yyyy",CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(viewModel.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture)
            });
             return RedirectToAction("Index","User");
        }
    }
}