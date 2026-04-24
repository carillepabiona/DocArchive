using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocArchive.Models
{
    public class Document
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string FilePath { get; set; }

        public int CategoryId { get; set; }

        public int UploadedBy { get; set; }
        public bool IsConfidential { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
