using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace CVFormatter.Web.Extensions
{
    public static class IFormFileExtensions
    {
        public static string GetFileName(this IFormFile file)
        {
            return ContentDispositionHeaderValue.Parse(
                            file.ContentDisposition).FileName.ToString().Trim('"');
        }

    }
}
