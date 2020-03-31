using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.WebApi.Model
{
    public static class TipoListaPostExtensions
    {
        private static Dictionary<string, TipoListaPost> mapa =
            new Dictionary<string, TipoListaPost>
            {
                { "ParaLer", TipoListaPost.ParaLer },
                { "Lendo", TipoListaPost.Lendo },
                { "Lidos", TipoListaPost.Lidos }
            };

        public static string ParaString(this TipoListaPost tipo)
        {
            return mapa.First(s => s.Value == tipo).Key;
        }

        public static TipoListaPost ParaTipo(this string texto)
        {
            return mapa.First(t => t.Key == texto).Value;
        }
    }

    public enum TipoListaPost
    {
        ParaLer,
        Lendo,
        Lidos
    }

    public class ListaPost
    {
        public string Tipo { get; set; }
        public IEnumerable<PostApi> Posts { get; set; }
    }

}


