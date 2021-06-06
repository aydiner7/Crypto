using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Class
{
    class KarZarar
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        SqlDataReader dr;

        int gelenCoinID;

        public KarZarar(int coinID)
        {
            gelenCoinID = coinID;
        }



        private float SatilanCoin()
        {
            veriTabani();
            string sorgu = "select SUM(tutar) AS toplam from islem_log where kullanici_id = @kullanici AND coin_id = @coin AND islem_kodu = @islem";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kullanici", login.gonderilecekID);
            komut.Parameters.AddWithValue("@coin", gelenCoinID);
            komut.Parameters.AddWithValue("@islem", 2);
            baglanti.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                if (dr["toplam"].ToString() == "")
                {
                    return 01;
                }
                else
                {

                    float mevcutCoinMiktar = float.Parse(dr["toplam"].ToString());
                    return mevcutCoinMiktar;
                }
            }
            return 001;
        }

        private float AlinanCoin()
        {
            veriTabani();
            string sorgu = "select SUM(tutar) AS toplamTutar from islem_log where kullanici_id = @kullanici AND coin_id = @coin AND islem_kodu = @islem";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kullanici", login.gonderilecekID);
            komut.Parameters.AddWithValue("@coin", gelenCoinID);
            komut.Parameters.AddWithValue("@islem", 1);
            baglanti.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                if (dr["toplamTutar"].ToString() == "")
                {
                    return 0;
                }
                else
                {

                    float mevcutCoinMiktar = float.Parse(dr["toplamTutar"].ToString());
                    return mevcutCoinMiktar;
                }
            }
            return 01;
        }


        public float zararHesaplama()
        {
            float gelenAlinanCoin = AlinanCoin();
            float gelenSatilanCoin = SatilanCoin();

            if (gelenAlinanCoin == 0 && gelenSatilanCoin == 0) return 01;           //İşlem görmemiş Coin Kodu
            else if (gelenAlinanCoin == 01 || gelenSatilanCoin == 01) return 001; // VeriTabanı Hata Kodu
            else if (gelenAlinanCoin != 0 && gelenAlinanCoin>=gelenSatilanCoin)
            {
                float zarar = gelenAlinanCoin - gelenSatilanCoin;
                return zarar;
            }
            else return 0001;  //Hesaplama Hata Kodu

        }

        public float karHesaplama()
        {
            float gelenAlinanCoin = AlinanCoin();
            float gelenSatilanCoin = SatilanCoin();

            if (gelenAlinanCoin == 0 && gelenSatilanCoin == 0) return 01;           //İşlem görmemiş Coin Kodu
            else if (gelenAlinanCoin == 01 || gelenSatilanCoin == 01) return 001; // VeriTabanı Hata Kodu
            else if (gelenSatilanCoin != 0 && gelenSatilanCoin > gelenAlinanCoin)
            {
                float kar = gelenSatilanCoin - gelenAlinanCoin;
                return kar;
            }
            else return 0001;  //Hesaplama Hata Kodu

        }






        void veriTabani()
        {
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");
        }
    }
}
