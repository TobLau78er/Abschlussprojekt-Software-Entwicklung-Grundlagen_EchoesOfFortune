using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241216_AbschlussProjekt_SEGL_Lauer_EchoesOfFortune
{
    public class Einzelfeld
    {
        public Einzelfeld()
        {
        }

        public int PosZeile { get; set; }
        public int PosSpalte { get; set; }
        public bool HatSpieler { get; set; }
        public bool HatSchatz { get; set; }
        public bool HatFalle { get; set; }
        public string Symbol { get; set; }

        public void AktualisiereFeld()
        {
            if (HatSpieler)
            {
                Symbol = "P";
            }
            else if (HatSchatz)
            {
                Symbol = "$";
            }
            else if (HatFalle)
            {
                Symbol = "%";
            }
            else
            {
                Symbol = ".";
            }
            
            
        }


    }
    
}
