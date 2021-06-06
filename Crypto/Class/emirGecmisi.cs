using Crypto.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Crypto.Class
{
    class emirGecmisi
    {


        public DateTime tarih { get; set; }
        public float miktar { get; set; }
        public float tutar { get; set; }


        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        SqlDataReader dr;

        int coinNO;
        int kulNO;

        public emirGecmisi(int coin, int kullanici)
        {
            coinNO = coin;
            kulNO = kullanici;
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");
        }
        // Emir Defterine yazılacak liste ( KISITLI ) * top 6
        public List<Emirler> emirListesi()
        {
            baglanti.Open();
            List<Emirler> liste = new List<Emirler>();
            string sorgu = ("select top 6 coin_miktar, coin_fiyat, tutar, islem, durum, iptal from stop_log where kullanici_id=@kid AND coin_id=@cid AND durum=@durum order by id desc");
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kid", kulNO);
            komut.Parameters.AddWithValue("@cid", coinNO);
            komut.Parameters.AddWithValue("@durum", 1);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                Emirler emir = new Emirler();
                emir.miktar =oku["coin_miktar"].ToString();
                emir.fiyat = oku["coin_fiyat"].ToString();
                emir.tutar = oku["tutar"].ToString();
                emir.islem = oku["islem"].ToString();
                emir.durum = oku["durum"].ToString();
                emir.iptal = oku["iptal"].ToString();
                if (oku["islem"].ToString() == "1")
                {
                    emir.renk = Colors.DarkGoldenrod;
                }
                else if (oku["islem"].ToString() == "2")
                {
                    emir.renk = Colors.Red;
                }
                liste.Add(emir);
            }
            oku.Close();
            baglanti.Close();
            return liste;

        }

        // Tüm emir defterini getirir. ( KISITSIZ )
        public List<Emirler> emirListesiTum()
        {
            baglanti.Open();
            List<Emirler> liste = new List<Emirler>();
            string sorgu = ("select tetiklenen_fiyat, coin_miktar, coin_fiyat, tutar, islem, durum, iptal from stop_log where kullanici_id=@kid AND coin_id=@cid order by id desc");
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kid", kulNO);
            komut.Parameters.AddWithValue("@cid", coinNO);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                Emirler emir = new Emirler();
                emir.tektiklenenFiyat = oku["tetiklenen_fiyat"].ToString();
                emir.miktar = oku["coin_miktar"].ToString();
                emir.fiyat = oku["coin_fiyat"].ToString();
                emir.tutar = oku["tutar"].ToString();
                emir.islem = oku["islem"].ToString();
                emir.durum = oku["durum"].ToString();
                emir.iptal = oku["iptal"].ToString();                
                liste.Add(emir);
            }
            oku.Close();
            baglanti.Close();
            return liste;

        }





        public void emirIptalIslem()
        {

        }





    }
}
