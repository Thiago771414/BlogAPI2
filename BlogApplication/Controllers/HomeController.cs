using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.WebApi.Model;
using Blog.WebApi.Repository.Posts;
using BlogApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IRepository<BlogPosts> _repo;

        public HomeController(IRepository<BlogPosts> repository)
        {
            _repo = repository;
        }

        private IEnumerable<PostApi> ListaDoTipo(TipoListaPost tipo)
        {
            return _repo.All
                .Where(l => l.Lista == tipo)
                .Select(l => l.ToApi())
                .ToList();
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                ParaLer = ListaDoTipo(TipoListaPost.ParaLer),
                Lendo = ListaDoTipo(TipoListaPost.Lendo),
                Lidos = ListaDoTipo(TipoListaPost.Lidos)
            };
            return View(model);
        }
    }
}