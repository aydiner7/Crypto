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
using Crypto.Class;
using Crypto.Dto;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Crypto.Controls
{
    /// <summary>
    /// DenetimPiyasa.xaml etkileşim mantığı
    /// </summary>
    public partial class DenetimPiyasa : UserControl
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        SqlDataReader dr;


        public DenetimPiyasa()
        {
            InitializeComponent();
            islemGecmis();
            stopGecmisi();
            coinDegerBelirle1();
            coinDegerBelirle2();
            coinDegerBelirle3();

            grafik.Series = new SeriesCollection
            {
                //SDU 085
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0 , double.Parse(sgelen10)),
                        new ObservablePoint(1 , double.Parse(sgelen9)),
                        new ObservablePoint(2 , double.Parse(sgelen8)),
                        new ObservablePoint(3 , double.Parse(sgelen7)),
                        new ObservablePoint(4 , double.Parse(sgelen6)),
                        new ObservablePoint(5 , double.Parse(sgelen5)),
                        new ObservablePoint(6 , double.Parse(sgelen4)),
                        new ObservablePoint(7 , double.Parse(sgelen3)),
                        new ObservablePoint(8 , double.Parse(sgelen2)),
                        new ObservablePoint(9 ,double.Parse(sgelen1))
                    },
                    PointGeometry= DefaultGeometries.Diamond,
                    Title = "SDU",
                    PointGeometrySize = 15
                },
                //MAKU 051
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                       new ObservablePoint(0 , double.Parse(mgelen10)),
                        new ObservablePoint(1 , double.Parse(mgelen9)),
                        new ObservablePoint(2 , double.Parse(mgelen8)),
                        new ObservablePoint(3 , double.Parse(mgelen7)),
                        new ObservablePoint(4 , double.Parse(mgelen6)),
                        new ObservablePoint(5 , double.Parse(mgelen5)),
                        new ObservablePoint(6 , double.Parse(mgelen4)),
                        new ObservablePoint(7 , double.Parse(mgelen3)),
                        new ObservablePoint(8 , double.Parse(mgelen2)),
                        new ObservablePoint(9 ,double.Parse(mgelen1))
                    },
                    PointGeometry= DefaultGeometries.Square,
                    Title = "MAKU",
                    PointGeometrySize = 15
                    
                    
                },
                //AKU 112
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0 , double.Parse(agelen10)),
                        new ObservablePoint(1 , double.Parse(agelen9)),
                        new ObservablePoint(2 , double.Parse(agelen8)),
                        new ObservablePoint(3 , double.Parse(agelen7)),
                        new ObservablePoint(4 , double.Parse(agelen6)),
                        new ObservablePoint(5 , double.Parse(agelen5)),
                        new ObservablePoint(6 , double.Parse(agelen4)),
                        new ObservablePoint(7 , double.Parse(agelen3)),
                        new ObservablePoint(8 , double.Parse(agelen2)),
                        new ObservablePoint(9 ,double.Parse(agelen1))
                    },
                    Title = "AKU",
                    FontSize =31,
                    PointGeometrySize = 15

                }
            };
        }

        string sgelen1;
        string sgelen2;
        string sgelen3;
        string sgelen4;
        string sgelen5;
        string sgelen6;
        string sgelen7;
        string sgelen8;
        string sgelen9;
        string sgelen10;

        //SDU coin Değerleri
        private void coinDegerBelirle1()
        {
            CoinDegerCek coinlerim = new CoinDegerCek(1);
            var gelen = coinlerim.coinListesi();

             sgelen1 = gelen[0].deger;
             sgelen2 = gelen[1].deger;
             sgelen3 = gelen[2].deger;
             sgelen4 = gelen[3].deger;
             sgelen5 = gelen[4].deger;
             sgelen6 = gelen[5].deger;
             sgelen7 = gelen[6].deger;
             sgelen8 = gelen[7].deger;
             sgelen9 = gelen[8].deger;
             sgelen10 = gelen[9].deger;
            simge_sdu.Tutar = sgelen1;
        }

        string mgelen1;
        string mgelen2;
        string mgelen3;
        string mgelen4;
        string mgelen5;
        string mgelen6;
        string mgelen7;
        string mgelen8;
        string mgelen9;
        string mgelen10;
        //Maku Coin Degerleri
        private void coinDegerBelirle2()
        {
            CoinDegerCek coinlerim = new CoinDegerCek(2);
            var gelen = coinlerim.coinListesi();

            mgelen1 = gelen[0].deger;
            mgelen2 = gelen[1].deger;
            mgelen3 = gelen[2].deger;
            mgelen4 = gelen[3].deger;
            mgelen5 = gelen[4].deger;
            mgelen6 = gelen[5].deger;
            mgelen7 = gelen[6].deger;
            mgelen8 = gelen[7].deger;
            mgelen9 = gelen[8].deger;
            mgelen10 = gelen[9].deger;
            simge_maku.Tutar = mgelen1;

        }

        string agelen1;
        string agelen2;
        string agelen3;
        string agelen4;
        string agelen5;
        string agelen6;
        string agelen7;
        string agelen8;
        string agelen9;
        string agelen10;
        // AKU coin Degerleri
        private void coinDegerBelirle3()
        {
            CoinDegerCek coinlerim = new CoinDegerCek(3);
            var gelen = coinlerim.coinListesi();

            agelen1 = gelen[0].deger;
            agelen2 = gelen[1].deger;
            agelen3 = gelen[2].deger;
            agelen4 = gelen[3].deger;
            agelen5 = gelen[4].deger;
            agelen6 = gelen[5].deger;
            agelen7 = gelen[6].deger;
            agelen8 = gelen[7].deger;
            agelen9 = gelen[8].deger;
            agelen10 = gelen[9].deger;
            simge_aku.Tutar = agelen1;

        }




        private void lblgunluk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bargunluk.Background = (Brush)new BrushConverter().ConvertFrom("#febc2c");
            barhaftalik.Background = Brushes.White;
            baraylik.Background = Brushes.White;
        }

        private void lblhaftalik_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bargunluk.Background = Brushes.White;
            barhaftalik.Background = (Brush)new BrushConverter().ConvertFrom("#febc2c");
            baraylik.Background = Brushes.White;
        }

        private void lblaylik_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bargunluk.Background = Brushes.White;
            barhaftalik.Background = Brushes.White;
            baraylik.Background = (Brush)new BrushConverter().ConvertFrom("#febc2c");            
        }

        private void islemGecmis()
        {
            islemGecmisi islem = new islemGecmisi(tiklananCoin, Int32.Parse(login.gonderilecekID));
            var gelen= islem.islemListesi();

            int row = 2;
            foreach (var item in gelen)
            {
                    Label lbltarih = new Label
                    {
                        Content = item.tarih,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#FFD3C8C8")
                    };
                    Grid.SetRow(lbltarih, row);
                    Grid.SetColumn(lbltarih, 0);

                    Label lblmiktar = new Label
                    {
                        Content = Math.Round(float.Parse(item.miktar), 2).ToString(),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#FFD3C8C8"),
                        Margin = new Thickness(10, 0, 0, 0)
                    };
                    Grid.SetRow(lblmiktar, row);
                    Grid.SetColumn(lblmiktar, 1);

                    Label lblfiyat = new Label
                    {
                        Content = Math.Round(float.Parse(item.fiyat), 2).ToString(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Foreground = new SolidColorBrush(item.renk)
                    };
                    Grid.SetRow(lblfiyat, row);
                    Grid.SetColumn(lblfiyat, 2);

                    islem_gecmisi.Children.Add(lbltarih);
                    islem_gecmisi.Children.Add(lblmiktar);
                    islem_gecmisi.Children.Add(lblfiyat);
                    row++;

                
            }

        }

        private void stopGecmisi()
        {
            emirGecmisi stop = new emirGecmisi(tiklananCoin, Int32.Parse(login.gonderilecekID));
            var gelen = stop.emirListesi();

            int row = 2;
            foreach (var item in gelen)
            {
                        Label lblfiyat = new Label
                        {
                            Content = Math.Round(float.Parse(item.fiyat), 2).ToString(),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Foreground = new SolidColorBrush(item.renk)
                        };
                        Grid.SetRow(lblfiyat, row);
                        Grid.SetColumn(lblfiyat, 0);

                        Label lblmiktar = new Label
                        {
                            Content = Math.Round(float.Parse(item.miktar), 2).ToString(),
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Center,
                            Foreground = (Brush)new BrushConverter().ConvertFrom("#FFD3C8C8"),
                            Margin = new Thickness(10, 0, 0, 0)
                        };
                        Grid.SetRow(lblmiktar, row);
                        Grid.SetColumn(lblmiktar, 1);

                        Label lbltutar = new Label
                        {
                            Content = Math.Round(float.Parse(item.tutar), 2).ToString(),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Foreground = (Brush)new BrushConverter().ConvertFrom("#FFD3C8C8")
                        };
                        Grid.SetRow(lbltutar, row);
                        Grid.SetColumn(lbltutar, 2);

                        emir_defteri.Children.Add(lblfiyat);
                        emir_defteri.Children.Add(lblmiktar);
                        emir_defteri.Children.Add(lbltutar);
                        row++;
                    
            }
        }

         int tiklananCoin = 1;

        private void coin_SDU_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tiklananCoin = 1;
            islem_gecmisi.Children.Clear();
            emir_defteri.Children.Clear();
            islemGecmis();
            stopGecmisi();
        }

        private void coin_MAKU_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tiklananCoin = 2;
            emir_defteri.Children.Clear();
            islem_gecmisi.Children.Clear();
            stopGecmisi();
            islemGecmis();
        }

        private void coin_AKU_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tiklananCoin = 3;
            emir_defteri.Children.Clear();
            islem_gecmisi.Children.Clear();
            islemGecmis();
            stopGecmisi();
        }
    }
}
