using ServerApplication.BLL.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Models.CaffFile.DB
{
    [Table("CaffFiles")]
    public class CaffFile : DBModel
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string FilePath { get; set; }
        public byte[] AesKey { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
