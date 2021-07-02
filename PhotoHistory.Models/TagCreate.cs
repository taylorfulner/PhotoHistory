using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Models
{
    public class TagCreate
    {
        [Required]
        public string TagName { get; set; }

        [Required]
        public string TagType { get; set; }
    }
}
