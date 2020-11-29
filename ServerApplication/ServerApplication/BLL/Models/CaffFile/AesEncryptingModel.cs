using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.BLL.Models.CaffFile
{
    public class AesEncryptingModel
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
