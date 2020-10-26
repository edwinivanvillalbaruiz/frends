using DevExtreme.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualFrends.Models;

namespace VirtualFrends
{
    public class AdminDiario
    {

        public void AddDiario(Transferencia modelo)
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser userusu = userManager.FindById(user);
            using (Contexto Context = new Contexto())
            {

                Diario diario = new Diario
                {
                    IdTransaccion=1,
                    Fecha=DateTime.Now,
                    IdOperacion=1,
                    Debito=modelo.Cantidad,
                    Credito=0,
                    Concepto=modelo.Concepto,
                    Estado=1,
                    EmailOperacion=modelo.EmailBeneficario,
                    Email= userusu.Email
                };

                Context.Diario.Add(diario);

                Diario diarioC = new Diario
                {
                    IdTransaccion = 1,
                    Fecha = DateTime.Now,
                    IdOperacion = 1,
                    Debito = 0,
                    Credito = modelo.Cantidad,
                    Concepto = modelo.Concepto,
                    Estado = 1,
                    EmailOperacion = userusu.Email ,
                    Email = modelo.EmailBeneficario
                };
                Context.Diario.Add(diarioC);
                Context.SaveChanges();
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
        public decimal SaldoDiario()
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
    }
}