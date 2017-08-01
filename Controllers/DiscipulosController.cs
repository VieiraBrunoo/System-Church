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
    public class DiscipulosController : Controller
    {
        private Contexto db = new Contexto();

        [Authorize]
        public ActionResult Index(string sortOrder, string Pesquisa,string currentFilter , int? Page)
        {

            var dISCIPULO = db.DISCIPULO.Include(d => d.DISCIPULO2).Include(d => d.ESCOLA_LIDERES1).Include(d => d.FEZ_BATISMO).Include(d => d.FEZ_ENCONTRO).Include(d => d.SEXO1);

            ViewBag.CurrentSort = sortOrder;

            ViewBag.DiscipuladorParam = string.IsNullOrEmpty(sortOrder) ? "Discipulador" : "";


            if (Pesquisa != null)
            {
                Page = 1;
            }
            else
            {

                Pesquisa = currentFilter;
            }

            ViewBag.CurrentFilter = Pesquisa;

            var discipulos = from s in db.DISCIPULO
                             select s;


            if (!String.IsNullOrEmpty(Pesquisa))
            {

                discipulos = discipulos.Where(s => s.NOME.ToUpper().Contains(Pesquisa.ToUpper()));
            }

            switch (sortOrder)
            {
             
                case "Discipulador":

                    discipulos = discipulos.OrderBy(s => s.DISCIPULO2.NOME);

                    break;

                default:
                    discipulos = discipulos.OrderBy(s => s.NOME);
                    break;
                   
                             }
            int pageSize = 5;
            int pageNumber= (Page ?? 1); 

           return View(discipulos.ToPagedList(pageNumber,pageSize));
        }
        [Authorize]
        public ActionResult Total()
        {


           string valor = db.DISCIPULO.Count().ToString();

            int Quantidade = Convert.ToInt16(valor);

            return Content(" Quantidade Total de Discipulos " + Quantidade, "text/plain");
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DISCIPULO dISCIPULO = db.DISCIPULO.Find(id);
            if (dISCIPULO == null)
            {
                return HttpNotFound();
            }
            return View(dISCIPULO);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.DISCIPULADOR = new SelectList(db.DISCIPULO, "ID_DISCIPULO", "NOME");
            ViewBag.ESCOLA_LIDERES = new SelectList(db.ESCOLA_LIDERES, "ID_ESCOLA_LIDERES", "OPCAO");
            ViewBag.BATISMO = new SelectList(db.FEZ_BATISMO, "ID_BATISMO", "OPCAO");
            ViewBag.ENCONTRO = new SelectList(db.FEZ_ENCONTRO, "ID_FEZ_ENCONTRO", "OPCAO");
            ViewBag.SEXO = new SelectList(db.SEXO, "ID_SEXO", "NOME");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DISCIPULO,NOME,IDADE,DATA_NASC,EMAIL,ENDERECO,TELEFONE,SEXO,ESCOLA_LIDERES,BATISMO,ENCONTRO,DISCIPULADOR")] DISCIPULO dISCIPULO)
        {
            if (ModelState.IsValid)
            {
                db.DISCIPULO.Add(dISCIPULO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DISCIPULADOR = new SelectList(db.DISCIPULO, "ID_DISCIPULO", "NOME", dISCIPULO.DISCIPULADOR);
            ViewBag.ESCOLA_LIDERES = new SelectList(db.ESCOLA_LIDERES, "ID_ESCOLA_LIDERES", "OPCAO", dISCIPULO.ESCOLA_LIDERES);
            ViewBag.BATISMO = new SelectList(db.FEZ_BATISMO, "ID_BATISMO", "OPCAO", dISCIPULO.BATISMO);
            ViewBag.ENCONTRO = new SelectList(db.FEZ_ENCONTRO, "ID_FEZ_ENCONTRO", "OPCAO", dISCIPULO.ENCONTRO);
            ViewBag.SEXO = new SelectList(db.SEXO, "ID_SEXO", "NOME", dISCIPULO.SEXO);
            return View(dISCIPULO);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DISCIPULO dISCIPULO = db.DISCIPULO.Find(id);
            if (dISCIPULO == null)
            {
                return HttpNotFound();
            }
            ViewBag.DISCIPULADOR = new SelectList(db.DISCIPULO, "ID_DISCIPULO", "NOME", dISCIPULO.DISCIPULADOR);
            ViewBag.ESCOLA_LIDERES = new SelectList(db.ESCOLA_LIDERES, "ID_ESCOLA_LIDERES", "OPCAO", dISCIPULO.ESCOLA_LIDERES);
            ViewBag.BATISMO = new SelectList(db.FEZ_BATISMO, "ID_BATISMO", "OPCAO", dISCIPULO.BATISMO);
            ViewBag.ENCONTRO = new SelectList(db.FEZ_ENCONTRO, "ID_FEZ_ENCONTRO", "OPCAO", dISCIPULO.ENCONTRO);
            ViewBag.SEXO = new SelectList(db.SEXO, "ID_SEXO", "NOME", dISCIPULO.SEXO);
            return View(dISCIPULO);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DISCIPULO,NOME,IDADE,DATA_NASC,EMAIL,ENDERECO,TELEFONE,SEXO,ESCOLA_LIDERES,BATISMO,ENCONTRO,DISCIPULADOR")] DISCIPULO dISCIPULO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dISCIPULO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DISCIPULADOR = new SelectList(db.DISCIPULO, "ID_DISCIPULO", "NOME", dISCIPULO.DISCIPULADOR);
            ViewBag.ESCOLA_LIDERES = new SelectList(db.ESCOLA_LIDERES, "ID_ESCOLA_LIDERES", "OPCAO", dISCIPULO.ESCOLA_LIDERES);
            ViewBag.BATISMO = new SelectList(db.FEZ_BATISMO, "ID_BATISMO", "OPCAO", dISCIPULO.BATISMO);
            ViewBag.ENCONTRO = new SelectList(db.FEZ_ENCONTRO, "ID_FEZ_ENCONTRO", "OPCAO", dISCIPULO.ENCONTRO);
            ViewBag.SEXO = new SelectList(db.SEXO, "ID_SEXO", "NOME", dISCIPULO.SEXO);
            return View(dISCIPULO);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DISCIPULO dISCIPULO = db.DISCIPULO.Find(id);
            if (dISCIPULO == null)
            {
                return HttpNotFound();
            }
            return View(dISCIPULO);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DISCIPULO dISCIPULO = db.DISCIPULO.Find(id);
            db.DISCIPULO.Remove(dISCIPULO);
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
