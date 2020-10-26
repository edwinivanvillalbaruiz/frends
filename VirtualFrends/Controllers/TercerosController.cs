using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Net;
using System.Web.Mvc;
using VirtualFrends.Models;

namespace VirtualFrends.Controllers
{
    [Authorize]
    public class TercerosController : Controller
    {
        // GET: Terceros
        AdminTerceros Admin = new AdminTerceros();

        public ActionResult Index()
        {
            return View(Admin.Consultar());
        }

        public ActionResult Consultas()
        {
            return View(Admin.Mimatrix());
        }
        public ActionResult MiMatrix()
        {
            return View(Admin.Mimatrix());
        }
        public ActionResult Detalle(int id)
        {
            return View(Admin.Consultar(id));
        }

        public ActionResult Crear()
        {
            Contexto db = new Contexto();
            ViewBag.TipoDoumento = new SelectList(db.TipoDocumento, "IdTipoDocumento", "Nombre");
            return View();
        }
        [Authorize]
        public ActionResult Guardar(Terceros modelo)
        {
            Admin.Guardar(modelo);
            return View("Detalle", modelo);
        }

        public ActionResult Editar(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contexto db = new Contexto();
            ViewBag.TipoDoumento = new SelectList(db.TipoDocumento, "IdTipoDocumento", "Nombre");

            Terceros terceros = Admin.Consultar(Convert.ToInt16(id));
            if (terceros == null)
            {
                return HttpNotFound();
            }

            return View(terceros);

        }

        public ActionResult Modificar(Terceros modelo)
        {

            Admin.Modificar(modelo);

            return View("Perfil", modelo);
        }
        [Authorize]
        public ActionResult Perfil()
        {

            return View(Admin.Perfil());
        }
        public ActionResult MatrixGeneral()
        {
            var user = User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser userusu = userManager.FindById(user);
            ViewBag.EmailLogeado = userusu.Nombres + " " + userusu.Apellidos;
            return View(Admin.MiMatrixLider());
        }

        public ActionResult Organizacion()
        {
            var user = User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser userusu = userManager.FindById(user);
            ViewBag.EmailLogeado = userusu.Nombres + " " + userusu.Apellidos;
            return View(Admin.Mimatrix());
        }

        public ActionResult Actividad()
        {
            ViewBag.Saldo = Admin.SaldoDiario();
            return View(Admin.MiViewDiario());
        }
    }
}