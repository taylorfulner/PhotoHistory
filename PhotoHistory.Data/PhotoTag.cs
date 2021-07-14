using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Data
{
    public class PhotoTag
    {
        [Key]
        public int PhotoTagId { get; set; }

        [Display(Name = "Photo")]
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }

        [Display(Name = "Tag")]
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
