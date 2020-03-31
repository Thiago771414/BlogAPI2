using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.WebApi.Model;
using Blog.WebApi.Repository.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IRepository<BlogPosts> _repo;

        public PostController(IRepository<BlogPosts> repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View(new PostUpload());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Novo(PostUpload model)
        {
            if (ModelState.IsValid)
            {
                _repo.Incluir(model.ToPost());
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
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

        public BlogPosts RecuperaPost(int id)
        {
            return  _repo.Find(id);          
        }

        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            var model = RecuperaPost(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model.ToModel());
        }

        public ActionResult<PostUpload> DetalhesJson(int id)
        {
            var model = RecuperaPost(id);
            if (model == null)
            {
                return NotFound();
            }
            return model.ToModel();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detalhes(PostUpload model)
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
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remover(int id)
        {
            var model = _repo.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            _repo.Excluir(model);
            return RedirectToAction("Index", "Home");
        }
    }
}