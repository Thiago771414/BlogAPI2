using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Blog.WebApi.Model
{
    public class BlogPosts
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Post { get; set; }
        public byte[] ImagemCapa { get; set; }
        public string Autor { get; set; }
        public TipoListaPost Lista { get; set; }
    }

    [XmlType("Post")]
    public class PostApi
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Autor { get; set; }
        public string Post { get; set; }
        public string Capa { get; set; }
        public string Lista { get; set; }
    }

    public class PostUpload
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Autor { get; set; }
        public string Post { get; set; }
        public IFormFile Capa { get; set; }
        public TipoListaPost Lista { get; set; }
    }
}
