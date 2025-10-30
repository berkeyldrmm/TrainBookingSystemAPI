using TrainBookingSystemAPI.Abstracts.Rezervasyonlar;
using TrainBookingSystemAPI.Dtos.Rezervasyonlar.YerlesimAyrinti;
using TrainBookingSystemAPI.Wrappers;

namespace TrainBookingSystemAPI.Services.Rezervasyonlar
{
    public class RezervasyonService : IRezervasyonService
    {
        private readonly double MaxDolulukOrani;

        public RezervasyonService(IConfiguration iConfiguration)
        {
            MaxDolulukOrani = iConfiguration.GetValue<double>("Rezervasyon:MaxVagonDolulukOrani"); ;
        }

        public bool RezervastonYapilabilirlik(RezervasyonKontrolRequest request)
        {
            int kisiSayisi = request.RezervasyonYapilacakKisiSayisi;
            var vagonlar = request.Tren.Vagonlar;

            if (request.KisilerFarkliVagonlaraYerlestirilebilir)
            {
                int toplamBosKoltukSayisi = 0;

                foreach (var vagon in vagonlar)
                {
                    toplamBosKoltukSayisi += (int)Math.Floor(vagon.Kapasite * this.MaxDolulukOrani - vagon.DoluKoltukAdet);
                }

                return kisiSayisi <= toplamBosKoltukSayisi;
            }
            else
            {
                foreach (var vagon in vagonlar)
                {
                    int doldurulabilirKoltukSayisi = (int)Math.Floor(vagon.Kapasite * this.MaxDolulukOrani - vagon.DoluKoltukAdet);

                    if (kisiSayisi <= doldurulabilirKoltukSayisi)
                        return true;
                }

                return false;
            }
        }

        public List<YerlesimAyrintiDto> YerlesimAyrintiOlustur(RezervasyonKontrolRequest request)
        {
            var vagonlar = request.Tren.Vagonlar;
            var kisiSayisi = request.RezervasyonYapilacakKisiSayisi;
            var yerlesimAyrintiListesi = new List<YerlesimAyrintiDto>();

            if (!request.KisilerFarkliVagonlaraYerlestirilebilir)
            {
                foreach (var vagon in vagonlar)
                {
                    int doldurulabilirKoltukSayisi = (int)Math.Ceiling(vagon.Kapasite * this.MaxDolulukOrani - vagon.DoluKoltukAdet);

                    if (kisiSayisi <= doldurulabilirKoltukSayisi)
                    {
                        yerlesimAyrintiListesi.Add(new YerlesimAyrintiDto(vagon.Ad, kisiSayisi));
                        break;
                    }
                }
            }
            else
            {
                foreach (var vagon in vagonlar)
                {
                    int doldurulabilirKoltukSayisi = (int)Math.Ceiling(vagon.Kapasite * this.MaxDolulukOrani - vagon.DoluKoltukAdet);
                    if (doldurulabilirKoltukSayisi > 0)
                    {
                        if (kisiSayisi >= doldurulabilirKoltukSayisi)
                        {
                            yerlesimAyrintiListesi.Add(new YerlesimAyrintiDto(vagon.Ad, doldurulabilirKoltukSayisi));
                            kisiSayisi -= doldurulabilirKoltukSayisi;
                        }
                        else
                        {
                            yerlesimAyrintiListesi.Add(new YerlesimAyrintiDto(vagon.Ad, kisiSayisi));
                            break;
                        }
                    }

                    if (kisiSayisi == 0)
                        break;
                }
            }

            return yerlesimAyrintiListesi;
        }
    }
}
