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
using Rotativa;
using Rotativa.Options;

namespace SistemaIgreja.Controllers
{
    public class CelulasController : Controller
    {
        private Contexto db = new Contexto();
         [Authorize]
        public ActionResult Index(string sortOrder, string Pesquisa, string currentFilter, int? Page, Boolean? gerarPDF)
        {
            var cELULA = db.CELULA.Include(c => c.DIA_SEMANA).Include(c => c.DISCIPULO).Include(c => c.HORARIO_CELULA);

            ViewBag.CurrentSort = sortOrder;


            ViewBag.DiaParam = string.IsNullOrEmpty(sortOrder) ? "Dia da Semana" : "";
            ViewBag.BairroParam = string.IsNullOrEmpty(sortOrder) ? "Bairro" : "";

            if (Pesquisa != null)
            {
                Page = 1;
            }
            else
            {

                Pesquisa = currentFilter;
            }

            ViewBag.CurrentFilter = Pesquisa;

            var celulas = from s in db.CELULA
                             select s;


            if (!String.IsNullOrEmpty(Pesquisa))
            {

                celulas = celulas.Where(s => s.DISCIPULO.NOME.ToString().ToUpper().Contains(Pesquisa.ToUpper()));
            }

            switch (sortOrder)
            {

                case "Dia da Semana":

                    celulas = celulas.OrderBy(s => s.DIA_SEMANA.NOME_DIA);

                    break;

                case "Bairro":

                    celulas = celulas.OrderBy(s => s.LUGAR);

                    break;

                default:
                    celulas = celulas.OrderBy(s => s.ID_DISCIPULO.ToString());
                    break;

            }
            int pageSize = 5;
            int pageNumber = (Page ?? 1);

            if (gerarPDF == true)
            {

                var pdf = new ViewAsPdf
                {
                    ViewName = "Pdf",
                    PageSize = Size.A4,
                    IsGrayScale = true,

                };
                    return pdf;
                }


                return View(celulas.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult Total()
        {


            string valor = db.CELULA.Count().ToString();

            int Quantidade = Convert.ToInt16(valor);

            return Content(" Quantidade Total de Células " + Quantidade, "text/plain");
        }


        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CELULA cELULA = db.CELULA.Find(id);
            if (cELULA == null)
            {
                return HttpNotFound();
            }
            return View(cELULA);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.DIA = new SelectList(db.DIA_SEMANA, "ID_DIA", "NOME_DIA");
            ViewBag.ID_DISCIPULO = new SelectList(db.DISCIPULO, "ID_DISCIPULO", "NOME");
            ViewBag.HORARIO = new SelectList(db.HORARIO_CELULA, "ID_HORARIO", "HORARIO");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CELULA,ID_DISCIPULO,LUGAR,DIA,HORARIO")] CELULA cELULA)
        {
            if (ModelState.IsValid)
            {
                db.CELULA.Add(cELULA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DIA = new SelectList(db.DIA_SEMANA, "ID_DIA", "NOME_DIA", cELULA.DIA);
            ViewBag.ID_DISCIPULO = new SelectList(db.DISCIPULO, "ID_DISCIPULO", "NOME", cELULA.ID_DISCIPULO);
            ViewBag.HORARIO = new SelectList(db.HORARIO_CELULA, "ID_HORARIO", "HORARIO", cELULA.HORARIO);
            return View(cELULA);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CELULA cELULA = db.CELULA.Find(id);
            if (cELULA == null)
            {
                return HttpNotFound();
            }
            ViewBag.DIA = new SelectList(db.DIA_SEMANA, "ID_DIA", "NOME_DIA", cELULA.DIA);
            ViewBag.ID_DISCIPULO = new SelectList(db.DISCIPULO, "ID_DISCIPULO", "NOME", cELULA.ID_DISCIPULO);
            ViewBag.HORARIO = new SelectList(db.HORARIO_CELULA, "ID_HORARIO", "HORARIO", cELULA.HORARIO);
            return View(cELULA);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CELULA,ID_DISCIPULO,LUGAR,DIA,HORARIO")] CELULA cELULA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cELULA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DIA = new SelectList(db.DIA_SEMANA, "ID_DIA", "NOME_DIA", cELULA.DIA);
            ViewBag.ID_DISCIPULO = new SelectList(db.DISCIPULO, "ID_DISCIPULO", "NOME", cELULA.ID_DISCIPULO);
            ViewBag.HORARIO = new SelectList(db.HORARIO_CELULA, "ID_HORARIO", "HORARIO", cELULA.HORARIO);
            return View(cELULA);
        }
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CELULA cELULA = db.CELULA.Find(id);
            if (cELULA == null)
            {
                return HttpNotFound();
            }
            return View(cELULA);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CELULA cELULA = db.CELULA.Find(id);
            db.CELULA.Remove(cELULA);
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
