using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Models
{
    public class PhotoTagCreate
    {
        [Required]
        public int PhotoId { get; set; }

        [Required]
        public int TagId { get; set; }
    }
}
