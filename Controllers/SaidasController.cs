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
    public class SaidasController : Controller
    {
        private Contexto db = new Contexto();

        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, int? Page)
        {
            var sAIDA = db.SAIDA.Include(s => s.TIPO_SAIDA);


            ViewBag.CurrentSort = sortOrder;


            ViewBag.TipoParam = string.IsNullOrEmpty(sortOrder) ? "Tipo" : "";
            ViewBag.DateParm = sortOrder == "Date" ? "Data" : "Date";





            var saidas = from s in db.SAIDA
                           select s;




            switch (sortOrder)
            {

                case "Tipo":

                    saidas = saidas.OrderBy(s => s.id_tipo_saida.ToString());

                    break;

                case "Data":

                    saidas = saidas.OrderByDescending(s => s.data_saida);

                    break;

                default:
                    saidas = saidas.OrderBy(s => s.data_saida);
                    break;

            }
            int pageSize = 5;
            int pageNumber = (Page ?? 1);



            return View(saidas.ToPagedList(pageNumber, pageSize));





            
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAIDA sAIDA = db.SAIDA.Find(id);
            if (sAIDA == null)
            {
                return HttpNotFound();
            }
            return View(sAIDA);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.id_tipo_saida = new SelectList(db.TIPO_SAIDA, "ID_TIPO_SAIDA", "TIPO");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_saida,id_tipo_saida,valor,data_saida,observacao")] SAIDA sAIDA)
        {
            if (ModelState.IsValid)
            {
                db.SAIDA.Add(sAIDA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_tipo_saida = new SelectList(db.TIPO_SAIDA, "ID_TIPO_SAIDA", "TIPO", sAIDA.id_tipo_saida);
            return View(sAIDA);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAIDA sAIDA = db.SAIDA.Find(id);
            if (sAIDA == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_tipo_saida = new SelectList(db.TIPO_SAIDA, "ID_TIPO_SAIDA", "TIPO", sAIDA.id_tipo_saida);
            return View(sAIDA);
        }

         [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_saida,id_tipo_saida,valor,data_saida,observacao")] SAIDA sAIDA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sAIDA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_tipo_saida = new SelectList(db.TIPO_SAIDA, "ID_TIPO_SAIDA", "TIPO", sAIDA.id_tipo_saida);
            return View(sAIDA);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAIDA sAIDA = db.SAIDA.Find(id);
            if (sAIDA == null)
            {
                return HttpNotFound();
            }
            return View(sAIDA);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SAIDA sAIDA = db.SAIDA.Find(id);
            db.SAIDA.Remove(sAIDA);
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
