using Accounting.Domain.Enums;

namespace Accounting.Domain.Requests
{
    public class DocumentSearchRequest
    {
        public DateTime DateCreate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Name { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}
