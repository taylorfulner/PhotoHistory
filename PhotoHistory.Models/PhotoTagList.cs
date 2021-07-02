using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Models
{
    public class PhotoTagList
    {
        public int PhotoTagId { get; set; }
        public int PhotoId { get; set; }
        public PhotoList Photo { get; set; }
        public int TagId { get; set; }
        public TagList Tag { get; set; }
    }
}

