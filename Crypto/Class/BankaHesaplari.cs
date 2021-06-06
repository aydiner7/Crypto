using Crypto.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Class
{
    class BankaHesaplari
    {

        SqlConnection baglanti;
        SqlCommand komut;

        int gelenKul;

        public BankaHesaplari(int kullanici)
        {
            gelenKul = kullanici;
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");
        }

        public List<HesaplarBanka> hesapListesi()
        {
            baglanti.Open();
            List<HesaplarBanka> liste = new List<HesaplarBanka>();
            string sorgu = ("select hesap_adi, banka_adi, iban_num from Banka_Hesap where kullanici_id=@kid");
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@kid", gelenKul);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                HesaplarBanka hesap = new HesaplarBanka();
                hesap.hesap = oku["hesap_adi"].ToString();
                hesap.banka = oku["banka_adi"].ToString();
                hesap.iban = oku["iban_num"].ToString();
                
                liste.Add(hesap);
            }
            oku.Close();
            baglanti.Close();
            return liste;
        }
    }
}
