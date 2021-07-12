using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoHistory.Data
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        [Required]
        public string PhotoName { get; set; }

        public string PhotoDesc { get; set; }

        public DateTime PhotoDate { get; set; }

        [Required]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase User_image_data { get; set; }

        [Required]
        public DateTimeOffset PhotoUploadDate { get; set; }

        public virtual List<PhotoTag> Tags { get; set; } = new List<PhotoTag>();

    }
}
