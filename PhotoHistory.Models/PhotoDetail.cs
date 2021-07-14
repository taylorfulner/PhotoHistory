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

        public string Image { get; set; }

        public DateTimeOffset PhotoUploadDate { get; set; }

        public virtual List<TagList> Tags { get; set; } = new List<TagList>();
    }
}
