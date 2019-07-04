using System;

namespace JoinNCFiles_DTVM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //get all necessary data from pascl.dat file
                IPamsclReader reader = new PamsclReader(@"\Temp\Planit");
                var ecsettings = ECsettings.CreateInstance(reader);
                ecsettings.GetDetails();
                //get a propriate joinner for the actual postprocesor
                var joinner = JoinFactory.CreateJoinner(ecsettings.Postprocesor);
                //joining NC files
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
