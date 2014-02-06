using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EresData;


namespace EresMVC.Models.Grade
{
    public class CreateModel
    {
        public IEnumerable<SelectListItem> Realizations;
        public EresData.Grade NewGrade;
    }


}