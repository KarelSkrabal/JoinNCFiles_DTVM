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

                joinner.Join();

                MC3000 joinFiles = new MC3000(ecsettings.NCfile, new MC3000Settings());
                
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
