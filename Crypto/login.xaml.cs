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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Net;

namespace Crypto
{
    /// <summary>
    /// login.xaml etkileşim mantığı
    /// </summary>
    public partial class login : Window
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        SqlDataReader dr;

        public login()
        {
            kullanicilariGetir();
            InitializeComponent();
            textBoxSinirlama();
        }

        public static String gonderilecekAd;
        public static String gonderilecekSoyad;
        public static String gonderilecekKullaniciAdi;
        public static String gonderilecekEmail;
        public static String gonderilecekSifre;
        public static String gonderilecekKimlik;
        public static String gonderilecekTelefon;
        public static String gonderilecekAdres;
        public static String gonderilecekID;
        public static String gonderilecekDurum;
        public static String gonderilecekNakit;




        void kullanicilariGetir()
        {
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");
            baglanti.Open();
            da = new SqlDataAdapter(" SELECT * FROM  Kullanicilar", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            baglanti.Close();
        }


        int saniye = 0;
        DispatcherTimer timer;

        public void timer_Tick(object sender, EventArgs e)
        {
            saniye++;
            if (saniye==3)
            {
                timer.Stop();
                MainWindow anasayfa = new MainWindow();
                anasayfa.Show();
                this.Close();
            }
        }


        //Giriş yap Butonu
        [Obsolete]
        private void btnOturumAc_Click(object sender, RoutedEventArgs e)
        {

            string sorgu = "SELECT * FROM Kullanicilar where kullaniciAdi=@kullaniciAdi AND sifre=@sifre";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kullaniciAdi", Giris_KullaniciAdi.Text);
            komut.Parameters.AddWithValue("@sifre", Giris_Sifre.Password);
            baglanti.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                gonderilecekAd = dr["ad"].ToString();
                gonderilecekSoyad = dr["soyad"].ToString();
                gonderilecekKullaniciAdi = dr["kullaniciAdi"].ToString();
                gonderilecekEmail = dr["eMail"].ToString();
                gonderilecekSifre = dr["sifre"].ToString();
                gonderilecekKimlik = dr["kimlikNo"].ToString();
                gonderilecekTelefon = dr["telefonNo"].ToString();
                gonderilecekAdres = dr["adres"].ToString();
                gonderilecekID = dr["id"].ToString();
                gonderilecekDurum = dr["durum"].ToString();
                gonderilecekNakit = dr["nakit"].ToString();

                kullanici_Log();

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
            }
            else
            {
                MessageBox.Show("Bilgilerinizi kontrol edip, tekrar deneyiniz.");
            }
        }

