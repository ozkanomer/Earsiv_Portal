using Earsiv_Portal.Models;
using Earsiv_Portal.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Earsiv_Portal.Controllers
{
    public class EarsivController : Controller
    {
        private EarsivService _service;
        public EarsivController()
        {
            _service = new EarsivService();
        }

        // Belge Listeleme iişlemi
        [HttpGet]
        public async Task<JsonResult> GetData(string StartDate, string EndDate)
        {
            // Gelen tarih değerleri uygun formatla formatlanıyor
            var startDate = Convert.ToDateTime(StartDate);
            var endDate = Convert.ToDateTime(EndDate);

            var sonucKullaniciOner = await _service.KullaniciOner();

            if (sonucKullaniciOner.Item1 == 3)
            {
                var kullanici = JsonConvert.DeserializeObject<EarsivUserModel>(sonucKullaniciOner.Item2); // Önerilen kullanıcı yanıtı JSON formatından kullanıcı modeline çevriliyor

                // Kullanıcı öneri isteğinin sonucu başarılı olduğu için kullanıcı giriş aşamasına geçiliyor
                var sonucKullaniciGiris = await _service.KullaniciGiris(kullanici.userid, "1");

                if (sonucKullaniciGiris.Item1 == 3)
                {
                    var token = JsonConvert.DeserializeObject<EarsivTokenModel>(sonucKullaniciGiris.Item2);

                    // Belge Sorgulama İşlemi
                    var res = await _service.BelgeSorgu(token.Token, startDate, endDate);

                    var belgeListesi = JsonConvert.DeserializeObject<EarsivDocumentModel>(res.Item2);

                    return Json(belgeListesi);
                }
                else
                {
                    return Json(sonucKullaniciGiris);
                }
            }
            else
            {
                return Json(sonucKullaniciOner);
            }
        }

        // Belge Göster
        public async Task<JsonResult> ShowDocument(string Ettn, string Onay)
        {
            var sonucKullaniciOner = await _service.KullaniciOner();

            if (sonucKullaniciOner.Item1 == 3)
            {
                var kullanici = JsonConvert.DeserializeObject<EarsivUserModel>(sonucKullaniciOner.Item2); // Önerilen kullanıcı yanıtı JSON formatından kullanıcı modeline çevriliyor

                // Kullanıcı öneri isteğinin sonucu başarılı olduğu için kullanıcı giriş aşamasına geçiliyor
                var sonucKullaniciGiris = await _service.KullaniciGiris(kullanici.userid, "1");

                if (sonucKullaniciGiris.Item1 == 3)
                {
                    var token = JsonConvert.DeserializeObject<EarsivTokenModel>(sonucKullaniciGiris.Item2);

                    var belgeSonuc = await _service.BelgeGoruntule(token.Token, JsonConvert.SerializeObject(new
                    {
                        ettn = Ettn,
                        onayDurumu = Onay
                    }).ToString());

                    return Json(belgeSonuc);
                }
                else
                {
                    return Json(sonucKullaniciGiris);
                }
            }
            else
            {
                return Json(sonucKullaniciOner);
            }
        }
    }
}
