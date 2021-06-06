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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Crypto.Controls
{
    /// <summary>
    /// DenetimVarliklarim.xaml etkileşim mantığı
    /// </summary>
    public partial class DenetimVarliklarim : UserControl
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        SqlDataReader dr;


        public DenetimVarliklarim()
        {
            InitializeComponent();
            SduCoinGetir();
            AkuCoinGetir();
            MakuCoinGetir();
            TryGetir();


            double SDUGrafik = double.Parse(VarlikBirimSDUTry.Content.ToString());
            double MAKUGrafik = double.Parse(VarlikBirimMAKUTry.Content.ToString());
            double AKUGrafik = double.Parse(VarlikBirimAKUTry.Content.ToString());
            double LiraGrafik = double.Parse(VarlikBirimLiraTRY.Content.ToString());

            SDUGrafik = Math.Round(SDUGrafik, 3);
            MAKUGrafik = Math.Round(MAKUGrafik, 3);
            AKUGrafik = Math.Round(AKUGrafik, 3);
            LiraGrafik = Math.Round(LiraGrafik, 3);

            SeriesCollection = new SeriesCollection {
                new PieSeries
                {

                    Title = "Türk Lirası (TRY)",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(LiraGrafik) },
                    DataLabels = true,
                    FontSize = 25

                },

                new PieSeries
                {
                    Title = "SDU Coin (TRY)",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(SDUGrafik) },
                    DataLabels = true,
                    FontSize = 25
                },

                new PieSeries
                {
                    Title = "Maku Coin (TRY)",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(MAKUGrafik) },
                    DataLabels = true,
                    FontSize = 25
                },

                new PieSeries
                {
                    Title = "AKU Coin (TRY)",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(AKUGrafik) },
                    DataLabels = true,
                    FontSize = 25
                }

            };
            DataContext = this;
        }

        void SduCoinGetir()
        {
            int coinNO = 1;
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");

            string sorgu = "SELECT * FROM Kullanici_Varliklari kv, Coinler c where kullanici_id=@kullanici_id AND c.coin_id=@coin AND c.coin_id = kv.coin_id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kullanici_id", login.gonderilecekID);
            komut.Parameters.AddWithValue("@coin", coinNO);
            baglanti.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                VarlikBirimSDU.Content = dr["adi"].ToString();
                VarlikBirimSDUadet.Content = dr["coin_miktar"].ToString();
                VarlikBirimSDUadet.Content = Math.Round( double.Parse((string)VarlikBirimSDUadet.Content),3);

                String SduMiktar = dr["coin_miktar"].ToString();
                String SduDeger = dr["degeri"].ToString();
                double deger = double.Parse(SduDeger);
                double miktar = double.Parse(SduMiktar);
                double SDUislem = deger * miktar;

                VarlikBirimSDUTry.Content = SDUislem.ToString();
                VarlikBirimSDUTry.Content = Math.Round(double.Parse((string)VarlikBirimSDUTry.Content), 3);

            }
            else VarlikBirimSDUTry.Content = "0";

        
            
        }

        void MakuCoinGetir()
        {
            int coinNO = 2;
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");

            string sorgu = "SELECT * FROM Kullanici_Varliklari kv, Coinler c where kullanici_id=@kullanici_id AND c.coin_id=@coin AND c.coin_id = kv.coin_id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kullanici_id", login.gonderilecekID);
            komut.Parameters.AddWithValue("@coin", coinNO);
            baglanti.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                VarlikBirimMAKU.Content = dr["adi"].ToString();
                VarlikBirimMAKUadet.Content = dr["coin_miktar"].ToString();
                VarlikBirimMAKUadet.Content = Math.Round(double.Parse((string)VarlikBirimMAKUadet.Content), 3);

                String MakuMiktar = dr["coin_miktar"].ToString();
                String MakuDeger = dr["degeri"].ToString();
                double deger = double.Parse(MakuDeger);
                double miktar = double.Parse(MakuMiktar);
                double MAKUislem = deger * miktar;

                VarlikBirimMAKUTry.Content = MAKUislem.ToString();
                VarlikBirimMAKUTry.Content = Math.Round(double.Parse((string)VarlikBirimMAKUTry.Content), 3);


            }
            else VarlikBirimMAKUTry.Content = "0";
        }

        void AkuCoinGetir()
        {
            int coinNO = 3;
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");

            string sorgu = "SELECT * FROM Kullanici_Varliklari kv, Coinler c where kullanici_id=@kullanici_id AND c.coin_id=@coin AND c.coin_id = kv.coin_id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kullanici_id", login.gonderilecekID);
            komut.Parameters.AddWithValue("@coin", coinNO);
            baglanti.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                VarlikBirimAKU.Content = dr["adi"].ToString();
                VarlikBirimAKUadet.Content = dr["coin_miktar"].ToString();
                VarlikBirimAKUadet.Content = Math.Round(double.Parse((string)VarlikBirimAKUadet.Content), 3);

                String AkuMiktar = dr["coin_miktar"].ToString();
                String AkuDeger = dr["degeri"].ToString();
                double deger = double.Parse(AkuDeger);
                double miktar = double.Parse(AkuMiktar);
                double AKUislem = deger * miktar;

                VarlikBirimAKUTry.Content = AKUislem.ToString();
                VarlikBirimAKUTry.Content = Math.Round(double.Parse((string)VarlikBirimAKUTry.Content), 3);

            }
            else VarlikBirimAKUTry.Content = "0";

        }


        void TryGetir()
        {

            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");

            string sorgu = "select nakit from Kullanicilar where id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@id", login.gonderilecekID);
            baglanti.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                VarlikBirimLiraTRY.Content = dr["nakit"].ToString();

            }

        }




        public SeriesCollection SeriesCollection { get; set; }
    }
}
