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
    /// DenetimCoinler.xaml etkileşim mantığı
    /// </summary>
    public partial class DenetimCoinler : UserControl
    {
        public string Ad { get { return CoinName.Content.ToString(); } set { CoinName.Content = value; } }
        public string Tutar { get { return CoinFiyat.Content.ToString(); } set { CoinFiyat.Content = value; } }
        string YolAdres;
        public string Yol
        {
            set
            {
                Uri imageUri = new Uri(value, UriKind.Relative);
                BitmapImage imageBitmap = new BitmapImage(imageUri);
                CoinLogo.Source = imageBitmap;
                YolAdres = value;
            }
            get { return YolAdres; }
        }

        public DenetimCoinler()
        {
            InitializeComponent();
        }
    }
}
