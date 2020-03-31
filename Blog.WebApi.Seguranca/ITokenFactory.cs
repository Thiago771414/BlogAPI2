using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.WebApi.Seguranca
{
    public interface ITokenFactory
    {
        string Token { get; }
    }
}
