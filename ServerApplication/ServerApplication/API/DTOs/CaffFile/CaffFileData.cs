using ServerApplication.BLL.Models.CaffFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.API.DTOs.CaffFile
{
    public class CaffFileData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime CreationDate { get; set; }
        public List<CommentData> Comments { get; set; }
    }
}
