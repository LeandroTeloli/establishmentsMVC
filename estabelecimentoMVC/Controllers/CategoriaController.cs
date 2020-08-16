using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using estabelecimentoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace estabelecimentoMVC.Controllers
{
    public class CategoriaController : Controller
    {
        readonly DataContext db = new DataContext();

        [Route("CategoriaList")]
        public IActionResult CategoriaList()
        {
            ViewBag.categoria = db.Categoria.ToList();
            return View("CategoriaList");
        }

        [HttpGet]
        [Route("CategoriaAdd")]
        public IActionResult CategoriaAdd()
        {
            return View("CategoriaAdd");
        }

        [HttpPost]
        [Route("CategoriaAdd")]
        public IActionResult CategoriaAdd([Bind("IdCategoria,Descricao")] Categoria categoria)
        {
            db.Categoria.Add(categoria);
            db.SaveChanges();
            return RedirectToAction("CategoriaList");
        }

        [HttpPost]
        [Route("CategoriaSearch")]
        public IActionResult CategoriaSearch(string descricao)
        {
            if (String.IsNullOrEmpty(descricao))
            {
                ViewBag.categoria = db.Categoria.ToList();
                return View("CategoriaList", ViewBag.categoria);
            }
            else
            {
                ViewBag.categoria = db.Categoria.Where(c => c.Descricao.ToUpper().Contains(descricao.ToUpper()));
                return View("CategoriaList", ViewBag.categoria);
            }
        }

        [HttpGet]
        [Route("CategoriaEdit/{id}")]
        public IActionResult CategoriaEdit(int id)
        {
            return View("CategoriaEdit", db.Find<Categoria>(id));
        }

        [HttpPost]
        [Route("CategoriaEdit/{id}")]
        public IActionResult CategoriaEdit([Bind("IdCategoria,Descricao")] Categoria categorias)
        {
            db.Entry(categorias).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CategoriaList");
        }

        [HttpGet]
        [Route("CategoriaDelete/{id}")]
        public IActionResult CategoriaDelete(int id)
        {
            db.Remove(db.Find<Categoria>(id));
            db.SaveChanges();
            return RedirectToAction("CategoriaList");
        }
    }
}
