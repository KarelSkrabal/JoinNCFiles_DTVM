using System;

namespace JoinNCFiles_DTVM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {             
                var ecsettings = ECsettings.Instance;
                //ziskam informace o spojovanych souborech
                var joinner = JoinFactory.createJoinner(ecsettings.post);

                MC3000 joinFiles = new MC3000(ecsettings.NCfile, new MC3000Settings());
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
