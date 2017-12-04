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

        // GET: Musicas
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            ViewBag.TituloSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DataSortParm = sortOrder == "Data" ? "data_desc" : "Data";
            ViewBag.CurrentFilter = searchString;

            var musicas = from s in db.Musicas
                           select s;
            
           
            
            if (!String.IsNullOrEmpty(searchString))
            {
                musicas = musicas.Where(s => s.Titulo.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "titulo_desc":
                    musicas = musicas.OrderByDescending(s => s.Titulo);
                    break;
                case "Data":
                    musicas = musicas.OrderBy(s => s.Data);
                    break;
                case "data._desc":
                    musicas = musicas.OrderByDescending(s => s.Data);
                    break;
                default:
                    musicas = musicas.OrderBy(s => s.Categoria);
                    break;
            }

            
            return View(db.Musicas);
        }

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
