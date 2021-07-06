using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Models
{
    public class PhotoList
    {
        public int PhotoId { get; set; }

        public string PhotoName { get; set; }

        public DateTime PhotoDate { get; set; }

        public string PhotoStorageLocation { get; set; }
    }
}
