using Blog.WebApi.Model;
using System.Collections.Generic;

namespace BlogApplication.Models
{
    public class HomeViewModel
    {
        public IEnumerable<PostApi> ParaLer { get; set; }
        public IEnumerable<PostApi> Lendo { get; set; }
        public IEnumerable<PostApi> Lidos { get; set; }
    }
}
