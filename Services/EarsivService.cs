using Microsoft.AspNetCore.Authentication;
using System.Text.Json;

namespace Earsiv_Portal.Services
{
    public class EarsivService
    {
        HttpClient httpClient;

        public EarsivService()
        {
            httpClient = new HttpClient(); // Yeni bir clien oluşturuldu
            httpClient.Timeout = TimeSpan.FromSeconds(30); // Zaman aşımı sınırlandırılması

            // İsteklerin kabul edilmesi için ek başlıklar eklendi
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; AcmeInc/1.0)");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Kullanıcı Öner Methodu
        public async Task<Tuple<int, string>> KullaniciOner()
        {
            try
            {
                // Url içeriğinde gönderilecek olan değerler oluşturuldu
                var formIcerik = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("assoscmd", "kullaniciOner"),
                    new KeyValuePair<string, string>("rtype", "json")
                });

                // HTTPClient ile POST İşlemi
                var result = await httpClient.PostAsync("https://earsivportaltest.efatura.gov.tr/earsiv-services/esign", formIcerik);

                // Eğer istek gönderimi başarılı ise
                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(3, resultContent);
                }
                else
                {
                    var errorContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(0, $"Error: {result.StatusCode}, Content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(-1, ex.Message);
            }
        }

        // Kullanıcı Giriş İşlemi
        public async Task<Tuple<int, string>> KullaniciGiris(string userid, string? password = null)
        {
            try
            {
                // Url içeriğinde gönderilecek olan değerler oluşturuldu
                var formIcerik = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("assoscmd", "login"),
                    new KeyValuePair<string, string>("rtype", "json"),
                    new KeyValuePair<string, string>("userid", userid),
                    new KeyValuePair<string, string>("sifre", password == null ? "1" : password),
                    new KeyValuePair<string, string>("parola", password == null ? "1" : password),
                });

                // HTTPClient ile POST İşlemi
                var result = await httpClient.PostAsync("https://earsivportaltest.efatura.gov.tr/earsiv-services/assos-login", formIcerik);

                // Eğer istek gönderimi başarılı ise
                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(3, resultContent);
                }
                else
                {
                    var errorContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(0, $"Error: {result.StatusCode}, Content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(-1, ex.Message);
            }
        }

        // Sorgu İşlemi
        public async Task<Tuple<int, string>> BelgeSorgu(string token, DateTime startdate, DateTime enddate)
        {
            try
            {
                // jp Değeri için veriler JSON formatına hazırlandı
                var jpObject = new
                {
                    baslangic = startdate.ToString("dd/MM/yyyy"),
                    bitis = enddate.ToString("dd/MM/yyyy"),
                    hangiTip = "Buyuk"
                };

                var jpJson = Newtonsoft.Json.JsonConvert.SerializeObject(jpObject);

                // Url içeriğinde gönderilecek olan değerler oluşturuldu
                var formIcerik = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("cmd", "EARSIV_PORTAL_TASLAKLARI_GETIR"),
                    new KeyValuePair<string, string>("callid", Guid.NewGuid().ToString()),
                    new KeyValuePair<string, string>("pageName", "RG_BASITTASLAKLAR"),
                    new KeyValuePair<string, string>("token", token),
                    new KeyValuePair<string, string>("jp", jpJson) // JSON Formatında veriler eklendi
                });

                // HTTPClient ile POST İşlemi
                var result = await httpClient.PostAsync("https://earsivportaltest.efatura.gov.tr/earsiv-services/dispatch", formIcerik);

                // Otomatik Çıkış yapılacak
                await Logout(token);

                // Eğer istek gönderimi başarılı ise
                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(3, resultContent);
                }
                else
                {
                    var errorContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(0, $"Error: {result.StatusCode}, Content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(-1, ex.Message);
            }
        }

        // Oturum Sonlandırma İşlemi
        public async Task<Tuple<int, string>> Logout(string token)
        {
            try
            {
                // Url içeriğinde gönderilecek olan değerler oluşturuldu
                var formIcerik = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("assoscmd", "logout"),
                    new KeyValuePair<string, string>("token", token)
                });

                // HTTPClient ile POST İşlemi
                var result = await httpClient.PostAsync("https://earsivportaltest.efatura.gov.tr/earsiv-services/assos-login", formIcerik);

                // Eğer istek gönderimi başarılı ise
                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(3, resultContent);
                }
                else
                {
                    var errorContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(0, $"Error: {result.StatusCode}, Content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(-1, ex.Message);
            }
        }

        // Belge görüntüle
        public async Task<Tuple<int, string>> BelgeGoruntule(string token, string belgeBilgi)
        {
            try
            {
                // Url içeriğinde gönderilecek olan değerler oluşturuldu
                var formIcerik = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("cmd", "EARSIV_PORTAL_FATURA_GOSTER"),
                    new KeyValuePair<string, string>("callid", Guid.NewGuid().ToString()),
                    new KeyValuePair<string, string>("pageName", "RG_TASLAKLAR"),
                    new KeyValuePair<string, string>("token", token),
                    new KeyValuePair<string, string>("jp", belgeBilgi)
                });

                // HTTPClient ile POST İşlemi
                var result = await httpClient.PostAsync("https://earsivportaltest.efatura.gov.tr/earsiv-services/dispatch", formIcerik);

                // Otomatik Çıkış işlemi
                await Logout(token);

                // Eğer istek gönderimi başarılı ise
                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    using (JsonDocument doc = JsonDocument.Parse(resultContent))
                    {
                        var content = doc.RootElement.GetProperty("data").GetString();
                        return new Tuple<int, string>(3, content);
                    }
                }
                else
                {
                    var errorContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(0, $"Error: {result.StatusCode}, Content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(-1, ex.Message);
            }
        }

        // Belge Yazdır
        public async Task<Tuple<int, string>> BelgeYazdır()
        {
            try
            {
                // Url içeriğinde gönderilecek olan değerler oluşturuldu
                var formIcerik = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("assoscmd", "logout")
                });

                // HTTPClient ile POST İşlemi
                var result = await httpClient.PostAsync("https://earsivportaltest.efatura.gov.tr/earsiv-services/dispatch", formIcerik);

                // Eğer istek gönderimi başarılı ise
                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(3, resultContent);
                }
                else
                {
                    var errorContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(0, $"Error: {result.StatusCode}, Content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(-1, ex.Message);
            }
        }

        // Belge indir
        public async Task<Tuple<int, string>> BelgeIndir()
        {
            try
            {
                // Url içeriğinde gönderilecek olan değerler oluşturuldu
                var formIcerik = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("cmd", "EARSIV_PORTAL_FATURA_GOSTER"),
                    new KeyValuePair<string, string>("callid", Guid.NewGuid().ToString()),
                    new KeyValuePair<string, string>("pageName", "RG_TASLAKLAR"),
                    new KeyValuePair<string, string>("token", "EARSIV_PORTAL_FATURA_GOSTER"),
                    new KeyValuePair<string, string>("jp", "EARSIV_PORTAL_FATURA_GOSTER"),
                });

                // HTTPClient ile POST İşlemi
                var result = await httpClient.PostAsync("https://earsivportaltest.efatura.gov.tr/earsiv-services/dispatch", formIcerik);

                // Eğer istek gönderimi başarılı ise
                if (result.IsSuccessStatusCode)
                {
                    var resultContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(3, resultContent);
                }
                else
                {
                    var errorContent = await result.Content.ReadAsStringAsync();
                    return new Tuple<int, string>(0, $"Error: {result.StatusCode}, Content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(-1, ex.Message);
            }
        }
    }
}
