using Crypto.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Crypto.Class
{
    class islemGecmisi
    {

        public DateTime tarih { get; set; }
        public float miktar { get; set; }
        public float tutar { get; set; }


        SqlConnection baglanti;
        SqlCommand komut;

        int coinNO;
        int kulNO;

        public islemGecmisi(int coin, int kullanici)
        {
            coinNO = coin;
            kulNO = kullanici;
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");
        }

       
         public List<Coinler> islemListesi()
         {
            baglanti.Open();
            List<Coinler> liste = new List<Coinler>();
            string sorgu = ("select top 6 CONVERT(VARCHAR, tarih, 8) AS tarih, coin_miktar, coin_fiyat,islem_kodu from islem_log where kullanici_id=@kid AND coin_id=@cid order by id desc");
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kid", kulNO);
            komut.Parameters.AddWithValue("@cid", coinNO);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                Coinler coin = new Coinler();
                coin.tarih = oku["tarih"].ToString();
                coin.miktar = oku["coin_miktar"].ToString();
                coin.fiyat = oku["coin_fiyat"].ToString();
                if (oku["islem_kodu"].ToString() == "1")
                {
                    coin.renk = Colors.DarkGoldenrod;
                }
                else if (oku["islem_kodu"].ToString() == "2")
                {
                    coin.renk = Colors.Red;
                }
                liste.Add(coin);
            }
            oku.Close();
            baglanti.Close();
            return liste;
        }



        //  select CONVERT(VARCHAR, tarih, 8) AS tarih, coin_miktar, tutar from islem_log where kullanici_id = 4 AND coin_id = 1 //Tarih sorgusu
    }
}
