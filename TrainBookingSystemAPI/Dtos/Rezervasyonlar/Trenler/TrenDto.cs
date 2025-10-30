using TrainBookingSystemAPI.Dtos.Rezervasyonlar.Vagonlar;

namespace TrainBookingSystemAPI.Dtos.Rezervasyonlar.Trenler
{
    public record TrenDto(string Ad, IEnumerable<VagonDto> Vagonlar);
}
