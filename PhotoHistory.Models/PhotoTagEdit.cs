using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Models
{
    public class PhotoTagEdit
    {
        public int PhotoTagId { get; set; }
        
        public int PhotoId { get; set; }

        public int TagId { get; set; }
    }
}
