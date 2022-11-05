namespace Accounting.Domain.Requests
{
    public class DeducationDocumentSearchRequest
    {
        public DateTime DateCreate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Name { get; set; }
    }
}
