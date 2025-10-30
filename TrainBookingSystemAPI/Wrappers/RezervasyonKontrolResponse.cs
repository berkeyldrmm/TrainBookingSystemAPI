using TrainBookingSystemAPI.Dtos.Rezervasyonlar.YerlesimAyrinti;

namespace TrainBookingSystemAPI.Wrappers
{
    public record RezervasyonKontrolResponse(bool RezervasyonYapilabilir, IEnumerable<YerlesimAyrintiDto> YerlesimAyrinti);
}
