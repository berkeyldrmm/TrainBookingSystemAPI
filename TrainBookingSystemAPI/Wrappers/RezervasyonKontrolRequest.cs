using TrainBookingSystemAPI.Dtos.Rezervasyonlar.Trenler;

namespace TrainBookingSystemAPI.Wrappers
{
    public record RezervasyonKontrolRequest(TrenDto Tren, int RezervasyonYapilacakKisiSayisi, bool KisilerFarkliVagonlaraYerlestirilebilir);
}
