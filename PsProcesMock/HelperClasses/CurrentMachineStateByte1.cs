using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.HelperClasses
{
    public class CurrentMachineStateByte1
    {
    
        byte byte1 { get; set; }
        bool esteButonulCareStingeToatePompeleApasat { get; set; }
        bool esteSenzorulDoiActiv { get; set; }
        bool esteSenzorulTreiActiv { get; set; } //senzorul de alarma
        bool estePompaUnuActiv { get; set; }
        bool estePompaDoiActiv { get; set; }
        bool estePompaTreiActiv { get; set; }
        bool estePompaPatruActiv { get; set; }

        public CurrentMachineStateByte1(byte byte1)
        {
            this.byte1 = byte1;
            esteButonulCareStingeToatePompeleApasat = BitHelper.FromBit(byte1, 0);
            esteSenzorulDoiActiv = BitHelper.FromBit(byte1, 1);
            esteSenzorulTreiActiv = BitHelper.FromBit(byte1, 6);
            estePompaUnuActiv = BitHelper.FromBit(byte1, 2);
            estePompaDoiActiv = BitHelper.FromBit(byte1, 3);
            estePompaTreiActiv = BitHelper.FromBit(byte1, 4);
            estePompaPatruActiv = BitHelper.FromBit(byte1, 5);
        }
        public override string ToString()
        {
            String s;
            s = "Pompa unu: " + estePompaUnuActiv
                + "\nPompa doi: " + estePompaDoiActiv
                + "\nPompa trei: " + estePompaTreiActiv
                + "\nPompa patru: " + estePompaPatruActiv
                + "\nSenzor doi: " + esteSenzorulDoiActiv
                + "\nSenzor trei: " + esteSenzorulTreiActiv
                + "\nButonul care stinge toate apasat: " + esteButonulCareStingeToatePompeleApasat;
            return s;
        }
    }
}
