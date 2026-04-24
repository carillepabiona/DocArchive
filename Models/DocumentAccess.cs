namespace DocArchive.Models
{
    public class DocumentAccess
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }
        public int RoleId { get; set; }

        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}