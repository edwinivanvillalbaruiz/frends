using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualFrends.Models;
namespace VirtualFrends
{
    public class AdminTerceros
    {
        public SelectList MiListaTipoDocumento;
        public AdminTerceros()
        {
            using (Contexto Context = new Contexto())
            {
                MiListaTipoDocumento = new SelectList(Context.TipoDocumento, "IdTipoDocumento", "Nombre");
            }
        }
        public IEnumerable<Terceros> Consultar()
        {
            using (Contexto Context = new Contexto())
            {
                return Context.Terceros.AsNoTracking().ToList();
            }
        }
        public Terceros Consultar(int id)
        {
            using (Contexto Context = new Contexto())
            {
                return Context.Terceros.AsNoTracking().FirstOrDefault(t => t.IdTerceros == id);
            }
        }
        public void Guardar(Terceros modelo)
        {
            using (Contexto Context = new Contexto())
            {
                Context.Terceros.Add(modelo);
                Context.SaveChanges();
            }
        }
        public void Modificar(Terceros modelo)
        {
            var mitercero = Consultar(modelo.IdTerceros);
            modelo.Fecha = mitercero.Fecha;
            modelo.IdTipoDocumento = mitercero.IdTipoDocumento;
            modelo.Email = mitercero.Email;
            modelo.IdTransaccion = mitercero.IdTransaccion;
            using (Contexto Context = new Contexto())
            {
                Context.Entry(modelo).State = EntityState.Modified;
                Context.SaveChanges();
            }
        }
        public void Eliminar(Terceros modelo)
        {
            using (Contexto Context = new Contexto())
            {
                Context.Entry(modelo).State = EntityState.Deleted;
                Context.SaveChanges();
            }
        }
        public Terceros Perfil()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser userusu = userManager.FindById(user);

            using (Contexto Context = new Contexto())
            {
                return Context.Terceros.AsNoTracking().FirstOrDefault(t => t.Email == userusu.Email);
            }
        }

