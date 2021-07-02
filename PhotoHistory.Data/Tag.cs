using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Data
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        public string TagName { get; set; }

        [Required]
        public string TagType { get; set; }

        public virtual List<Photo> Photos { get; set; }
    }
}
