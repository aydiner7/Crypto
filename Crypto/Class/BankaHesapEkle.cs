using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Crypto.Dto;

namespace Crypto.Class
{
    class BankaHesapEkle
    {
        SqlConnection baglanti;
        SqlCommand komut;


        //String hAdi;
        //String bAdi;
        //String iban;
        int gID;
        public BankaHesapEkle(int kullaniciID)
        {
            gID = kullaniciID;
        }
        //public BankaHesapEkle(String Hesap_Adi, String Banka_Adi, String Banka_iban)
        //{
        //    hAdi = Hesap_Adi;
        //    bAdi = Banka_Adi;
        //    iban = Banka_iban;

        //}

        public int HesapEkle(HesaplarBanka gelen)
        {
            veriTabani();
            if (string.IsNullOrEmpty(gelen.banka) || string.IsNullOrEmpty(gelen.hesap) || string.IsNullOrEmpty(gelen.iban)  || gelen.iban.Length!=26)
            {
                return 0;
            }
            string sorgu = "INSERT INTO Banka_Hesap(kullanici_id,hesap_adi,banka_adi,iban_num) VALUES (@kul,@hesap,@banka,@iban)";
            komut = new SqlCommand(sorgu, baglanti);

            komut.Parameters.AddWithValue("@kul", gID);
            komut.Parameters.AddWithValue("@hesap", gelen.hesap);
            komut.Parameters.AddWithValue("@banka", gelen.banka);
            komut.Parameters.AddWithValue("@iban", gelen.iban);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            return 1;
        }

        //public int HesapEkle(int kullaniciID)
        //{
        //    veriTabani();
        //    if (string.IsNullOrEmpty(bAdi) || string.IsNullOrEmpty(hAdi) || string.IsNullOrEmpty(iban) || iban.Length != 26)
        //    {
        //        return 0;
        //    }
        //    string sorgu = "INSERT INTO Banka_Hesap(kullanici_id,hesap_adi,banka_adi,iban_num) VALUES (@kul,@hesap,@banka,@iban)";
        //    komut = new SqlCommand(sorgu, baglanti);

        //    komut.Parameters.AddWithValue("@kul", kullaniciID);
        //    komut.Parameters.AddWithValue("@hesap", hAdi);
        //    komut.Parameters.AddWithValue("@banka", bAdi);
        //    komut.Parameters.AddWithValue("@iban", iban);
        //    baglanti.Open();
        //    komut.ExecuteNonQuery();
        //    baglanti.Close();
        //    return 1;
        //}



        void veriTabani()
        {

            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");
        }


        
        
    }
}
