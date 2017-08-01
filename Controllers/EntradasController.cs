using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaIgreja.Models;
using PagedList;

namespace SistemaIgreja.Controllers


{   
    public class EntradasController : Controller
    {
        private Contexto db = new Contexto();

        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, int? Page)
        {
            var eNTRADA = db.ENTRADA.Include(e => e.TIPO_ENTRADA);

            ViewBag.CurrentSort = sortOrder;


            ViewBag.TipoParam = string.IsNullOrEmpty(sortOrder) ? "Tipo" : "";
            ViewBag.DateParm = sortOrder == "Date" ? "Data" : "Date";

           
            
          

            var entradas = from s in db.ENTRADA
                          select s;


            

            switch (sortOrder)
            {

                case "Tipo":

                    entradas = entradas.OrderBy(s => s.cod_tipo_entrada.ToString());

                    break;

                case "Data":

                    entradas = entradas.OrderByDescending(s => s.data_entrada);

                    break;

                default:
                    entradas = entradas.OrderBy(s => s.data_entrada);
                    break;

            }
            int pageSize = 5;
            int pageNumber = (Page ?? 1);



            return View(entradas.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult SaldoCaixa()
        {

            string valor = db.ENTRADA.Sum(c => c.valor).ToString();

            string valor2 = db.SAIDA.Sum(c => c.valor).ToString();

            decimal valorCredito = Convert.ToDecimal(valor);

            decimal valorDebito = Convert.ToDecimal(valor2);

            decimal valorTotal = (valorCredito - valorDebito);

            return Content(" R$ " + valorTotal, "text/plain");

        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENTRADA eNTRADA = db.ENTRADA.Find(id);
            if (eNTRADA == null)
            {
                return HttpNotFound();
            }
            return View(eNTRADA);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.cod_tipo_entrada = new SelectList(db.TIPO_ENTRADA, "ID_TIPO_ENTRADA", "TIPO");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_entrada,cod_tipo_entrada,valor,data_entrada,observao")] ENTRADA eNTRADA)
        {
            if (ModelState.IsValid)
            {
                db.ENTRADA.Add(eNTRADA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cod_tipo_entrada = new SelectList(db.TIPO_ENTRADA, "ID_TIPO_ENTRADA", "TIPO", eNTRADA.cod_tipo_entrada);
            return View(eNTRADA);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENTRADA eNTRADA = db.ENTRADA.Find(id);
            if (eNTRADA == null)
            {
                return HttpNotFound();
            }
            ViewBag.cod_tipo_entrada = new SelectList(db.TIPO_ENTRADA, "ID_TIPO_ENTRADA", "TIPO", eNTRADA.cod_tipo_entrada);
            return View(eNTRADA);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_entrada,cod_tipo_entrada,valor,data_entrada,observao")] ENTRADA eNTRADA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eNTRADA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cod_tipo_entrada = new SelectList(db.TIPO_ENTRADA, "ID_TIPO_ENTRADA", "TIPO", eNTRADA.cod_tipo_entrada);
            return View(eNTRADA);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENTRADA eNTRADA = db.ENTRADA.Find(id);
            if (eNTRADA == null)
            {
                return HttpNotFound();
            }
            return View(eNTRADA);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ENTRADA eNTRADA = db.ENTRADA.Find(id);
            db.ENTRADA.Remove(eNTRADA);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
