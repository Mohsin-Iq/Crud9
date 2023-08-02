/*using Crud9.Models;
using System.Linq;
using System.Web.Mvc;


namespace Crud9.Controllers
{
    public class StudentController : Controller
    {
        StudendsDBHandler sdb = new StudendsDBHandler();
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(sdb.GetStudent());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Students smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sdb.AddStudents(smodel))
                    {
                        ViewBag.Message = "Student Details Added Successfully";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(sdb.GetStudent().Find(smodel => smodel.Id == id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Students smodel)
        {
            try
            {
                sdb.UpdateDetails(smodel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                if (sdb.DeleteStudents(id))
                {
                    ViewBag.AlertMsg = "Student Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            Students student = sdb.GetStudent().Where(obj => obj.Id == id).First();
            return View(student);
        }
   
    }
}*/