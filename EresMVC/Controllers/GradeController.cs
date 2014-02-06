using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EresData;

namespace EresMVC.Controllers
{
    public class GradeController : Controller
    {
        //
        // GET: /Grade/

        public ActionResult Index()
        {
            Storage model = new Storage();
            return View(model.getGrades());
        }

        //
        // GET: /Grade/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Grade/Create

        public ActionResult Create()
        {
            Models.Grade.CreateModel model = new Models.Grade.CreateModel();
            model.NewGrade = new Grade();
            model.Realizations = getRealisationSelectList("");
            return View(model);
        }

        //
        // POST: /Grade/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string name = Convert.ToString(collection["NewGrade.Name"]);
                int realisation = Convert.ToInt32(collection["NewGrade.RealisationID"]);
                string maxValue = Convert.ToString(collection["NewGrade.MaxValue"]);

                Storage m = new Storage();
                m.createGrade(realisation, name, maxValue);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Grade/Edit/5

        public ActionResult Edit(int id)
        {
            Storage m = new Storage();
            Grade grade = m.getGradeById(id);
            Models.Grade.CreateModel model = new Models.Grade.CreateModel();
            model.NewGrade = grade;
            model.Realizations = getRealisationSelectList(grade.RealisationID.ToString());

            return View(model);
        }

        //
        // POST: /Grade/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                string name = Convert.ToString(collection["NewGrade.Name"]);
                int realisation = Convert.ToInt32(collection["NewGrade.RealisationID"]);
                string maxValue = Convert.ToString(collection["NewGrade.MaxValue"]);


                if (string.IsNullOrEmpty(name))
                    ModelState.AddModelError("nazwa", "Musisz podać nazwę oceny!");
                if(string.IsNullOrEmpty(maxValue))
                    ModelState.AddModelError("max wartosc", "Musisz podać maksymalna wartos!");

                if (ModelState.IsValid)
                {
                    Storage m = new Storage();
                    Grade grade = m.getGradeById(id);
                    m.updateGrade(id, name, maxValue, realisation, grade.TimeStamp);
                }
                else
                {
                    Storage m = new Storage();
                    Grade grade = m.getGradeById(id);
                    Models.Grade.CreateModel model = new Models.Grade.CreateModel();
                    model.NewGrade = grade;
                    model.Realizations = getRealisationSelectList(grade.RealisationID.ToString());

                    return View(model);
                }

                
            }
            catch (EresDataContextException e)
            {
                handleException(e);
            }
            catch
            {
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Grade/Delete/5

        public ActionResult Delete(int id)
        {
            Storage m = new Storage();
            Grade grade = m.getGradeById(id);
            return View(grade);
        }

        //
        // POST: /Grade/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Storage m = new Storage();
                Grade grade = m.getGradeById(id);
                m.deleteGrade(grade);

                
            }
            catch(EresDataContextException e)
            {

                handleException(e);

            }
            return RedirectToAction("Index");
        }

        private SelectList getRealisationSelectList(string realisation)
        {
            Storage m = new Storage();
            var roles = m.getRealisations().Select(x =>
                            new SelectListItem
                            {
                                Value = x.RealisationID.ToString(),
                                Text = x.ToString(),

                            });
            return new SelectList(roles, "Value", "Text", realisation); ;
        }


        private void handleException(EresDataContextException e)
        {
            ModelState.AddModelError(e.OperationType, e.Message);
        }
    }
}
