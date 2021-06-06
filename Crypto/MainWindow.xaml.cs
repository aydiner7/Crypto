using Crypto.Class;
using Crypto.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using Syncfusion;
using System.Data.SqlClient;
using Crypto.Dto;
using System.Windows.Threading;

namespace Crypto
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        SqlDataReader dr;


        public MainWindow()
        {
            InitializeComponent();
            ButonKontrol();
            KullaniciBilgiGetir();
          // timerCalistir();
            BakiyeGoster();
            coinDegerBelirle1();
            coinDegerBelirle2();
            coinDegerBelirle3();
            coinDegisim1();
            coinDegisim2();
            coinDegisim3();
            
            //coinDegerleriniGunceller(1);
            //coinDegerleriniGunceller(2);
            //coinDegerleriniGunceller(3);
        }


        // SDU Güncel Fiyat
        private void coinDegerBelirle1()
        {
            CoinDegerCek coinlerim = new CoinDegerCek(1);
            var gelen = coinlerim.coinListesi();

            sdu_fiyat.Text = gelen[0].deger;
           

        }

        // Maku Güncel Fiyat
        private void coinDegerBelirle2()
        {
            CoinDegerCek coinlerim = new CoinDegerCek(2);
            var gelen = coinlerim.coinListesi();

                maku_fiyat.Text = gelen[0].deger;
            
        }

        // AKU Güncel Fiyat
        private void coinDegerBelirle3()
        {
            CoinDegerCek coinlerim = new CoinDegerCek(3);
            var gelen = coinlerim.coinListesi();

                aku_fiyat.Text = gelen[0].deger;

        }

        // SDU Değişim Hesapla
        private void coinDegisim1()
        {
            CoinDegisim c_degisim = new CoinDegisim(1);
            var gelen = c_degisim.coinListesi();

            float yeni= float.Parse(gelen[0].deger);
            float eski =float.Parse(gelen[1].deger);

            if (yeni > eski)
            {
                yeni = yeni - eski;
                yeni = (yeni / eski)*100;
                sdu_degisim.Text = "↑ "+Math.Round(yeni,1).ToString()+"%";
                sdu_degisim.Foreground = (Brush)new BrushConverter().ConvertFrom("#febc2c");
            }
            else if (yeni < eski)
            {
                eski = eski - yeni;
                eski = (eski / yeni) * 100;
                sdu_degisim.Text ="↓ "+ Math.Round(eski, 1).ToString() + "%";
                sdu_degisim.Foreground = (Brush)new BrushConverter().ConvertFrom("#f84c48");
            }
            else if (yeni == eski)
            {
                sdu_degisim.Text = "- 0.0%";
                sdu_degisim.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");

            }
        }

        // MAKU Değişim Hesapla
        private void coinDegisim2()
        {
            CoinDegisim c_degisim = new CoinDegisim(2);
            var gelen = c_degisim.coinListesi();

            float yeni = float.Parse(gelen[0].deger);
            float eski = float.Parse(gelen[1].deger);

            if (yeni > eski)
            {
                yeni = yeni - eski;
                yeni = (yeni / eski) * 100;
                maku_degisim.Text = "↑ " + Math.Round(yeni, 1).ToString() + "%";
                maku_degisim.Foreground = (Brush)new BrushConverter().ConvertFrom("#febc2c");
            }
            else if (yeni < eski)
            {
                eski = eski - yeni;
                eski = (eski / yeni) * 100;
                maku_degisim.Text = "↓ " + Math.Round(eski, 1).ToString() + "%";
                maku_degisim.Foreground = (Brush)new BrushConverter().ConvertFrom("#f84c48");
            }
            else if (yeni == eski)
            {
                maku_degisim.Text = "- 0.0%";
                maku_degisim.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");

            }
        }


        // AKU Değişim Hesapla
        private void coinDegisim3()
        {
            CoinDegisim c_degisim = new CoinDegisim(3);
            var gelen = c_degisim.coinListesi();

            float yeni = float.Parse(gelen[0].deger);
            float eski = float.Parse(gelen[1].deger);

            if (yeni > eski)
            {
                yeni = yeni - eski;
                yeni = (yeni / eski) * 100;
                aku_degisim.Text = "↑ " + Math.Round(yeni, 1).ToString() + "%";
                aku_degisim.Foreground = (Brush)new BrushConverter().ConvertFrom("#febc2c");
            }
            else if (yeni < eski)
            {
                eski = eski - yeni;
                eski = (eski / yeni) * 100;
                aku_degisim.Text = "↓ " + Math.Round(eski, 1).ToString() + "%";
                aku_degisim.Foreground = (Brush)new BrushConverter().ConvertFrom("#f84c48");
            }
            else if (yeni == eski)
            {
                aku_degisim.Text = "- 0.0%";
                aku_degisim.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");

            }
        }




        // işlem panelinde kontrol için verilen kodlarım
        int alSatKodu = 1;
        int limitStopKodu = 1;

        void islemYap()
        {
            coinNumaraTakip();
            float bakiye = float.Parse(Kullanilabilir_Bakiye.Content.ToString());
            float toplam = float.Parse(toplam_textbox.Text);

            // Limitten Satın Alma
            if (alSatKodu == 1 && limitStopKodu == 1 && coinNO != 0)
            {
                if (bakiye >= toplam)
                {
                    veriTabani();
                    string sorgu = "INSERT INTO islem_log(kullanici_id,coin_id,coin_miktar,coin_fiyat,tutar,islem_kodu) VALUES (@kullanici,@coin,@miktar,@fiyat,@tutar,@islem_kodu)";
                    komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@kullanici", Int32.Parse(login.gonderilecekID));
                    komut.Parameters.AddWithValue("@coin", coinNO);
                    komut.Parameters.AddWithValue("@miktar", float.Parse(miktar_textbox.Text));
                    komut.Parameters.AddWithValue("@fiyat", float.Parse(fiyat_textbox.Text));
                    komut.Parameters.AddWithValue("@tutar", float.Parse(toplam_textbox.Text));
                    komut.Parameters.AddWithValue("@islem_kodu", 1);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    float yazilacakSonuc = bakiye - toplam;
                    string sorgu2 = "UPDATE Kullanicilar SET nakit=@nakit where id=@id";
                    komut = new SqlCommand(sorgu2, baglanti);
                    komut.Parameters.AddWithValue("@id", Int32.Parse(login.gonderilecekID));
                    komut.Parameters.AddWithValue("@nakit", yazilacakSonuc);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    float girilenMiktar = float.Parse(miktar_textbox.Text);
                    float hesaplananCoinMiktar = coinMiktarHesapla(coinNO, girilenMiktar);
                    // Coin miktarının üstüne ekleme yapar
                    if (hesaplananCoinMiktar != 0)
                    {
                        baglanti.Close();
                        string sorgu3 = "UPDATE Kullanici_Varliklari SET coin_miktar=@coinMiktar where coin_id=@coinID AND kullanici_id=@id";
                        komut = new SqlCommand(sorgu3, baglanti);
                        komut.Parameters.AddWithValue("@id", Int32.Parse(login.gonderilecekID));
                        komut.Parameters.AddWithValue("@coinID", coinNO);
                        komut.Parameters.AddWithValue("@coinMiktar", hesaplananCoinMiktar);
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        miktar_textbox.Text = "";
                        toplam_textbox.Text = "";
                    }
                    // İlk defa coin alan için
                    else if (hesaplananCoinMiktar == 0)
                    {
                        string sorgu4 = "INSERT INTO Kullanici_Varliklari(kullanici_id,coin_id,coin_miktar) VALUES (@kullanici,@coin,@miktar)";
                        komut = new SqlCommand(sorgu4, baglanti);
                        komut.Parameters.AddWithValue("@kullanici", Int32.Parse(login.gonderilecekID));
                        komut.Parameters.AddWithValue("@coin", coinNO);
                        komut.Parameters.AddWithValue("@miktar", girilenMiktar);
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        miktar_textbox.Text = "";
                        toplam_textbox.Text = "";
                    }

                    MessageBox.Show("Satın Alma İşleminiz Gerçekleşmiştir.");
                    BakiyeGosterGuncelle();
                }
                else MessageBox.Show("Yetersiz Bakiye.");

            }

            // Limitten Satış Yapma
            else if (alSatKodu == 2 && limitStopKodu == 1 && coinNO != 0)
            {
                veriTabani();
                string sorgu = "INSERT INTO islem_log(kullanici_id,coin_id,coin_miktar,coin_fiyat,tutar,islem_kodu) VALUES (@kullanici,@coin,@miktar,@fiyat,@tutar,@islem_kodu)";
                komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@kullanici", Int32.Parse(login.gonderilecekID));
                komut.Parameters.AddWithValue("@coin", coinNO);
                komut.Parameters.AddWithValue("@miktar", float.Parse(miktar_textbox.Text));
                komut.Parameters.AddWithValue("@fiyat", float.Parse(fiyat_textbox.Text));
                komut.Parameters.AddWithValue("@tutar", float.Parse(toplam_textbox.Text));
                komut.Parameters.AddWithValue("@islem_kodu", 2);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                bakiye = bakiye + toplam;
                string sorgu2 = "UPDATE Kullanicilar SET nakit=@nakit where id=@id";
                komut = new SqlCommand(sorgu2, baglanti);
                komut.Parameters.AddWithValue("@id", login.gonderilecekID);
                komut.Parameters.AddWithValue("@nakit", bakiye);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                float girilenMiktar = float.Parse(miktar_textbox.Text);
                float hesaplananCoinMiktar = coinMiktarHesapla(coinNO, girilenMiktar);
                //Coin miktarını azaltır
                if (hesaplananCoinMiktar != 0)
                {
                    baglanti.Close();
                    string sorgu3 = "UPDATE Kullanici_Varliklari SET coin_miktar=@coinMiktar where coin_id=@coinID AND kullanici_id=@id";
                    komut = new SqlCommand(sorgu3, baglanti);
                    komut.Parameters.AddWithValue("@id", Int32.Parse(login.gonderilecekID));
                    komut.Parameters.AddWithValue("@coinID", coinNO);
                    komut.Parameters.AddWithValue("@coinMiktar", hesaplananCoinMiktar);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Satış İşleminiz Gerçekleşmiştir.");
                    BakiyeGosterGuncelle();
                    miktar_textbox.Text = "";
                    toplam_textbox.Text = "";
                }
                // Satış öncesi coin kontrol uyarısı yapar
                else if (hesaplananCoinMiktar == 0)
                {
                    MessageBox.Show("Yetersiz Coine Sahipsiniz.");
                }

            }

            // Stop dan AL emri ver
            else if (alSatKodu == 1 && limitStopKodu == 2 && coinNO != 0)
            {
                if (bakiye >= toplam)
                {
                    veriTabani();
                    string sorgu = "INSERT INTO stop_log(kullanici_id,coin_id,tetiklenen_fiyat,coin_miktar,coin_fiyat,tutar,islem,durum) VALUES (@kullanici,@coin,@tetik,@miktar,@fiyat,@tutar,@islem,@durum)";
                    komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@kullanici", Int32.Parse(login.gonderilecekID));
                    komut.Parameters.AddWithValue("@coin", coinNO);
                    komut.Parameters.AddWithValue("@tetik", float.Parse(tetik_textbox.Text));
                    komut.Parameters.AddWithValue("@miktar", float.Parse(miktar_textbox.Text));
                    komut.Parameters.AddWithValue("@fiyat", float.Parse(fiyat_textbox.Text));
                    komut.Parameters.AddWithValue("@tutar", float.Parse(toplam_textbox.Text));
                    komut.Parameters.AddWithValue("@islem", 1);
                    komut.Parameters.AddWithValue("@durum", 1);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Satın Alma Emriniz Verilmiştir.");
                    miktar_textbox.Text = "";
                    toplam_textbox.Text = "";
                    //Bakiyeden düşer.
                    float yazilacakSonuc = bakiye - toplam;
                    string sorgu2 = "UPDATE Kullanicilar SET nakit=@nakit where id=@id";
                    komut = new SqlCommand(sorgu2, baglanti);
                    komut.Parameters.AddWithValue("@id", Int32.Parse(login.gonderilecekID));
                    komut.Parameters.AddWithValue("@nakit", yazilacakSonuc);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                }
                else MessageBox.Show("Satın Alma Emri için Bakiyeniz Yetersiz.");

            }

            //Stop dan Satış Emri Ver
            else if (alSatKodu == 2 && limitStopKodu == 2 && coinNO != 0)
            {
                float girilenMiktar = float.Parse(miktar_textbox.Text);
                float hesaplananCoinMiktar = coinMiktarHesapla(coinNO, girilenMiktar);
                if (hesaplananCoinMiktar != 0)
                {
                    veriTabani();
                    string sorgu = "INSERT INTO stop_log(kullanici_id,coin_id,tetiklenen_fiyat,coin_miktar,coin_fiyat,tutar,islem,durum) VALUES (@kullanici,@coin,@tetik,@miktar,@fiyat,@tutar,@islem,@durum)";
                    komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@kullanici", Int32.Parse(login.gonderilecekID));
                    komut.Parameters.AddWithValue("@coin", coinNO);
                    komut.Parameters.AddWithValue("@tetik", float.Parse(tetik_textbox.Text));
                    komut.Parameters.AddWithValue("@miktar", float.Parse(miktar_textbox.Text));
                    komut.Parameters.AddWithValue("@fiyat", float.Parse(fiyat_textbox.Text));
                    komut.Parameters.AddWithValue("@tutar", float.Parse(toplam_textbox.Text));
                    komut.Parameters.AddWithValue("@islem", 2);
                    komut.Parameters.AddWithValue("@durum", 1);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    miktar_textbox.Text = "";
                    toplam_textbox.Text = "";
                    //Coin miktarı düşer.
                    string sorgu1 = "UPDATE Kullanici_Varliklari SET coin_miktar=@coinMiktar where coin_id=@coinID AND kullanici_id=@id";
                    komut = new SqlCommand(sorgu1, baglanti);
                    komut.Parameters.AddWithValue("@id", Int32.Parse(login.gonderilecekID));
                    komut.Parameters.AddWithValue("@coinID", coinNO);
                    komut.Parameters.AddWithValue("@coinMiktar", hesaplananCoinMiktar);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Satış Emriniz Verilmiştir.");

                }
                else MessageBox.Show("Satış Emri için Coin Miktarı Yetersiz.");

            }




        }
        // Seçilen coinin id sini takip etme
        int coinNO = 0;
        void coinNumaraTakip()
        {
            if (miktar_coinName.Text == "SDU") coinNO = 1;
            else if (miktar_coinName.Text == "MAKU") coinNO = 2;
            else if (miktar_coinName.Text == "AKU") coinNO = 3;
            else MessageBox.Show("İşlem için Coin Seçiniz.");

        }

        //Coin Miktarını veritabanına yazmadan önce işlemin yapıldığı yer 
        private float coinMiktarHesapla(int gelenCoinID, float gelenCoinMiktar)
        {
            veriTabani();
            string sorgu = "SELECT * FROM Kullanici_Varliklari where kullanici_id=@kullanici AND coin_id=@coin";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kullanici", login.gonderilecekID);
            komut.Parameters.AddWithValue("@coin", gelenCoinID.ToString());
            baglanti.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                String mevcutCoinMiktar = dr["coin_Miktar"].ToString();
                if (alSatKodu==1)
                {
                    float islemCoinMiktar = gelenCoinMiktar + float.Parse(mevcutCoinMiktar);
                    return islemCoinMiktar;
                }
                else if (float.Parse(mevcutCoinMiktar) > gelenCoinMiktar && alSatKodu == 2)
                {
                    float islemCoinMiktar = float.Parse(mevcutCoinMiktar) - gelenCoinMiktar;
                    return islemCoinMiktar;
                }
                else return 0;
                
            }
            baglanti.Close();
            return 0;
        }




        //veri tabanı bağlantı kısayolu
        void veriTabani()
        {
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");
        }


        //Kullanici Adı ve soyad Bilgileri yazdırma
         void KullaniciBilgiGetir()
        {
            KullaniciAdi.Content = login.gonderilecekAd;
            KullaniciSoyadi.Content = login.gonderilecekSoyad;

        }


        //İşlem sonrası bakiye bilgisinin değişimi
        void BakiyeGosterGuncelle()
        {
            veriTabani();
            string sorgu = "select nakit from Kullanicilar where id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@id", login.gonderilecekID);
            baglanti.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Kullanilabilir_Bakiye.Content =Math.Round(float.Parse(dr["nakit"].ToString()),3);
            }
            baglanti.Close();

            }


        //İlk girişte bakiye bilgisi
        void BakiyeGoster()
        {
            Kullanilabilir_Bakiye.Content =Math.Round(float.Parse(login.gonderilecekNakit.ToString()),3);
        }

       
        private void al_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sat.Foreground = Brushes.White;
            al.Foreground = (Brush)new BrushConverter().ConvertFrom("#febc2c");
            alSatKodu = 1;
            miktar_textbox.IsEnabled = false;
            toplam_textbox.IsEnabled = true;

            // islemButon.Background = Brushes.Green;
        }

        private void sat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sat.Foreground = (Brush)new BrushConverter().ConvertFrom("#f84c48");
            al.Foreground = Brushes.White;
            alSatKodu = 2;
            miktar_textbox.IsEnabled = true;
            toplam_textbox.IsEnabled = false;
            //   islemButon.Background = Brushes.Red;
        }

        private void sat_MouseEnter(object sender, MouseEventArgs e)
        {
            //sat.Foreground = Brushes.Red;
        }

        private void sat_MouseLeave(object sender, MouseEventArgs e)
        {
            //sat.Foreground = Brushes.White;
        }

       

        private void limit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            limit_cizgi.Background = Brushes.DeepSkyBlue;
            stop_cizgi.Background = Brushes.Gray;
            tetiklenen.Visibility = Visibility.Hidden;
            limitStopKodu = 1;
        }

        private void stop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            limit_cizgi.Background = Brushes.Gray;
            stop_cizgi.Background = Brushes.DeepSkyBlue;
            tetiklenen.Visibility = Visibility.Visible;
            limitStopKodu = 2;
        }

        private void sdu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            miktar_coinName.Text = sdu.Text.ToString();
            fiyat_textbox.Text = sdu_fiyat.Text.ToString();
            toplam_textbox.Text = "0";
        }

        private void maku_MouseDown(object sender, MouseButtonEventArgs e)
        {
            miktar_coinName.Text = maku.Text.ToString();
            fiyat_textbox.Text = maku_fiyat.Text.ToString();
            toplam_textbox.Text = "0";
        }

        private void otdu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            miktar_coinName.Text = otdu.Text.ToString();
            fiyat_textbox.Text = aku_fiyat.Text.ToString();
            toplam_textbox.Text = "0";
        }


        //Fiyata göre miktar hesaplıyor
        private void toplam_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (alSatKodu == 1)
            {
                miktar_textbox.IsEnabled = false;
                toplam_textbox.IsEnabled = true;

                if (toplam_textbox.Text == "")
                {
                    toplam_textbox.Text = "0";
                }
                float toplam = float.Parse(toplam_textbox.Text);
                float fiyat = float.Parse(fiyat_textbox.Text);
                float sonuc = toplam / fiyat;
                miktar_textbox.Text = sonuc.ToString();
                
            }
            
                               
        }


        //Miktara göre toplam fiyat yazıyor
        private void miktar_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (alSatKodu == 2)
            {
                miktar_textbox.IsEnabled = true;
                toplam_textbox.IsEnabled = false;

                if (miktar_textbox.Text == "")
                {
                    miktar_textbox.Text = "0";
                }

                float miktar = float.Parse(miktar_textbox.Text);
                float fiyat = float.Parse(fiyat_textbox.Text);
                float sonuc = miktar * fiyat;
                toplam_textbox.Text = sonuc.ToString();

            }
            

        }

        private void buton_kapat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void menuButon_piyasa_Click(object sender, RoutedEventArgs e)
        {
            coinDegerBelirle1();
            coinDegerBelirle2();
            coinDegerBelirle3();
            coinDegisim1();
            coinDegisim2();
            coinDegisim3();
            //coinDegerleriniGunceller(1);
            //coinDegerleriniGunceller(2);
            //coinDegerleriniGunceller(3);
            ControlCagir.Control_Ekle(OrtaPanelIcerik, new DenetimPiyasa());
        }

        private void menuButon_varliklar_Click(object sender, RoutedEventArgs e)
        {
            ControlCagir.Control_Ekle(OrtaPanelIcerik, new DenetimVarliklarim());

        }

        private void menuButon_hesapDetay_Click(object sender, RoutedEventArgs e)
        {
            ControlCagir.Control_Ekle(OrtaPanelIcerik, new DenetimHesapDetaylarim());

        }

        private void menuButon_karZarar_Click(object sender, RoutedEventArgs e)
        {
            ControlCagir.Control_Ekle(OrtaPanelIcerik, new DenetimKarZarar());

        }

        private void menuButon_bankaHesap_Click(object sender, RoutedEventArgs e)
        {
            ControlCagir.Control_Ekle(OrtaPanelIcerik, new DenetimBankaHesaplarim());

        }

        private void menuButon_sifreDegistir_Click(object sender, RoutedEventArgs e)
        {
            ControlCagir.Control_Ekle(OrtaPanelIcerik, new DenetimSifreDegistir());

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ControlCagir.Control_Ekle(OrtaPanelIcerik, new DenetimPiyasa());

        }

        private void butonSimgeDurumu_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButonAcilirMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ButonAcilirMenu.Width != 50)
            {
                GridLength grd = new GridLength(80, GridUnitType.Pixel);
                GridLength grd2 = new GridLength(964, GridUnitType.Pixel);

                MenuGenislik.Width = grd;
                OrtaPanelGenislik.Width = grd2;
                MenuYazi1.Visibility = Visibility.Hidden;
                MenuYazi2.Visibility = Visibility.Hidden;
                MenuYazi3.Visibility = Visibility.Hidden;
                MenuYazi4.Visibility = Visibility.Hidden;
                MenuYazi5.Visibility = Visibility.Hidden;
                MenuYazi6.Visibility = Visibility.Hidden;
                MenuYazi7.Visibility = Visibility.Hidden;
                LogoYazi.Width = 0;
                ButonAcilirMenu.Width = 50;
            }
            else
            {
                GridLength grd = new GridLength(250, GridUnitType.Pixel);
                GridLength grd2 = new GridLength(794, GridUnitType.Pixel);

                MenuGenislik.Width = grd;
                OrtaPanelGenislik.Width = grd2;
                MenuYazi1.Visibility = Visibility.Visible;
                MenuYazi2.Visibility = Visibility.Visible;
                MenuYazi3.Visibility = Visibility.Visible;
                MenuYazi4.Visibility = Visibility.Visible;
                MenuYazi5.Visibility = Visibility.Visible;
                MenuYazi6.Visibility = Visibility.Visible;
                MenuYazi7.Visibility = Visibility.Visible;
                LogoYazi.Width = 100;
                ButonAcilirMenu.Width = 60;
            }
        }







        private void islemYapButon_Click(object sender, RoutedEventArgs e)
        {
            islemYap();
            BakiyeGosterGuncelle();

        }

        private void LogoYazi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            KullaniciBilgiGetir();
            BakiyeGosterGuncelle();
        }

        private void ButonKontrol()
        {
            if (login.gonderilecekDurum.Trim() =="0") islemYapButon.IsEnabled = false;
        }

        /* private void coinDegerleriniGunceller(int id)
            {
                veriTabani();
                string sorgu = "UPDATE Coinler SET degeri=@deger where coin_id=@id";
                komut = new SqlCommand(sorgu, baglanti);
                if (id==1)
                {
                    komut.Parameters.AddWithValue("@deger",decimal.Parse(sdu_fiyat.Text));
                    komut.Parameters.AddWithValue("@id", id);
                }
                else if (id == 2)
                {
                    komut.Parameters.AddWithValue("@deger", decimal.Parse(maku_fiyat.Text));
                    komut.Parameters.AddWithValue("@id", id);
                }
                else if (id == 3)
                {
                    komut.Parameters.AddWithValue("@deger", decimal.Parse(aku_fiyat.Text));
                    komut.Parameters.AddWithValue("@id", id);
                }

                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
            }  */

        int saniye = 0;
        DispatcherTimer timer;

        public void timer_Tick(object sender, EventArgs e)
        {
            saniye++;
            if (saniye == 10)
            {
                timer.Stop();
                rastgele();
                rastgeleYukle(1, Convert.ToSingle(rSDU));
                rastgeleYukle(2, Convert.ToSingle(rMAKU));
                rastgeleYukle(3, Convert.ToSingle(rAKU));
                MessageBox.Show("Coin Değerleri Güncellenmiştir.");
            }
            timer.Start();
            
        }

        double rSDU;
        double rMAKU;
        double rAKU;

        public void rastgele()
        {
            Random rnd = new Random();
             rSDU = rnd.NextDouble();
             rMAKU = rnd.NextDouble();
             rAKU = rnd.NextDouble();
        }

        private void rastgeleYukle(int coinID , float deger)
        {
            veriTabani();
            string sorgu = "INSERT INTO Coin_Degeri(coin_id, coin_degeri) VALUES (@cid, @deger)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@cid", coinID);
            komut.Parameters.AddWithValue("@deger", deger);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        private void timerCalistir()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
    }
}
