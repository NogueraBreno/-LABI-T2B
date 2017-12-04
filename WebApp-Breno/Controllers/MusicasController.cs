using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp_Breno.Models;

namespace WebApp_Breno.Controllers
{
    public class MusicasController : Controller
    {
        private BdSisMusica db = new BdSisMusica();

        public ActionResult VerificaTitulo(string Titulo)
        {
            return Json(db.Musicas.All(c => c.Titulo.ToLower() != Titulo.ToLower()), JsonRequestBehavior.AllowGet);
        }

        // GET: Musicas
        public ActionResult Index(string ordenacao,int? pagina)
        {
            ViewBag.OrdenacaoAtual = ordenacao;
            ViewBag.TituloParam = String.IsNullOrEmpty(ordenacao) ? "Titulo_desc" : "";
            ViewBag.CategoriaParam = ordenacao == "Categoria" ? "Categoria_desc" : "Categoria";
            ViewBag.TipoParam = ordenacao == "Tipo" ? "Tipo_desc" : "Tipo";

            var musicas = from m in db.Musicas select m;


            int tamanhoPagina = 3;
            int numeroPagina = pagina ?? 1;

            switch (ordenacao)
            {
                case "Titulo_desc":
                    musicas = musicas.OrderByDescending(s => s.Titulo);
                    break;
                case "Categoria":
                    musicas = musicas.OrderBy(s => s.Categoria);
                    break;
                case "Categoria_desc":
                    musicas = musicas.OrderByDescending(s => s.Categoria);
                    break;
                case "Tipo_desc":
                    musicas = musicas.OrderByDescending(s => s.Tipo);
                    break;
                case "Tipo":
                    musicas = musicas.OrderBy(s => s.Tipo);
                    break;
                default:
                    musicas = musicas.OrderBy(s => s.Titulo);
                    break;
             }


            return View(musicas.ToPagedList(numeroPagina, tamanhoPagina));
        }

        // db.Musicas.OrderBy(p => p.Titulo).ToPagedList(numeroPagina, tamanhoPagina)
        // GET: Musicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musica musica = db.Musicas.Find(id);
            if (musica == null)
            {
                return HttpNotFound();
            }
            return View(musica);
        }

        // GET: Musicas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Musicas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Tipo,Categoria,Data")] Musica musica)
        {
            if (ModelState.IsValid)
            {
                db.Musicas.Add(musica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(musica);
        }

        // GET: Musicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musica musica = db.Musicas.Find(id);
            if (musica == null)
            {
                return HttpNotFound();
            }
            return View(musica);
        }

        // POST: Musicas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Tipo,Categoria,Data")] Musica musica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musica);
        }

        // GET: Musicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musica musica = db.Musicas.Find(id);
            if (musica == null)
            {
                return HttpNotFound();
            }
            return View(musica);
        }

        // POST: Musicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Musica musica = db.Musicas.Find(id);
            db.Musicas.Remove(musica);
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
