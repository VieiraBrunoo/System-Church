using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Rotativa;
using Rotativa.Options;
using SistemaIgreja.Models;

namespace Sistema.Controllers
{
    public class RelatoriosController : Controller
    {
        private Contexto db = new Contexto();


        [Authorize]
        public ActionResult FormVendasPorPedido()
{
   return View();
}
        [Authorize]
        public ActionResult RelaDoze()
        {
            return View();
        }



        [Authorize]
        public ActionResult SaidaPorPeriodo()
        {


            return View();
        }

        [Authorize]
        public ActionResult Celula()
        {
            return View();
        }


        [Authorize]
        public ActionResult Pdf(int? pagina, Boolean? gerarPDF,string Pesquisa)

        {
            ViewBag.Pesquisa = Pesquisa;





            var listagemClientes = db.CELULA.Where(i=> i.DISCIPULO.NOME.Equals(Pesquisa ) || i.DIA_SEMANA.NOME_DIA.Equals(Pesquisa)|| i.LUGAR.Equals(Pesquisa)).OrderBy(n => n.DISCIPULO.NOME).ToList<CELULA>();

            if (gerarPDF != true)
            {
                //Definindo a paginação              

                int paginaQdteRegistros = 100;
                int paginaNumeroNavegacao = (pagina ?? 1);

                return View(listagemClientes.ToPagedList(paginaNumeroNavegacao, paginaQdteRegistros));
            }
            else
            {
                int paginaNumero = 1;
                var pdf = new ViewAsPdf
                {
                    ViewName = "Pdf",
                    PageSize = Size.A4,
                    IsGrayScale = true,
                    Model = listagemClientes.ToPagedList(paginaNumero, listagemClientes.Count)
                };
                return pdf;


            }
        }

        [Authorize]
        public ActionResult RelVendasPorPeriodo(DateTime dataInicial, DateTime dataFinal, int? pagina, Boolean? gerarPDF)
        {
            var vendas = db.ENTRADA.Where(i => i.data_entrada >= dataInicial && i.data_entrada <= dataFinal).OrderBy(p => p.cod_tipo_entrada).ToList<ENTRADA>();

            ViewBag.dataInicial = dataInicial;
            ViewBag.dataFinal = dataFinal;

            if (gerarPDF != true)
            {
                int paginaQdteRegistros = 100;
                int paginaNumeroNavegacao = (pagina ?? 1);

                return View(vendas.ToPagedList(paginaNumeroNavegacao, paginaQdteRegistros));
            }
            else
            {
                int paginaNumero = 1;

                var pdf = new ViewAsPdf
                {
                    ViewName = "RelVendasPorPeriodo",
                    PageSize = Size.A4,
                    IsGrayScale = true,
                    Model = vendas.ToPagedList(paginaNumero, vendas.Count),
                };
                return pdf;
            }
        }

        [Authorize]
        public ActionResult SaidaPeriodo(DateTime dataInicial, DateTime dataFinal, int? pagina, Boolean? gerarPDF)
        {
            var saidas = db.SAIDA.Where(i => i.data_saida >= dataInicial && i.data_saida <= dataFinal).OrderBy(p => p.id_tipo_saida).ToList<SAIDA>();

            ViewBag.dataInicial = dataInicial;
            ViewBag.dataFinal = dataFinal;

            if (gerarPDF != true)
            {
                int paginaQdteRegistros = 100;
                int paginaNumeroNavegacao = (pagina ?? 1);

                return View(saidas.ToPagedList(paginaNumeroNavegacao, paginaQdteRegistros));
            }
            else
            {
                int paginaNumero = 1;

                var pdf = new ViewAsPdf
                {
                    ViewName = "SaidaPeriodo",
                    PageSize = Size.A4,
                    IsGrayScale = true,
                    Model = saidas.ToPagedList(paginaNumero, saidas.Count()),
                };
                return pdf;
            }
        }
        [Authorize]
        public ActionResult DiscEquipe(string Nome, int? pagina, Boolean? gerarPDF)
        {



            var discipulos = db.DISCIPULO.Where(i => i.DISCIPULO2.NOME.Equals(Nome)).OrderBy(p => p.NOME).ToList<DISCIPULO>();
            ViewBag.Nome = Nome;

           
            if (gerarPDF != true)
            {
                int paginaQdteRegistros = 100;
                int paginaNumeroNavegacao = (pagina ?? 1);

                return View(discipulos.ToPagedList(paginaNumeroNavegacao, paginaQdteRegistros));
            }
            int paginaNumero = 1;

            var pdf = new ViewAsPdf
            {
                ViewName = "DiscEquipe",
                PageSize = Size.A4,
                IsGrayScale = true,
                Model = discipulos.ToPagedList(paginaNumero, discipulos.Count()),
            };
            return pdf;
        }

    }
}










