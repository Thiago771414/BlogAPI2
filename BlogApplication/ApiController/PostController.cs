using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.WebApi.Model;
using Blog.WebApi.Repository.Posts;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.ApiController
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IRepository<BlogPosts> _repo;

        public PostController(IRepository<BlogPosts> repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public IActionResult ListaDePosts()
        {
            var lista = _repo.All.Select(l => l.ToApi()).ToList();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult Recuperar(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model.ToApi());
        }

        [HttpGet("{id}/capa")]
        public IActionResult ImagemCapa(int id)
        {
            byte[] img = _repo.All
               .Where(l => l.Id == id)
               .Select(l => l.ImagemCapa)
               .FirstOrDefault();
            if (img != null)
            {
                return File(img, "image/png");
            }
            return File("~/images/capas/capa-vazia.png", "image/png");
        }

        [HttpPost]
        public ActionResult Incluir([FromBody] PostUpload model)
        {
            if (ModelState.IsValid)
            {
                var post = model.ToPost();
                _repo.Incluir(post);
                var uri = Url.Action("Recuperar", new { id = post.Id });
                return Created(uri, post);
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Alterar([FromBody] PostUpload model)
        {
            if (ModelState.IsValid)
            {
                var post = model.ToPost();
                if (model.Capa == null)
                {
                    post.ImagemCapa = _repo.All
                        .Where(l => l.Id == post.Id)
                        .Select(l => l.ImagemCapa)
                        .FirstOrDefault();
                }
                _repo.Alterar(post);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            _repo.Excluir(model);
            return NoContent();
        }
    }
}