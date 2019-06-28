using System;

namespace JoinNCFiles_DTVM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Todo - class ECsettings - singleton
                //ziskam nastaveni,cteni z pamscl.dat
                
                var ecsettings = ECsettings.Instance;
                //ziskam informace o spojovanych souborech
                JoinNCFiles joinFiles = new JoinNCFiles(ecsettings.NCfile);
                //spojim serizovaci listy nastroju,ale jen tehdy je-li souboru ke spojeni vice jak 1
                if (joinFiles.NCoutput.Count > 1)
                {
                    joinFiles.Join(false, joinFiles.ToolSheetResult, joinFiles.ToolSheet);
                    //spojim NC soubory
                    joinFiles.Join(true, joinFiles.NCfileResult, joinFiles.NCoutput);
                    //doplnim do NC souboru ktery je v seznamu na 0 pozici seznam nastroju
                    joinFiles.AddToolSheetToNCfile();
                }
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
