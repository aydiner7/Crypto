using Crypto.Class;
using Crypto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Crypto.Controls
{
    /// <summary>
    /// HesapEkle.xaml etkileşim mantığı
    /// </summary>
    public partial class HesapEkle : Window
    {
        public HesapEkle()
        {
            InitializeComponent();
            sinirla();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        BankaHesapEkle BHE;
        private static readonly Regex DigitRegex = new Regex("[^0-9,-]+");

        private void btn_ekle_Click(object sender, RoutedEventArgs e)
        {
            HesaplarBanka eklenecekHesap = new HesaplarBanka() {

                hesap = hesapEkleHesapAdi.Text,
                banka = hesapEkleBankaAdi.Text,
                iban = hesapEkleIban.Text
            };
            //String ha = hesapEkleHesapAdi.Text;
            //String ba = hesapEkleBankaAdi.Text;
            //String ib = hesapEkleIban.Text;

            try
            {
                BHE = new BankaHesapEkle(Int32.Parse(login.gonderilecekID));
                int gelen = BHE.HesapEkle(eklenecekHesap);
                if (gelen == 1)
                {
                    MessageBox.Show(eklenecekHesap.hesap + " Adlı Hesabınız Eklenmiştir.");
                    this.Close();
                }
                else if (gelen == 0)
                {
                    MessageBox.Show("Hatalı Bilgi Girdiniz.");
                }

                //BHE = new BankaHesapEkle(ha, ba, ib);
                //int gelen = BHE.HesapEkle(Int32.Parse(login.gonderilecekID));
                //if (gelen == 1)
                //{
                //    MessageBox.Show(ha + " Adlı Hesabınız Eklenmiştir.");
                //    this.Close();
                //}
                //else if (gelen == 0)
                //{
                //    MessageBox.Show("Hatalı Bilgi Girdiniz.");
                //}

            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Daha Sonra Tekrar Deneyiniz.");

            }
        }

        private void hesapEkleIban_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DigitRegex.IsMatch(hesapEkleIban.Text)) hesapEkleIban.Background = Brushes.DarkRed;
            else if (string.IsNullOrEmpty(hesapEkleIban.Text)) hesapEkleIban.Background = Brushes.DarkRed;
            else if (Convert.ToDouble(hesapEkleIban.Text) <= 0.0) hesapEkleIban.Background = Brushes.DarkRed;
            else hesapEkleIban.Background = (Brush)new BrushConverter().ConvertFrom("#FF3D434B");
        }

        void sinirla()
        {
            hesapEkleIban.MaxLength = 26;
        }
    }
}
    

