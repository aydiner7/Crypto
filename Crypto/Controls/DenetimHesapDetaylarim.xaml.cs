using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Crypto.Controls
{
    /// <summary>
    /// DenetimHesapDetaylarim.xaml etkileşim mantığı
    /// </summary>
    public partial class DenetimHesapDetaylarim : UserControl
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        SqlDataReader dr;


        public DenetimHesapDetaylarim()
        {
            InitializeComponent();
            bilgiGetir();
            Bloke();
            textboxSinirlama();
        }

        private string durum;
        private string durumGuncel ="1";


        void bilgiGetir()
        {
            HesapDetayAd.Text = login.gonderilecekAd.Trim();
            HesapDetayKullaniciAdi.Text = login.gonderilecekKullaniciAdi.Trim();
            HesapDetaySoyad.Text = login.gonderilecekSoyad.Trim();
            HesapDetayKimlik.Text = login.gonderilecekKimlik.Trim();
            HesapDetayEmail.Text = login.gonderilecekEmail.Trim();
            HesapDetayTelefon.Text = login.gonderilecekTelefon.Trim();
            Adres.Text = login.gonderilecekAdres.Trim();
            durum = login.gonderilecekDurum;

        }



        //Erişebilirlik düzenle
        void Bloke()
        {
            if (HesapDetayAd.Text != "")
            {
               HesapDetayAd.IsEnabled = false;
            }
            if (HesapDetaySoyad.Text != "")
            {
                HesapDetaySoyad.IsEnabled = false;
            }
            if (HesapDetayKimlik.Text != "")
            {
                HesapDetayKimlik.IsEnabled = false;
            }
            if (HesapDetayTelefon.Text != "")
            {
                HesapDetayTelefon.IsEnabled = false;
            }
            if (HesapDetayKullaniciAdi.Text != "")
            {
                HesapDetayKullaniciAdi.IsEnabled = false;
            }
            if (HesapDetayEmail.Text != "")
            {
                HesapDetayEmail.IsEnabled = false;
            }
                if (durum.Trim() == "1")
                {
                    DurumText.Content = "Aktif";
                    DurumText.Foreground = Brushes.Green;
                }
                else { 
                    DurumText.Content = "Pasif";
                    DurumText.Foreground = Brushes.Red;    
                }

            if (HesapDetayAd.Text != "" && HesapDetaySoyad.Text != "" && HesapDetayKimlik.Text != "" && HesapDetayTelefon.Text != "")
            {
                Hesap_Aktif_Buton.IsEnabled = false;
            }
        }

        private void Hesap_Aktif_Buton_Click(object sender, RoutedEventArgs e)
        {
            String bakiye = "1500";

            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");


            if (HesapDetayAd.Text != "" && HesapDetaySoyad.Text != "" && HesapDetayKimlik.Text != "" && HesapDetayTelefon.Text != "")
            {
                if (HesapDetayTelefon.Text.Length == 10 && HesapDetayKimlik.Text.Length == 11)
                {
                    string sorgu = "UPDATE Kullanicilar SET durum=@durum , ad=@kuladi , soyad=@kulsoyadi , kimlikNo=@kulkimlik , telefonNo=@kultel , nakit=@bakiye where id=@id";
                    komut = new SqlCommand(sorgu, baglanti);

                    komut.Parameters.AddWithValue("@durum", durumGuncel);
                    komut.Parameters.AddWithValue("@kuladi", HesapDetayAd.Text);
                    komut.Parameters.AddWithValue("@kulsoyadi", HesapDetaySoyad.Text);
                    komut.Parameters.AddWithValue("@kulkimlik", HesapDetayKimlik.Text);
                    komut.Parameters.AddWithValue("@kultel", HesapDetayTelefon.Text);
                    komut.Parameters.AddWithValue("@bakiye", bakiye);
                    komut.Parameters.AddWithValue("@id", login.gonderilecekID);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    DurumText.Content = "Aktif";
                    DurumText.Foreground = Brushes.Green;
                    MessageBox.Show("Hesap Aktif Edildi, Lütfen Tekrar Giriş Yapınız.");
                    Hesap_Aktif_Buton.IsEnabled = false;
                    Environment.Exit(0); // Uygulamayı Hatasız Kapatır.
                }
                else MessageBox.Show("Geçersiz Kimlik Numarası veya Telefon Numarasını Girdiniz.");

            }
            else
            {
                MessageBox.Show("Hesap Aktif Edilemedi, Lütfen bilgilerinizi eksiksiz giriniz.");

            }
        }

        private void Adres_Guncelle_Buton_Click(object sender, RoutedEventArgs e)
        {
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");

            string sorgu = "UPDATE Kullanicilar SET adres=@adres where id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@adres", Adres.Text);
            komut.Parameters.AddWithValue("@id", login.gonderilecekID);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Adres Bilgileriniz Güncellenmiştir.");

        }

        private void HesapDetayAd_KeyDown(object sender, KeyEventArgs e)
        {
            int sayi = HesapDetayAd.Text.Length;
            if (sayi == 15) MessageBox.Show("Maksimum 15 karakter Girebilirsiniz.");
        }

        private void HesapDetaySoyad_KeyDown(object sender, KeyEventArgs e)
        {
            int sayi = HesapDetaySoyad.Text.Length;
            if (sayi == 15) MessageBox.Show("Maksimum 15 karakter Girebilirsiniz.");
        }              


        private void textboxSinirlama()
        {
                HesapDetayAd.MaxLength = 15;
                HesapDetaySoyad.MaxLength = 15;
                HesapDetayKimlik.MaxLength = 11;
                HesapDetayTelefon.MaxLength = 10;
        }

        private static readonly Regex DigitRegex = new Regex("[^0-9,-]+");

        private void HesapDetayKimlik_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DigitRegex.IsMatch(HesapDetayKimlik.Text)) HesapDetayKimlik.Background = Brushes.DarkRed;
            else HesapDetayKimlik.Background = (Brush)new BrushConverter().ConvertFrom("#FF3D434B");
        }

        private void HesapDetayTelefon_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DigitRegex.IsMatch(HesapDetayTelefon.Text)) HesapDetayTelefon.Background = Brushes.DarkRed;
            else HesapDetayTelefon.Background = (Brush)new BrushConverter().ConvertFrom("#FF3D434B");
        }

        private void HesapDetayAd_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        
        private void HesapDetaySoyad_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
