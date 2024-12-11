using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241210_Abschlussprojekt_SEGL_TobiasLauer
{
    public class Einzelfeld : Spielfeld
    {
        public Einzelfeld()
        {

        }

        public string Art { get; set; }
        public bool besetzt { get; set; }



        public void EinzelfeldFarbe(bool meineFarbe)
        {
            string feldFarbe;

            if (meineFarbe) feldFarbe = Convert.ToChar((char)'\u2591').ToString();

            else feldFarbe = Convert.ToChar((char)'\u2588').ToString();

            Console.Write($"{feldFarbe}");


        }

        
        
    }
}
