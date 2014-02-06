using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EresData;

namespace EresMVC.Controllers
{
    public class RealizationController : Controller
    {
        //
        // GET: /Realization/

        public ActionResult Index()
        {
            Storage model = new Storage();
            return View(model.getRealisations());
        }

        //
        // GET: /Realization/Details/5

        public ActionResult Details(int id)
        {
            Storage model = new Storage();

            return View(model.getRealisationStudents(id));
        }

        //
        // GET: /Realization/Create


        public ActionResult OcenyStudenta(int id)
        {
            Storage model = new Storage();
           

            return View(model.getGradeValues());
        }

         public ActionResult EditGV(int id)
        {
            Storage m = new Storage();
            GradeValue g = m.getGradeValueById(id);
            return View(g);
        }

        //
        // POST: /Grade/Edit/5

         [HttpPost]
         public ActionResult EditGV(int id, FormCollection collection)
         {
              Storage m = new Storage();
                     GradeValue grade = m.getGradeValueById(id);
             int rid = m.getRegistrations().Find(r=>r.RegistrationID == grade.RegistrationID).StudentID;
             //int rid = m.getGradeValues().Find(o => o.R)
             try
             {
                 string name = Convert.ToString(collection["Value"]);


                 if (string.IsNullOrEmpty(name))
                     ModelState.AddModelError("nazwa", "Musisz podać nazwę oceny!");


                 if (ModelState.IsValid)
                 {
                    
                     m.updateGV(id, name, DateTime.Today.ToString("dd-MM-yyyy"), grade.RegistrationID, grade.GradeID, grade.TimeStamp);
                 }
                 else
                 {
                     return View(grade);
                 }


             }
             catch (EresDataContextException e)
             {
                 handleException(e);
             }
             catch
             {
             }

             return RedirectToAction("OcenyStudenta", new {id = rid });
         }

         private void handleException(EresDataContextException e)
         {
             ModelState.AddModelError(e.OperationType, e.Message);
         }

    }
}
