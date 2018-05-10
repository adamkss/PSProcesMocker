using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.HelperClasses
{
    public class CurrentMachineStateByte2
    {
       
        byte byte2 { get; set; }
        bool esteSenzorulUnuActiv { get; set; } //primul de jos
        bool esteSenzorulPatruActiv { get; set; }
        bool esteButonulPompaUnuApasat { get; set; }
        bool esteButonulPompaDoiApasat { get; set; }
        bool esteButonulPompaTreiApasat { get; set; }
        bool esteButonulPompaPatruApasat { get; set; }
        bool sunaAlarma { get; set; }
        public CurrentMachineStateByte2(byte byte2)
        {
            this.byte2 = byte2;
            esteSenzorulUnuActiv = BitHelper.FromBit(byte2, 6);
            esteSenzorulPatruActiv = BitHelper.FromBit(byte2, 5);
            esteButonulPompaUnuApasat = BitHelper.FromBit(byte2, 1);
            esteButonulPompaDoiApasat = BitHelper.FromBit(byte2, 2);
            esteButonulPompaTreiApasat = BitHelper.FromBit(byte2, 3);
            esteButonulPompaPatruApasat = BitHelper.FromBit(byte2, 4);
            sunaAlarma = BitHelper.FromBit(byte2, 0);
        }
        public override string ToString()
        {
            String s;
            s = "Senzor unu: " + esteSenzorulUnuActiv
                + "\nSenzor patru: " + esteSenzorulPatruActiv
                + "\nButonul care stinge pompa 1 apasat: " + esteButonulPompaUnuApasat
                + "\nButonul care stinge pompa 2 apasat: " + esteButonulPompaDoiApasat
                + "\nButonul care stinge pompa 3 apasat: " + esteButonulPompaTreiApasat
                + "\nButonul care stinge pompa 4 apasat: " + esteButonulPompaPatruApasat
                + "\nSuna alarma: " + sunaAlarma;
            return s;
        }
    }
}
