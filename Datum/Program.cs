using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;

namespace Datum
{
    public class Example
    {

        public static void Main()
        {
            
            List<string> lines = [];
            string path = @"C:\temp\Datum.txt";
            DateTime localDate = DateTime.Now;
            DateTime utcDate = DateTime.UtcNow;
            string[] cultureNames = ["de-DE"];

            

            foreach (var cultureName in cultureNames)
            {

                var culture = new CultureInfo(cultureName);
                Console.WriteLine("{0}:", culture.NativeName);
                Console.WriteLine("   Local date and time: {0}, {1:G}",
                                  localDate.ToString(culture), localDate.Kind);
                Console.WriteLine("   UTC date and time: {0}, {1:G}\n",
                                  utcDate.ToString(culture), utcDate.Kind);
                Console.WriteLine("   Local date and time: {0}, {1:G}",
                                  localDate.ToString(culture), localDate.Kind);
                Console.WriteLine("Enter your birth date:");

                lines.Add(culture.NativeName);
                lines.Add("local " + localDate.ToString(culture));
                lines.Add("utc " + utcDate.ToString(culture));

                try
                {
                    string[] customFmts = ["d MMM yyyy"];


                    Console.Write("Enter a day: ");
                    int day = int.Parse(Console.ReadLine());
                    Console.Write("Enter a month: ");
                    int month = int.Parse(Console.ReadLine());
                    Console.Write("Enter a year: ");
                    int year = int.Parse(Console.ReadLine());

                    DateTime date1 = DateTime.Today;
                    DateTime date2 = new(date1.Year, month, day);
                    double numberOfDays = (date2 - date1).TotalDays;


                    Console.WriteLine("Die Anzahl der Tage ist: {0}", numberOfDays);

                    lines.Add("Geburstdatum " + date2.ToString("ddd.dd.MMM.yyyy"));
                    lines.Add("Noch " + numberOfDays.ToString() + " tage");
                }

                catch
                {

                    Console.WriteLine("ungültiger wert");




                }
            }
           

            try
            {
                using (FileStream fileStream = new(@"C:\temp\Datum.txt", FileMode.OpenOrCreate))

                {
                    using Aes aes = Aes.Create();
                    byte[] key =
                    [
                        0x01,
                        0x02,
                        0x03,
                        0x04,
                        0x05,
                        0x06,
                        0x07,
                        0x08,
                        0x09,
                        0x10,
                        0x11,
                        0x12,
                        0x13,
                        0x14,
                        0x15,
                        0x16
            ];
                    aes.Key = key;

                    byte[] iv = aes.IV;
                    fileStream.Write(iv, 0, iv.Length);

                    using CryptoStream cryptoStream = new(
                        fileStream,
                        aes.CreateEncryptor(),
                        CryptoStreamMode.Write);
                    // By default, the StreamWriter uses UTF-8 encoding.
                    // To change the text encoding, pass the desired encoding as the second parameter.
                    // For example, new StreamWriter(cryptoStream, Encoding.Unicode).
                    using StreamWriter encryptWriter = new(cryptoStream);
                   // System.IO.File.WriteAllLines(path, lines);
                    encryptWriter.WriteLine();


                }

                Console.WriteLine("The file funkioniert encrypted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The encryption fehler. {ex}");
            }
       
        }
       
        
    }


}
