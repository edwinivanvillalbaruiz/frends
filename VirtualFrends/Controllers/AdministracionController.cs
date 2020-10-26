using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualFrends.Models;




namespace VirtualFrends.Controllers
{

    [Authorize(Roles ="Administrador")]
    public class AdministracionController : Controller
    {
        AdminTerceros adm = new AdminTerceros();
        // GET: Administracion
        public ActionResult Abonar(int Id)
        {
            Contexto adm = new Contexto();
            Compras compras = adm.Compras.Find(Id);             
            return View(compras);
        }

        public ActionResult AddAbono(Diario model)
        {
            return View();
        }
        public void Imagen(string Id)
        {
            string FilePath = GoogleDriveFilesRepository.DownloadGoogleFile(Id);

            Response.ContentType = "application/zip";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FilePath));
            Response.WriteFile(System.Web.HttpContext.Current.Server.MapPath("~/GoogleDriveFiles/" + Path.GetFileName(FilePath)));
            Response.End();
            Response.Flush();
           
        }


        public ActionResult google()
        {
            return View(GoogleDriveFilesRepository.GetDriveFiles());
        }

        public ActionResult ComprasPendientes()
        {
            List<Compras> lstcompras = new List<Compras>();           
            using (Contexto Context = new Contexto())
            {
                lstcompras = Context.Compras.AsNoTracking().ToList().Where(m => m.Estado == 1).ToList();         
            }             
            return View((lstcompras));
        }
        public ActionResult VentasPendientes()
        {
            List<Ventas> lstcompras = new List<Ventas>();
            using (Contexto Context = new Contexto())
            {
                lstcompras = Context.Ventas.AsNoTracking().ToList().Where(m => m.Estado == 1).ToList();
            }
            return View((lstcompras));
        }
        //public void DownloadFile(string id)
        //{
        //    string FilePath = GoogleDriveFilesRepository.DownloadGoogleFile(id);

        //    Response.ContentType = "application/zip";
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FilePath));
        //    Response.WriteFile(System.Web.HttpContext.Current.Server.MapPath("~/GoogleDriveFiles/" + Path.GetFileName(FilePath)));
        //    Response.End();
        //    Response.Flush();
        //}

        [HttpGet]
        public ActionResult DownloadFile(Guid id)
        {
            Archivo _archivo;
            FileContentResult _fileContent;

            using (Contexto _dbContext = new Contexto())
            {
                _archivo = _dbContext.Archivos.FirstOrDefault(x => x.Id == id);
            }

            if (_archivo == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    // Descargamos el archivo del Servidor.
                    _fileContent = new FileContentResult(_archivo.DescargarArchivo(),
                                                         "application/octet-stream");
                    _fileContent.FileDownloadName = _archivo.Nombre + "." + _archivo.Extension;

                    // Actualizamos el nº de descargas en la base de datos.
                    using (Contexto _dbContext = new Contexto())
                    {
                        _archivo.Descargas++;
                        _dbContext.Archivos.Attach(_archivo);
                        _dbContext.Entry(_archivo).State = EntityState.Modified;
                        _dbContext.SaveChanges();
                    }

                    return _fileContent;
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
        }





    }


















}
