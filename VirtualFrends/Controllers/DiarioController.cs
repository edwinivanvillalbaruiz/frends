using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.EnterpriseServices;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VirtualFrends.Models;

namespace VirtualFrends.Controllers
{
    [Authorize]
    public class DiarioController : Controller
    {
        AdminTerceros adm = new AdminTerceros();
        // GET: Diario
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Transferir()
        {
            AdminTerceros Admin = new AdminTerceros();
            var saldo = Admin.SaldoDiario();
            Transferencia tr = new Transferencia
            {
                Saldo = saldo
            };
            return View(tr);
        }
        public ActionResult AddTransferencia(Transferencia Modelo)
        {
            AdminDiario ADM = new AdminDiario();
            if (ADM.ValidacionEmail(Modelo.EmailBeneficario) == false)
            {
                Modelo.Saldo = ADM.SaldoDiario();
                ViewBag.Error = "Correo no Existe";
                return View("Transferir", Modelo);
            }
            else
                ADM.AddDiario(Modelo);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SolComprar()
        {
            return View();
        }
        public ActionResult ComprarMoneda(string entidad)
        {
          
            TempData["Entidad"] = entidad;
            
            Compras tr = new Compras
            {
                Entidad = entidad
            };
            return View(tr);
        }

        [HttpPost]
        //public ActionResult AddMoneda(Compras model)
        //{
        //    HttpPostedFileBase file = Request.Files[0];
        //    //WebImage web = new WebImage(file.InputStream);

        //    //model.Soporte = web.GetBytes();
        //    string folder = "1S3Gt9WGAlpKmKVO4ybwkKElWZM0tK3s_";
        //    model.Soporte= GoogleDriveFilesRepository.FileUploadInFolder(folder,file);
        //    model.Entidad = TempData["Entidad"].ToString();
        //    model.Email = adm.EmailLogeado();
        //    model.Fecha = DateTime.Now;
        //    model.Estado = 1;
        //    try
        //    {
        //        using (Contexto Context = new Contexto())
        //        {
        //            Context.Compras.Add(model);
        //            Context.SaveChanges();
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //        return View("ComprarMoneda", model);
        //    }
            
        //    return RedirectToAction("Index", "Home");
        //}


        public ActionResult AddMoneda(Compras model)
        {
            HttpPostedFileBase file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                // Extraemos el contenido en Bytes del archivo subido.
                var _contenido = new byte[file.ContentLength];
                file.InputStream.Read(_contenido, 0, file.ContentLength);

                // Separamos el Nombre del archivo de la Extensión.
                int indiceDelUltimoPunto = file.FileName.LastIndexOf('.');
                string _nombre = file.FileName.Substring(0, indiceDelUltimoPunto);
                string _extension = file.FileName.Substring(indiceDelUltimoPunto + 1,
                                    file.FileName.Length - indiceDelUltimoPunto - 1);

                // Instanciamos la clase Archivo y asignammos los valores.
                Archivo _archivo = new Archivo()
                {
                    Nombre = _nombre,
                    Extension = _extension,
                    Descargas = 0
                };
                
             
                try
                {
                    // Subimos el archivo al Servidor.
                    _archivo.SubirArchivo(_contenido);
                    model.Entidad = TempData["Entidad"].ToString();
                    model.Email = adm.EmailLogeado();
                    model.Fecha = DateTime.Now;
                    model.Estado = 1;
                    model.Soporte = _archivo.Id;
                    // Guardamos en la base de datos la instancia del archivo
                    using (Contexto _dbContext = new Contexto())
                    {
                        _dbContext.Archivos.Add(_archivo);
                        _dbContext.Compras.Add(model);
                        _dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View("ComprarMoneda", model);
                }
            }

            // Redirigimos a la Acción 'Index' para mostrar
            // Los archivos subidos al Servidor.
            return RedirectToAction("Index", "Home");
        }



        public ActionResult SolRetiro( )
        {
            AdminDiario Admin = new AdminDiario();
            ViewBag.Saldo = Admin.SaldoDiario();
            Contexto db = new Contexto();
            ViewBag.Entidad = new SelectList(db.Bancos,"Id","Nombre");
            ViewBag.TipoCuenta = new SelectList(db.TipoCuenta, "Id", "Tipo");
            return View();
        }

        public ActionResult AddRetiro(Ventas model)
        {
             
            model.Email = adm.EmailLogeado();
            model.Fecha = DateTime.Now;
            model.Estado = 1;
            try
            {
                using (Contexto Context = new Contexto())
                {
                    Context.Ventas.Add(model);
                    Context.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                AdminDiario Admin = new AdminDiario();
                ViewBag.Saldo = Admin.SaldoDiario();
                Contexto db = new Contexto();
                ViewBag.Entidad = new SelectList(db.Bancos, "Id", "Nombre");
                ViewBag.TipoCuenta = new SelectList(db.TipoCuenta, "Id", "Tipo");
                ViewBag.Error = ex.Message;
                return View("SolRetiro", model);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}