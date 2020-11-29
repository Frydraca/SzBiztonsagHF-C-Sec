using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Models.CaffFile
{
    public class DownloadableFile
    {
        public MemoryStream Memory { get; set; }
        public string FilePath { get; set; }
    }
}
