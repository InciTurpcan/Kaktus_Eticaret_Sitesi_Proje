namespace iakademi47_proje.Models
{
    public class Activite
    {
        public string? Tur { get; set; }
        public string? Adi { get; set; }
        public DateTime EtkinlikBaslamaTarihi { get; set; }
        public DateTime EtkinlikBitisTarihi { get; set; }
        public string? EtkinlikMerkezi { get; set; }
        public string? KisaAciklama { get; set; }
        public string? BiletSatisLinki { get; set; }
        public string? KucukAfis { get; set; }
    }
}
