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
    /// DenetimKarZarar.xaml etkileşim mantığı
    /// </summary>
    public partial class DenetimKarZarar : UserControl
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        SqlDataReader dr;


        public DenetimKarZarar()
        {
            InitializeComponent();
            SDUyazdir();
            MAKUyazdir();
            AKUyazdir();
            karZararBar();
        }

        // SDU Sütununu Doldur
        void SDUyazdir()
        {
            //Satın Alınan SDU
            float SatinAlinanSDU = Hesapla(1, 1);

            //Satılan SDU            
            float SatilanSDU = Hesapla(1, 2);

            if (SatinAlinanSDU > SatilanSDU)
            {
                SatinAlinanSDU = SatinAlinanSDU - SatilanSDU;
                SduKAR_txt.Content = "-";
                SduZARAR_txt.Content = SatinAlinanSDU.ToString();
            }
            else if (SatinAlinanSDU <= SatilanSDU)
            {
                SatinAlinanSDU = SatilanSDU - SatinAlinanSDU;
                SduKAR_txt.Content = SatinAlinanSDU.ToString();
                SduZARAR_txt.Content = "-";
            }
            if((Hesapla(1,1)==1 && Hesapla(1, 2) == 1 ) || (Hesapla(1, 1) == 0 && Hesapla(1, 2) == 1))
            {
                SduKAR_txt.Content = "-"; 
                SduZARAR_txt.Content = "-";
            }
            


        }

        // MAKU sütununu Doldur
        void MAKUyazdir()
        {
            //Satın Alınan MAKU
            float SatinAlinanSDU = Hesapla(2, 1);

            //Satılan MAKU            
            float SatilanSDU = Hesapla(2, 2);

            if (SatinAlinanSDU > SatilanSDU)
            {
                SatinAlinanSDU = SatinAlinanSDU - SatilanSDU;
                makuKAR_txt.Content = "-";
                makuZARAR_txt.Content = SatinAlinanSDU.ToString();
            }
            else if (SatinAlinanSDU <= SatilanSDU)
            {
                SatinAlinanSDU = SatilanSDU - SatinAlinanSDU;
                makuKAR_txt.Content = SatinAlinanSDU.ToString();
                makuZARAR_txt.Content = "-";
            }
            if(Hesapla(2, 1) == 1 && Hesapla(2, 2) == 1 || (Hesapla(2, 1) == 0 && Hesapla(2, 2) == 1))
            {
                makuKAR_txt.Content = "-";
                makuZARAR_txt.Content = "-";
            }
        }


        //AKU Sütununu Doldur
        void AKUyazdir()
        {
            //Satın Alınan AKU
            float SatinAlinanSDU = Hesapla(3, 1);

            //Satılan AKU            
            float SatilanSDU = Hesapla(3, 2);

            if (SatinAlinanSDU > SatilanSDU)
            {
                SatinAlinanSDU = SatinAlinanSDU - SatilanSDU;
                akuKar_txt.Content = "-";
                akuZARAR_txt.Content = SatinAlinanSDU.ToString();
            }
            else if (SatinAlinanSDU <= SatilanSDU)
            {
                SatinAlinanSDU = SatilanSDU - SatinAlinanSDU;
                akuKar_txt.Content = SatinAlinanSDU.ToString();
                akuZARAR_txt.Content = "-";
            }
            if (Hesapla(3, 1) == 1 && Hesapla(3, 2) == 1 || (Hesapla(3, 1) == 0 && Hesapla(3, 2) == 1))
            {
                akuKar_txt.Content = "-";
                akuZARAR_txt.Content = "-";
            }
        }





        private float Hesapla(int coinID, int islemK)
        {
            veriTabani();
            string sorgu = "select SUM(tutar) AS toplam from islem_log where kullanici_id = @kullanici AND coin_id = @coin AND islem_kodu = @islem";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kullanici", login.gonderilecekID);
            komut.Parameters.AddWithValue("@coin", coinID);
            komut.Parameters.AddWithValue("@islem", islemK);
            baglanti.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                if (dr["toplam"].ToString()=="")
                {
                    return 1;
                }
                else
                {

                    float mevcutCoinMiktar = float.Parse(dr["toplam"].ToString());
                    return mevcutCoinMiktar;
                }
            }
            return 0;
        }

        private void karZararBar()
        {
            //SDU BAR
           
            if (SduKAR_txt.Content=="-" || SduZARAR_txt.Content == "-")
            {
                sdu_karBar.Value = 0;
            }
            if (SduKAR_txt.Content != "-")
            {
                float yuzde =( float.Parse((string)SduKAR_txt.Content) ) / (float.Parse(login.gonderilecekNakit));
                yuzde = yuzde * 100;
                sdu_karBar.Value = yuzde;
                sdu_karBar.Foreground = (Brush)new BrushConverter().ConvertFrom("#febc2c");
            }
            if (SduZARAR_txt.Content != "-")
            {
                float yuzde = (float.Parse((string)SduZARAR_txt.Content)) / (float.Parse(login.gonderilecekNakit));
                yuzde = yuzde * 100;
                sdu_karBar.Value = yuzde;
                sdu_karBar.Foreground = (Brush)new BrushConverter().ConvertFrom("#f84c48");
            }


            //MAKU BAR
            if (makuKAR_txt.Content == "-" && makuZARAR_txt.Content == "-")
            {
                maku_karBar.Value = 0;
            }
            if (makuKAR_txt.Content != "-")
            {
                float yuzde = (float.Parse((string)makuKAR_txt.Content)) / (float.Parse(login.gonderilecekNakit));
                yuzde = yuzde * 100;
                maku_karBar.Value = yuzde;
                maku_karBar.Foreground = (Brush)new BrushConverter().ConvertFrom("#febc2c");
            }
            if (makuZARAR_txt.Content != "-")
            {
                float yuzde = (float.Parse((string)makuZARAR_txt.Content)) / (float.Parse(login.gonderilecekNakit));
                yuzde = yuzde * 100;
                maku_karBar.Value = yuzde;
                maku_karBar.Foreground = (Brush)new BrushConverter().ConvertFrom("#f84c48");
            }
            //AKU BAR
            if (akuKar_txt.Content == "-" && akuZARAR_txt.Content == "-")
            {
                aku_karBar.Value = 0;
            }
            if (akuKar_txt.Content != "-")
            {
                float yuzde = (float.Parse((string)akuKar_txt.Content)) / (float.Parse(login.gonderilecekNakit));
                yuzde = yuzde * 100;
                aku_karBar.Value = yuzde;
                aku_karBar.Foreground = (Brush)new BrushConverter().ConvertFrom("#febc2c");
            }
            if (akuZARAR_txt.Content != "-")
            {
                float yuzde = (float.Parse((string)akuZARAR_txt.Content)) / (float.Parse(login.gonderilecekNakit));
                yuzde = yuzde * 100;
                aku_karBar.Value = yuzde;
                aku_karBar.Foreground = (Brush)new BrushConverter().ConvertFrom("#f84c48");
            }

        }







        void veriTabani()
        {
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");
        }

    }
}
