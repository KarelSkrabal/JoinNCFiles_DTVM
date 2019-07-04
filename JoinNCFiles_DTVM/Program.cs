using System;

namespace JoinNCFiles_DTVM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //ziskam info z pamscl.dat souboru
                IPamsclReader reader = new PamsclReader(@"\Temp\Planit");
                var ecsettings = ECsettings.CreateInstance(reader);
                //ziskam informace o spojovanych souborech
                ecsettings.GetDetails();
                var joinner = JoinFactory.createJoinner(ecsettings.Postprocesor);
                //spojim soubory pro aktualne pouzity postprocesor
                joinner.JoinFiles(ecsettings.NCfile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Chyba ... " + e.StackTrace +
                    Environment.NewLine + Environment.NewLine +
                    e.Message + Environment.NewLine + Environment.NewLine +
                    e.TargetSite);
                Console.WriteLine("Program bude ukončen libovolnou klávesou.");
                Console.ReadKey();
            }
        }
    }
}
