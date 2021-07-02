using PhotoHistory.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Models
{
    public class PhotoDetail
    {
        public int PhotoId { get; set; }

        public string PhotoName { get; set; }

        public string PhotoDesc { get; set; }

        public DateTime PhotoDate { get; set; }

        public string PhotoStorageLocation { get; set; }

        [Display(Name = "Uploaded By")]
        public Guid AdminId { get; set; }

        public DateTime PhotoUploadDate { get; set; }

        public virtual List<Tag> Tags { get; set; }
    }
}
