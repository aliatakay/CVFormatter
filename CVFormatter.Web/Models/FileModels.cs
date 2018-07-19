using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVFormatter.Web.Models
{
    public class FileDetail
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class FileViewModel
    {
        public List<FileDetail> Files { get; set; } = new List<FileDetail>();
    }

}
