using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocArchive.Models
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string Name { get; set; } // Birth Certificate
        public string Code { get; set; } // BC
        public int LastNumber { get; set; } // 1,2,3...
    }
}
