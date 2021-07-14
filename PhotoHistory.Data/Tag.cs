using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Data
{
    public class Tag
    {
        [Key]
        [Display(Name = "Tag ID")]
        public int TagId { get; set; }

        [Required]
        [Display(Name = "Tag Name")]
        public string TagName { get; set; }

        [Required]
        [Display(Name = "Tag Type")]
        public string TagType { get; set; }

        public virtual List<PhotoTag> Photos { get; set; }
    }
}
