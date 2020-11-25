using ServerApplication.BLL.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Models.CaffFile.DB
{
    [Table("Comments")]
    public class Comment : DBModel
    {
        public string Owner { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
