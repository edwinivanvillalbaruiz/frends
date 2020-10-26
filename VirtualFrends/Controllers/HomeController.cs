using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualFrends.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        AdminTerceros Admin = new AdminTerceros();
        public ActionResult Index()
        {
            ViewBag.Saldo = Admin.SaldoDiario();
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}