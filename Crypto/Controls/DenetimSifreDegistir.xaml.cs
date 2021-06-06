using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Crypto.Controls
{
    /// <summary>
    /// DenetimSifreDegistir.xaml etkileşim mantığı
    /// </summary>
    public partial class DenetimSifreDegistir : UserControl
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        SqlDataReader dr;

        String gelenSifre = login.gonderilecekSifre;
        

        public DenetimSifreDegistir()
        {
            InitializeComponent();
        }

        private void SifreDegistir_Buton_Click(object sender, RoutedEventArgs e)
        {

            if (gelenSifre.Trim() == MevcutSifre.Password && yeniSifre1.Password == yeniSifre2.Password)
            {

                baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");

                string sorgu = "UPDATE Kullanicilar SET sifre=@sifre where id=@id";
                komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@sifre", yeniSifre1.Password);
                komut.Parameters.AddWithValue("@id", login.gonderilecekID);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Şifreniz Güncellenmiştir.");

                MevcutSifre.Password = "";
                yeniSifre1.Password = "";
                yeniSifre2.Password = "";

            }
            else
            {
                MessageBox.Show(gelenSifre);
            }
        }
    }
}
