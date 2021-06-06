using Crypto.Class;
using System;
using System.Collections.Generic;
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
    /// DenetimBankaHesaplarim.xaml etkileşim mantığı
    /// </summary>
    public partial class DenetimBankaHesaplarim : UserControl
    {
        public DenetimBankaHesaplarim()
        {
            InitializeComponent();
            HesapListele();
        }

        // Hesap Ekle Buton
        private void Button_Click(object sender, RoutedEventArgs e)

        {
            if (login.gonderilecekDurum.Trim() == "1")
            {
                HesapEkle ac = new HesapEkle();
                ac.ShowDialog();
            }
            else MessageBox.Show("Lütfen Hesabınızı Aktif Ediniz.");
        }
        // Hesapları Listele
        private void HesapListele()
        {
            BankaHesaplari BankaBilgi = new BankaHesaplari(Int32.Parse(login.gonderilecekID));
            var gelen = BankaBilgi.hesapListesi();

            int row = 0;
            foreach (var item in gelen)
            {
                Label lblHesap = new Label
                {
                    Content = item.hesap,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize=18,
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF"),
                    Margin = new Thickness(10, 0, 0, 0)
                };
                Grid.SetRow(lblHesap, row);
                Grid.SetColumn(lblHesap, 0);

                Label lbliban = new Label
                {
                    Content = "TR"+item.iban,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 18,
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF"),
                    Margin = new Thickness(10, 0, 0, 0)
                };
                Grid.SetRow(lbliban, row);
                Grid.SetColumn(lbliban, 1);

                Label lblBanka = new Label
                {
                    Content = item.banka,
                    FontSize = 18,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFFFF"),
                    Margin = new Thickness(0, 0, 10, 0)
                };
                Grid.SetRow(lblBanka, row);
                Grid.SetColumn(lblBanka, 2);

                Grid_bankaHesaplari.Children.Add(lblHesap);
                Grid_bankaHesaplari.Children.Add(lbliban);
                Grid_bankaHesaplari.Children.Add(lblBanka);
                row++;

            }

        }
    }
}
