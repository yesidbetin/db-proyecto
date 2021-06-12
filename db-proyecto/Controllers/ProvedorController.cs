using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using db_proyecto.Models;

namespace Provedor.Controllers
{
    public class ProvedorController : Controller
    {
        // GET: Provedor
        public ActionResult Index()
        {
            return View();
        }
    }
}