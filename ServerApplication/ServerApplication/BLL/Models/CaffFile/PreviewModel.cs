using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Models.CaffFile
{
    public class PreviewModel
    {
        public string CreationDate { get; set; }
        public string Creator { get; set; }
        public string PreviewCaption { get; set; }
        public string PreviewTags { get; set; }
        public string PreviewPath { get; set; }
    }
}
