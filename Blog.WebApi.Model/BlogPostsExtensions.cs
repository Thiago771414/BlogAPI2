using Microsoft.AspNetCore.Http;
using System.IO;

namespace Blog.WebApi.Model
{
    public static class BlogPostsExtensions
    {
        public static byte[] ConvertToBytes(this IFormFile image)
        {
            if (image == null)
            {
                return null;
            }
            using (var inputStream = image.OpenReadStream())
            using (var stream = new MemoryStream())
            {
                inputStream.CopyTo(stream);
                return stream.ToArray();
            }
        }

        public static BlogPosts ToPost(this PostUpload model)
        {
            return new BlogPosts
            {
                Id = model.Id,
                Titulo = model.Titulo,
                Subtitulo = model.Subtitulo,
                Post = model.Post,
                Autor = model.Autor,
                ImagemCapa = model.Capa.ConvertToBytes(),
                Lista = model.Lista
            };
        }

        public static PostApi ToApi(this BlogPosts blogPosts)
        {
            return new PostApi
            {
                Id = blogPosts.Id,
                Titulo = blogPosts.Titulo,
                Subtitulo = blogPosts.Subtitulo,
                Post = blogPosts.Post,
                Autor = blogPosts.Autor,
                Capa = $"/api/capas/{blogPosts.Id}",
                Lista = blogPosts.Lista.ParaString()
            };
        }

        public static PostUpload ToModel(this BlogPosts blogPosts)
        {
            return new PostUpload
            {
                Id = blogPosts.Id,
                Titulo = blogPosts.Titulo,
                Subtitulo = blogPosts.Subtitulo,
                Post = blogPosts.Post,
                Autor = blogPosts.Autor,
                Lista = blogPosts.Lista
            };
        }
    }
}
