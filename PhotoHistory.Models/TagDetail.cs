using PhotoHistory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoHistory.Models
{
    public class TagDetail
    {
        public string TagName { get; set; }

        public string TagType { get; set; }

        public virtual List<Photo> Photos { get; set; }
    }
}
