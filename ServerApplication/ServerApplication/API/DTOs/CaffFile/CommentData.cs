﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApplication.API.DTOs.CaffFile
{
    public class CommentData
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Owner { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
