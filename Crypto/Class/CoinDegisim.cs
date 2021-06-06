using Crypto.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Class
{
    class CoinDegisim
    {
        SqlConnection baglanti;
        SqlCommand komut;

        int gelenCoin_id;

        public CoinDegisim(int coinID)
        {
            gelenCoin_id = coinID;
            baglanti = new SqlConnection("Data Source=DESKTOP-OPGD9LL;Initial Catalog=betCoin;Integrated Security=True");
        }

        public List<CoinDeger> coinListesi()
        {
            baglanti.Open();
            List<CoinDeger> liste = new List<CoinDeger>();
            string sorgu = ("select top 2 coin_degeri from Coin_Degeri where coin_id=@cid order by id DESC");
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@cid", gelenCoin_id);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                CoinDeger coin = new CoinDeger();
                coin.deger = oku["coin_degeri"].ToString();

                liste.Add(coin);
            }
            oku.Close();
            baglanti.Close();
            return liste;
        }
    }
}
