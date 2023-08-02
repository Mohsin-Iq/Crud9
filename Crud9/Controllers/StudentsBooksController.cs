using Crud9.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Net;
using System.Web.Services.Protocols;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Web;

namespace Crud9.Controllers
{
    [Authorize]
    public class StudentsBooksController : Controller
    {
       
        StudentBookConnection sb = new StudentBookConnection();
        [AllowAnonymous]
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Student st)
        {
            var IsValidUser = sb.GetStudents().FirstOrDefault(user => user.Email == st.Email && user.Password == st.Password);
            if (IsValidUser != null)
            {

                string validStudentName = IsValidUser?.StudentName;
                string role = IsValidUser.IsAdmin ? "Admin" : "User";
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
           1,
           IsValidUser.StudentName,
           DateTime.Now,
           DateTime.Now.AddMinutes(30),
           false, role, FormsAuthentication.FormsCookiePath);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);
                if (IsValidUser.IsAdmin)
                {
                    return RedirectToAction("AdminIndex", "StudentsBooks");
                }
                else
                {
                    return RedirectToAction("Index", "StudentsBooks");
                }

            }
            ModelState.AddModelError(nameof(st.Email), "Incorrect UserName");
            ModelState.AddModelError(nameof(st.Password), "Incorrect password");
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [Authorize]
        public ActionResult AdminIndex()
        {
            ModelState.Clear();
            return View(sb.GetStudents());
        }
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(sb.GetStudents());
        }
        

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var model = new StudentViewModel()
            {
                BookList = sb.GetBooks()
            };
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Create(StudentViewModel st)
        {
            if (ModelState.IsValid )
            {
                
                sb.AddStudent(st);
                return RedirectToAction("Index");
            }
            return View(st);
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {

                if (sb.DeleteStudents(id))
                {
                    ViewBag.AlertMsg = "Student Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
               throw;
            }
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            Student students = sb.GetStudents().Where(obj => obj.StudentID == id).First();
            return View(students);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var bookview =sb.GetStudents().Find(Models => Models.StudentID == id);
            var Allbooks = sb.GetBooks();
            foreach (var book in Allbooks)
            {
                book.IsSelected = bookview.BookList.Any(b => b.BookID == book.BookID);
            }
            bookview.BookList = Allbooks;
           
            return View(bookview);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Edit(StudentViewModel sModels)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    sb.AddStudent(sModels);
                    return RedirectToAction("Index");
                }
                catch
                {
                    throw;
                }
            }
            sModels.BookList = sb.GetBooks();
            return View(sModels);

        }
    }
}


