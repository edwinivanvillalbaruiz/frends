using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualFrends.Controllers
{
    public class ArchivosController : Controller
    {
         Contexto _dbContext;

        [HttpGet]
        public ActionResult Index()
        {
            List<Archivo> _archivos = new List<Archivo>();
            using (_dbContext = new Contexto())
            {
                // Recuperamos la Lista de los archivos subidos.
                _archivos = _dbContext.Archivos.OrderBy(x => x.Creado).ToList();
            }
            // Retornamos la Vista Index, con los archivos subidos.
            return View(_archivos);
        }

        [HttpPost]
        public ActionResult SubirArchivo(HttpPostedFileBase file)
        {
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
                    // Guardamos en la base de datos la instancia del archivo
                    using (_dbContext = new Contexto())
                    {
                        _dbContext.Archivos.Add(_archivo);
                        _dbContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    // Aquí el código para manejar la Excepción.
                }                
            }

            // Redirigimos a la Acción 'Index' para mostrar
            // Los archivos subidos al Servidor.
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DescargarArchivo(Guid id)
        {
            Archivo _archivo;
            FileContentResult _fileContent;

            using (_dbContext = new Contexto())
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
                    using (_dbContext = new Contexto())
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

        [HttpGet]
        public ActionResult EliminarArchivo(Guid id)
        {
            Archivo _archivo;

            using (_dbContext = new Contexto())
            {
                _archivo = _dbContext.Archivos.FirstOrDefault(x => x.Id == id);
            }

            if (_archivo != null)
            {
                using (_dbContext = new Contexto())
                {
                    _archivo = _dbContext.Archivos.FirstOrDefault(x => x.Id == id);
                    _dbContext.Archivos.Remove(_archivo);
                    if (_dbContext.SaveChanges() > 0)
                    {
                        // Eliminamos el archivo del Servidor.
                        _archivo.EliminarArchivo();
                    }
                }
                // Redirigimos a la Acción 'Index' para mostrar
                // Los archivos subidos al Servidor.
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }                        
        }
    }
}