using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using estabelecimentoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace estabelecimentoMVC.Controllers
{
    [Route("estabelecimento")]
    public class EstabelecimentoController : Controller
    {
        readonly DataContext db = new DataContext();

        [Route("EstabelecimentoList")]
        public IActionResult EstabelecimentoList()
        {            
            var dataContext = db.Estabelecimento.Include(e => e.Categoria);
            return View("EstabelecimentoList", dataContext.ToList());
        }

        [HttpGet]
        [Route("EstabelecimentoAdd")]
        public IActionResult EstabelecimentoAdd()
        {
            ViewBag.IdCategoria = new SelectList(db.Set<Categoria>(), "IdCategoria", "Descricao");
            ViewBag.IdEstado = new SelectList(db.Set<Estado>(), "IdEstado", "Uf");
            return View("EstabelecimentoAdd");
        }

        [HttpPost]
        [Route("EstabelecimentoAdd")]
        public IActionResult EstabelecimentoAdd([Bind("IdEstabelecimento,RazaoSocial,NomeFantasia,Cnpj,Email,Endereco,Cidade,Estado,Telefone,DataCadastro,IdCategoria,Categoria,Status,Agencia,Conta")] Estabelecimento estabelecimento)
        {
            db.Add(estabelecimento);
            db.SaveChanges();
            return RedirectToAction("EstabelecimentoList");
        }

        [HttpPost]
        [Route("EstabelecimentoSearch")]
        public IActionResult EstabelecimentoSearch(string text)
        {
            var dataContext = db.Estabelecimento.Include(e => e.Categoria);

            if (string.IsNullOrEmpty(text))
            {
                return View("EstabelecimentoList", dataContext.ToList());
            } 
            else if (text.ToUpper() == "ATIVO")
            {
                return View("EstabelecimentoList", dataContext.Where(e => e.Status.ToUpper().Equals(text.ToUpper())));
            }
            else
            {
                return View("EstabelecimentoList", dataContext.Where(e => e.RazaoSocial.ToUpper().Contains(text.ToUpper())
                || e.NomeFantasia.ToUpper().Contains(text.ToUpper())
                || e.Cnpj.ToUpper().Contains(text.ToUpper())
                || e.Email.ToUpper().Contains(text.ToUpper())
                || e.Endereco.ToUpper().Contains(text.ToUpper())
                || e.Cidade.ToUpper().Contains(text.ToUpper())
                || e.Estado.ToUpper().Contains(text.ToUpper())
                || e.Telefone.ToUpper().Contains(text.ToUpper())
                || e.DataCadastro.ToString().Contains(text)
                || e.Categoria.Descricao.ToUpper().Contains(text.ToUpper())
                || e.Status.ToUpper().Contains(text.ToUpper())
                || e.Agencia.ToUpper().Contains(text.ToUpper())
                || e.Conta.ToUpper().Contains(text.ToUpper())));
            }            
        }

        [HttpGet]
        [Route("EstabelecimentoEdit/{id}/{uf?}")]
        public IActionResult EstabelecimentoEdit(int id, string uf)
        {
            ViewBag.IdCategoria = new SelectList(db.Set<Categoria>(), "IdCategoria", "Descricao");
            ViewBag.IdEstado = new SelectList(db.Set<Estado>(), "IdEstado", "Uf");
            var municipios = db.Municipio.Where(s => s.Uf == uf).ToList();
            ViewBag.IdMunicipio = new SelectList(municipios, "IdMunicipio", "Nome");
            return View("EstabelecimentoEdit", db.Find<Estabelecimento>(id));
        }

        [HttpPost]
        [Route("EstabelecimentoEdit/{id}/{uf?}")]
        public IActionResult EstabelecimentoEdit([Bind("IdEstabelecimento,RazaoSocial,NomeFantasia,Cnpj,Email,Endereco,Cidade,Estado,Telefone,DataCadastro,IdCategoria,Categoria,Status,Agencia,Conta")] Estabelecimento estabelecimentos)
        {
            db.Entry(estabelecimentos).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("EstabelecimentoList");
        }

        [HttpGet]
        [Route("EstabelecimentoDelete/{id}")]
        public IActionResult EstabelecimentoDelete(int id)
        {
            db.Remove(db.Find<Estabelecimento>(id));
            db.SaveChanges();
            return RedirectToAction("EstabelecimentoList");
        }

        [HttpGet]
        [Route("LoadMunicipios/{uf?}")]
        public JsonResult LoadMunicipios(string uf = "")
        {
            var municipios = db.Municipio.Where(m => m.Uf == uf).ToList();
            return Json(new SelectList(municipios, "IdMunicipio", "Nome"));
        }
    }
}
