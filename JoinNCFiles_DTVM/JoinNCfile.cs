using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace JoinNCFiles_DTVM
{
    /// <summary>
    /// Trida pro ziskani informaci o spojovanych souborech
    /// </summary>
    class JoinNCFiles
    {
        /// <summary>
        /// Pole nazvu souboru pro spojeni,soubory musi byt ve stejne slozce, s indexem _xxx.koncovka
        /// xxx je poradove cislo Nc souboru dle ktereho bude soubor pripojen do vysledneho NC souboru
        /// </summary>
        public List<string> NCoutput = new List<string>();
        /// <summary>
        /// Pole nazvu souboru serizovacich listu,soubory musi byt ve stejne slozce,s indexem _xxx-YYY.koncovka
        /// xxx je poradove cislo Nc souboru dle ktereho bude soubor pripojen do vysledneho NC souboru
        /// YYY je sufix generovany automaticky k nazvu serizovaciho listu,dle nastaveni v Konstrukteru postprocesoru
        /// </summary>
        public List<string> ToolSheet = new List<string>();
        /// <summary>
        /// Nazev,cesta serizovacich listu
        /// </summary>
        public string ToolSheetResult = string.Empty;
        /// <summary>
        /// Nazev,cesta NC souboru
        /// </summary>
        public string NCfileResult = string.Empty;
        /// <summary>
        /// Konstanta pro inkrement kterym se budou cislovat cisla radku ve spojovanych NC souborech
        /// </summary>
        private int lineNo = 10;
        //
        private string header = string.Empty;
        /// <summary>
        /// Konstanta pro nalezeni prvniho radku pro precislovani spojeneho vysledneho NC souboru
        /// </summary>
        private string firstLineNo = " @714";
        /// <summary>
        /// Konstanta pro nalezeni posledniho radku precislovani spojeneho vysledneho NC souboru
        /// </summary>
        private string lastLineNo = " M30";

        /// <summary>
        /// Konstruktor s parametrem,parametr slouzi pro vytvoreni patternu pro vyhledani vsech souboru vhodnych pro spojeni
        /// </summary>
        /// <param name="str">Nazev posledniho generovaneho NC souboru nacteneho z pamscl.dat</param>
        public JoinNCFiles(string str)
        {
            //vygeneruji si nazev vysledneho souboru ve kterem budou spojene soubory
            //POZOR TADY JE TROCHU CHAOZ!!!!! MUSIM UPRAVIT POZDEJC!!!!!
            if (Regex.Match(str, @"_{1}\d+").Success)
            {
                string[] tmp = new string[10];
                //ziskam posledni vyskyt podtrzitka
                int last = Path.GetFileName(str).LastIndexOf('_');
                //vytvorim si patern pro nacteni vsech souboru v dane slozce, ktere vehovuji podmince, ze ma nazev souboru doplneho o podtrzitko a suffix
                int pozice = Path.GetFileName(str).Length - last;
                int celkem = Path.GetFileName(str).Length;
                string patern = Path.GetFileNameWithoutExtension(str).Substring(0, celkem + 1 - pozice);
                tmp = Directory.GetFiles(Path.GetDirectoryName(str), patern + "*");
                string patern1 = string.Empty;
                last = -1;
                foreach (string item in tmp)
                {
                    if (last < item.LastIndexOf('-'))
                    {
                        last = item.LastIndexOf('-');
                        patern1 = item.Substring(last);
                    }
                }
                //Naplnim si List vsech NC souboru,serizovacich listu pro spojeni
                foreach (string item in tmp)
                {
                    if (item.EndsWith(patern1))
                        ToolSheet.Add(item);
                    else
                        NCoutput.Add(item);
                }
                //Ladici vystupy - bude potlaceno
                //foreach (string nc in NCoutput)
                //{
                //    Console.WriteLine("{0}", nc);
                //}
                //Console.ReadKey();
                //Sort(ref NCoutput);
                //Vygeneruji si cestu vysledneho spojeneho souboru pro NC vystup,serizovaci list
                //spojovanych souboru musi byt vice nez 1
                if (NCoutput.Count > 1)
                {
                    NCfileResult = Path.GetDirectoryName(NCoutput[1].ToString()) + @"\" + patern.Remove(patern.Length - 1) + Path.GetExtension(NCoutput[1].ToString());
                    ToolSheetResult = Path.GetDirectoryName(this.NCoutput[1].ToString()) + @"\" + patern.Remove(patern.Length - 1) + "-TOOL" + Path.GetExtension(this.NCoutput[1].ToString());
                }
            }
            else
            {
                NCoutput.Add("Chyba výběru souboru");
                Console.WriteLine("Chybný výběr souboru");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Metoda provede spojeni souboru
        /// </summary>
        /// <param name="insertLine">Flag pro rizeni vkladání řádků,TRUE tak se vloží (dojde k novemu precislovani nove vznikleho NC kodu)</param>
        public void Join(bool insertLine, string result, List<string> paths)
        {
            string str;
            //zjistim si vychozi kodovou stranku aktualne pouzivanou v OS
            //da se take zjistitpomoci cmd, na prikazovou radku napis chcp
            Encoding defaultEncoding = Encoding.GetEncoding(Encoding.Default.CodePage, new EncoderExceptionFallback(), new DecoderExceptionFallback());

            //jestlize jiz existuje soubor v ceste a s nazvem jako je vysledny soubor po spojeni, tak ho smazu
            this.Delete(result);
            foreach (String file in paths)
            {
                using (TextWriter tw = new StreamWriter(Path.GetFullPath(result), true, defaultEncoding))
                {
                    using (StreamReader tr = new StreamReader(file, defaultEncoding))
                    {
                        //test
                        //var ahoj = File.ReadAllLines(file);
                        //List<string> testAhoj = new List<string>(ahoj);
                        while (!tr.EndOfStream)
                        {
                            //trochu nelogicke prtoazeni nactene radky pres string, jinak provadi chyby
                            //pri zapisu do streamu, nevim proc.
                            str = tr.ReadLine();
                            if (insertLine)
                                tw.WriteLine(Renumber(str, ref this.lineNo));
                            else
                                tw.WriteLine(str);
                        }
                        tr.Close();
                    }
                    tw.Close();
                }
            }
        }

        /// <summary>
        /// Metoda pro nacteni obsahu serizovaciho listu vytvoreneho spojenim jednotlivych serizovacich listu
        /// </summary>
        /// <returns>String obsahujici vsechny radky serizovaciho listu vcetne koncu radky</returns>
        private List<string> GetToolSheet()
        {
            var allLines = File.ReadAllLines(ToolSheetResult, Encoding.Default);
            List<string> lines = new List<string>(allLines);
            return lines;
        }

        /// <summary>
        /// Metoda prida seznamy nastroju do vysledneho spojeneho NC souboru za pozici vyskytu stringu (OSAZENI ZASOBNIKU)
        /// </summary>
        public void AddToolSheetToNCfile()
        {
            var allLines = File.ReadAllLines(this.NCfileResult, Encoding.Default);
            List<string> lines = new List<string>(allLines);
            int index = lines.IndexOf("(OSAZENI ZASOBNIKU)");
            lines.InsertRange(index + 1, GetToolSheet());
            File.WriteAllLines(this.NCfileResult, lines.ToArray());
        }

        //metoda slouzi pro smazani  vysledneho souboru jestlize existuje
        private void Delete(string file)
        {
            try
            {
                FileInfo NCfile = new FileInfo(file);
                if (NCfile.Exists)
                    NCfile.Delete();
                NCfile = null;
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
        /// <summary>
        /// Metoda pro precislovani radku ve vyslednem spojenem NC souboru
        /// </summary>
        /// <param name="input">Nacteny radek souboru</param>
        /// <param name="num">Akt.cislo radku</param>
        /// <returns>Nacteny radek doplneny o cislo radku</returns>
        private string Renumber(string input, ref int num)
        {
            string ret = input;
            //doplnim cislo radku vsude tam kde v puvodnim NC vystupu cislo radku bylo,cislovani s krokem 10,zacatek cislovani 10
            if (input.StartsWith(")"))
            {
                ret = ("(" + num.ToString()).PadRight(1, ' ') + input;
                num += 10;
            }
            //doplnim cislo radku do indexace
            if (input.StartsWith(" N"))
            {
                input = input.Replace("N", "N" + num.ToString()).PadRight(1, ' ');
                ret = input.Replace("R800=", " R800=" + num.ToString()).PadRight(1, ' ');
                num += 10;
            }
            return ret;
        }



        //tato metoda pracuje fajn,ale nebude pouzita!!!!
        //metoda slouzi k vymazani cisla bloku jestli se vyskytuje
        //radek zacina N180_G1__
        //radek zacina N150 G0 G40
        private string ClearLineNo(string input)
        {
            string[] ret;
            char[] sep = { ' ' };
            if (input.StartsWith("N") && !input.StartsWith("N99999"))
            {
                if (Regex.Match(input, @"^N\d+ ").Success)
                {
                    ret = Regex.Split(input, @"^N\d+ ");
                    return ret[1].TrimStart(sep);
                }
                if (Regex.Match(input, @"^N\d+_").Success)
                {
                    ret = Regex.Split(input, @"^N\d+_");
                    return ret[1];
                }
            }
            return input;
        }

        /// <summary>
        /// Setrideni pole stringu podle hodnoty sumy ASCII jednotlivych stringu
        /// </summary>
        /// <param name="pole">REF pole stringu prostrideni</param>
        private void Sort(ref string[] pole)
        {
            string tmp = string.Empty;
            for (int i = 0; i < pole.Length - 1; i++)
            {
                if (GetIntVal(pole[i]) > GetIntVal(pole[i + 1]))
                {
                    tmp = pole[i + 1];
                    pole[i + 1] = pole[i];
                    pole[i] = tmp;
                }
            }
        }

        /// <summary>
        /// Prevod stringu na ASCII hodnotu jednotlivych znaku
        /// </summary>
        /// <param name="str">String pro prevod na ASCII hodnotu</param>
        /// <returns>Suma ASCII hodnot stringu</returns>
        private Int64 GetIntVal(string str)
        {
            Int64 ret = -1;
            for (int i = 0; i < str.Length; i++)
                ret = ret + Convert.ToInt64(str[i]);
            return ret;
        }

    }
}
