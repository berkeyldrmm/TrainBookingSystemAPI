using TrainBookingSystemAPI.Dtos.Rezervasyonlar.YerlesimAyrinti;
using TrainBookingSystemAPI.Wrappers;

namespace TrainBookingSystemAPI.Abstracts.Rezervasyonlar
{
    public interface IRezervasyonService
    {
        public bool RezervastonYapilabilirlik(RezervasyonKontrolRequest request);
        public List<YerlesimAyrintiDto> YerlesimAyrintiOlustur(RezervasyonKontrolRequest request);
    }
}