        public string EmailLogeado()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser userusu = userManager.FindById(user);
            return userusu.Email;
        }


        public IEnumerable<ViewDiario> MiViewDiario()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser userusu = userManager.FindById(user);
            using (Contexto Context = new Contexto())
            {
                return 
                    (Context.Database.SqlQuery<ViewDiario>(Resource1.ViewDiario,
                    new MySqlParameter("VarId", (userusu.Email))
                    ).ToList());
            }
        }
        public decimal  SaldoDiario()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser userusu = userManager.FindById(user);
            using (Contexto Context = new Contexto())
            {
                List<ViewDiario> DTS = Context.Database.SqlQuery<ViewDiario>(Resource1.SaldoDiario,
                         new MySqlParameter("VarId", (userusu.Email))).ToList();
                return DTS[0].Valor;
            }
        }

        public IEnumerable<TercerosNive> Mimatrix()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser userusu = userManager.FindById(user);
            using (Contexto Context = new Contexto())
            {
                return Matrix(
                    (Context.Database.SqlQuery<TercerosNive>(Resource1.Matrixgeneral,
                    new MySqlParameter("VarId", (userusu.Email))
                    ).ToList()));
            }
        }
        public bool ValidacionEmail(string StrEmail)
        {
            using (Contexto Context = new Contexto())
            {
                Terceros terceros = Context.Terceros.AsNoTracking().FirstOrDefault(t => t.Email == StrEmail);
                if (terceros == null)
                    return false;
                else
                    return true;
            }
        }

        public bool   ValidarRegistros(string StrEmail)
        {            
            using (Contexto Context = new Contexto())
            {           
               var lista=    Context.Database.SqlQuery<Referidos>("SELECT * from Referidos where CorreoAnfitrion = @VarId",
                    new MySqlParameter("VarId", StrEmail)
                    ).ToList();
                if (lista.Count < 2)
                    return true;
                else
                    return false;                       
            }
        }
        private List<TercerosNive> Matrix(List<TercerosNive> MiLista)
        {
            int cuenta = 1;
            int cuenta2 = 1;
            int cuenta3 = 1;
            List<TercerosNive> MiLista1 = new List<TercerosNive>();
            foreach (var item in MiLista)
            {
                if (item.Nivel == "Nivel1" & cuenta <= 2)
                {
                    TercerosNive terceros = new TercerosNive
                    {
                        IdTransaccion = item.IdTransaccion,
                        IdTerceros = item.IdTerceros,
                        Nombres = item.Nombres,
                        Apellidos = item.Apellidos,
                        NombreC = item.NombreC,
                        Fecha = item.Fecha,
                        Documento = item.Documento,
                        IdTipoDocumento = item.IdTipoDocumento,
                        Email = item.Email,
                        Celular = item.Celular,
                        Cont = cuenta,
                        Nivel = item.Nivel
                       
                        
                    };
                    MiLista1.Add(terceros);
                    cuenta = cuenta + 1;
                }
                else
                    if (item.Nivel == "Nivel2" & cuenta2 <= 4)
                {
                    TercerosNive terceros = new TercerosNive
                    {
                        IdTransaccion = item.IdTransaccion,
                        IdTerceros = item.IdTerceros,
                        Nombres = item.Nombres,
                        Apellidos = item.Apellidos,
                        Fecha = item.Fecha,
                        Documento = item.Documento,
                        IdTipoDocumento = item.IdTipoDocumento,
                        Email = item.Email,
                        Celular = item.Celular,
                        Cont = cuenta2,
                        Nivel = item.Nivel,
                        NombreC = item.NombreC
                    };
                    MiLista1.Add(terceros);
                    cuenta2 = cuenta2 + 1;
                }
                else

                if (item.Nivel == "Nivel3" & cuenta3 <= 8)
                {
                    TercerosNive terceros = new TercerosNive
                    {
                        IdTransaccion = item.IdTransaccion,
                        IdTerceros = item.IdTerceros,
                        Nombres = item.Nombres,
                        Apellidos = item.Apellidos,
                        Fecha = item.Fecha,
                        Documento = item.Documento,
                        IdTipoDocumento = item.IdTipoDocumento,
                        Email = item.Email,
                        Celular = item.Celular,
                        Cont = cuenta3,
                        Nivel = item.Nivel,
                        NombreC = item.NombreC
                    };
                    MiLista1.Add(terceros);
                    cuenta3 = cuenta3 + 1;
                }
            }

            return MiLista1;

        }
        public Terceros MiMatrixLider()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser userusu = userManager.FindById(user);
            Terceros tercero = new Terceros();
            using (Contexto Context = new Contexto())
            {
                var modelo = (Context.Database.SqlQuery<Terceros>(Resource1.MiMatrix,
                    new MySqlParameter("VarId", (userusu.Email))
                    ).ToList());

                if (modelo.Count == 0)
                {
                    Terceros terceros = new Terceros
                    {
                        IdTransaccion = 0,
                        IdTerceros = 0,
                        Nombres = "Administracion",
                        Apellidos = "Administracion",
                        Fecha = DateTime.Now,
                        Documento = "Admin",
                        IdTipoDocumento = 0,
                        Email = "Administracion@admin.com",
                        Celular = "3333333"

                    };
                    tercero = terceros;
                }
                else
                {
                    Terceros terceros = new Terceros
                    {
                        IdTransaccion = modelo[0].IdTransaccion,
                        IdTerceros = modelo[0].IdTerceros,
                        Nombres = modelo[0].Nombres,
                        Apellidos = modelo[0].Apellidos,
                        Fecha = modelo[0].Fecha,
                        Documento = modelo[0].Documento,
                        IdTipoDocumento = modelo[0].IdTipoDocumento,
                        Email = modelo[0].Email,
                        Celular = modelo[0].Celular
                    };
                    tercero = terceros;
                }
            }
            return tercero;


        }

        public void AddCompras(Compras modelo)
        {
            using (Contexto Context = new Contexto())
            {
                Context.Compras.Add(modelo);
                Context.SaveChanges();
            }
        }



        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (System.IO.MemoryStream mStream = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(mStream);

            }
        }

        //public List<MisCompras> lstCompras(List<Compras> MiLista)
        //{
        //    List<MisCompras> RsList = new List<MisCompras>();
        //    foreach (var item in MiLista)
        //    {
        //        MisCompras compra = new MisCompras
        //        {
        //            Concepto = item.Concepto,
        //            Email = item.Email,
        //            Valor = item.Valor,
        //            Id = item.Id,
        //            Entidad = item.Entidad,
        //            Estado = item.Estado,
        //            Fecha = item.Fecha,
        //            Soporte = item.Soporte,
        //            MiImagen = byteArrayToImage(item.Soporte)

        //        };
        //        RsList.Add(compra);

        //    }
        //    return RsList;
        //}

    }
}