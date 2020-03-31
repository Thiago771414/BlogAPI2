using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.WebApi.Model;
using Blog.WebApi.Repository.Posts;
using Microsoft.AspNetCore.Mvc;
using Lista = Blog.WebApi.Model.ListaPost;

namespace BlogApplication.ApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListasPostController : ControllerBase
    {
        private readonly IRepository<BlogPosts> _repo;

        public ListasPostController(IRepository<BlogPosts> repository)
        {
            _repo = repository;
        }
        private Lista CriaLista(TipoListaPost tipo)
        {
            return new Lista
            {
                Tipo = tipo.ParaString(),
                Posts = _repo.All
                .Where(l => l.Lista == tipo)
                .Select(l => l.ToApi())
                .ToList()
            };
        }

        [HttpGet]
        public IActionResult TodasListas()
        {
            Lista paraLer = CriaLista(TipoListaPost.ParaLer);
            Lista lendo = CriaLista(TipoListaPost.Lendo);
            Lista lidos = CriaLista(TipoListaPost.Lidos);
            var colecao = new List<Lista> { paraLer, lendo, lidos };
            return Ok(colecao);
        }

        [HttpGet("{tipo}")]
        public IActionResult Recuperar(TipoListaPost tipo)
        {
            var lista = CriaLista(tipo);
            return Ok(lista);
        }
    }
}