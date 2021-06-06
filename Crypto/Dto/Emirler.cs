using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Crypto.Dto
{
    class Emirler
    {


        public string fiyat { get; set; }
        public string miktar { get; set; }
        public Color renk { get; set; }
        public string tutar { get; set; }
        public string islem { get; set; }
        public string durum { get; set; }
        public string iptal { get; set; }
        public string tektiklenenFiyat { get; set; }

        public Emirler()
        {

        }
    }
}