        //Hesap Aç / Yeni kullanici Butonu
        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            if (HesapAc_Sifre.Text == HesapAc_SifreTekrar.Text)
            {
                if (eMail.Text.Contains("@") == true && eMail.Text.Contains(".com")==true)
                {
                    string sorgu = "INSERT INTO Kullanicilar(kullaniciAdi,eMail,sifre) VALUES (@kullaniciAdi,@eMail,@sifre)";
                    komut = new SqlCommand(sorgu, baglanti);

                    komut.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi.Text);
                    komut.Parameters.AddWithValue("@eMail", eMail.Text);
                    komut.Parameters.AddWithValue("@sifre", HesapAc_Sifre.Text);
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    MessageBox.Show("Hesabınız Oluşturuldu.");
                    kullaniciAdi.Text = "Kullanıcı Adı";
                    eMail.Text = "E-Posta";
                    HesapAc_Sifre.Text = "Şifre";
                    HesapAc_SifreTekrar.Text = "Şifre Onay";
                }
                else MessageBox.Show("Lütfen Geçerli E-Posta Hesabı Giriniz.");
            }
            else
            {
                MessageBox.Show("Hatalı Bilgi Girdiniz, Lütfen tekrar deneyiniz.");
            }
        }

        //Kullanıcı Log Girişi
        [Obsolete]
        void kullanici_Log()
        {
            baglanti.Close();
            string bilgisayarAdi = Dns.GetHostName();
            string ipAdresi = Dns.GetHostByName(bilgisayarAdi).AddressList[0].ToString();

            string sorgu = "INSERT INTO Kullanici_Log(kullanici_id,kullanici_IP,kullanici_bilgisayarAdi) VALUES (@kullanici,@ip,@pcAdi)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kullanici", gonderilecekID);
            komut.Parameters.AddWithValue("@ip", ipAdresi);
            komut.Parameters.AddWithValue("@pcAdi", bilgisayarAdi);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        private string gelenSifre;

        private void txtSifreKontrol()
        {
            baglanti.Close();
            string sorgu = "SELECT sifre FROM Kullanicilar where kullaniciAdi=@kullaniciAdi";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kullaniciAdi", Giris_KullaniciAdi.Text);
            baglanti.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                gelenSifre = dr["sifre"].ToString().Trim();
            }
            dr.Close();
            baglanti.Close();
        }

        private void Giris_KullaniciAdi_TextChanged(object sender, TextChangedEventArgs e)
        {            
            txtSifreKontrol();
        }



        private void Giris_Sifre_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (gelenSifre == null) Giris_Sifre.Foreground = Brushes.DarkRed;
            else if (gelenSifre == Giris_Sifre.Password) Giris_Sifre.Foreground = Brushes.White;
            else if (gelenSifre[0] == Giris_Sifre.Password[0] && Giris_Sifre.Password.Length>=1) Giris_Sifre.Foreground = Brushes.Gold;
            else if (gelenSifre != Giris_Sifre.Password) Giris_Sifre.Foreground = Brushes.DarkRed;


            int sayi = Giris_Sifre.Password.Length;
            if (sayi == 15) MessageBox.Show("Maksimum 15 karakter Girebilirsiniz.");


        }

        private void Giris_KullaniciAdi_KeyDown(object sender, KeyEventArgs e)
        {
            if (Giris_KullaniciAdi.Text == "Kullanıcı Adı") Giris_KullaniciAdi.Text = "";
            int sayi = Giris_KullaniciAdi.Text.Length;
            if (sayi == 15) MessageBox.Show("Maksimum 15 karakter Girebilirsiniz.");
        }

        private void kullaniciAdi_KeyDown(object sender, KeyEventArgs e)
        {
            if (kullaniciAdi.Text == "Kullanıcı Adı") kullaniciAdi.Text = "";
            int sayi = kullaniciAdi.Text.Length;
            if (sayi == 15) MessageBox.Show("Maksimum 15 karakter Girebilirsiniz.");
        }

        private void eMail_KeyDown(object sender, KeyEventArgs e)
        {
            if (eMail.Text == "E-Posta") eMail.Text = ""; 
            int sayi = eMail.Text.Length;
            if (sayi == 30) MessageBox.Show("Maksimum 30 karakter Girebilirsiniz.");
        }

        private void HesapAc_SifreTekrar_KeyDown(object sender, KeyEventArgs e)
        {
            if (HesapAc_SifreTekrar.Text == "Şifre Onay") HesapAc_SifreTekrar.Text = "";
            int sayi = HesapAc_SifreTekrar.Text.Length;
            if (sayi == 15) MessageBox.Show("Maksimum 15 karakter Girebilirsiniz.");
        }

        private void HesapAc_Sifre_KeyDown(object sender, KeyEventArgs e)
        {
            if (HesapAc_Sifre.Text == "Şifre") HesapAc_Sifre.Text = "";
            int sayi = HesapAc_Sifre.Text.Length;
            if (sayi == 15) MessageBox.Show("Maksimum 15 karakter Girebilirsiniz.");
        }
        
        private void textBoxSinirlama()
        {
            Giris_KullaniciAdi.MaxLength = 15;
            Giris_Sifre.MaxLength = 15;
            kullaniciAdi.MaxLength =15;
            eMail.MaxLength = 30;
            HesapAc_Sifre.MaxLength = 15;
            HesapAc_SifreTekrar.MaxLength = 15;
        }

        //Formun içinde Enter Tuşuna Basıldığında
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Oturum aç buton içerisine IsDefault="true" tanımlandı.
            }
        }
    }
}
