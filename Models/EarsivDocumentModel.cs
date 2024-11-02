using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Earsiv_Portal.Models
{
    public class EarsivDocumentModel
    {
        public List<Data>? Data { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class Data
    {
        public string? BelgeNumarasi { get; set; }
        public string? AliciVknTckn { get; set; }
        public string? AliciUnvanAdSoyad { get; set; }
        public string? BelgeTarihi { get; set; }
        public string? BelgeTuru { get; set; }
        public string? OnayDurumu { get; set; }
        public string? Ettn { get; set; }
    }

    public class Metadata
    {
        public string Optime { get; set; }
    }
}
