using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Crypto.Dto
{
    public class Coinler
    {

        public string tarih { get; set; }
        public string miktar { get; set; }
        public string fiyat { get; set; }
        public Color renk { get; set; }

        public Coinler()
        {

        }
    }
}
