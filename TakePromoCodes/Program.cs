using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;

namespace TakePromoCodes
{
    public static class Calculation
    {

        public interface IDirectoryHelper
        {
            ICollection<string> GetFiles(string path);
        }

        public class DirectoryHelper : IDirectoryHelper
        {
            public ICollection<string> GetFiles(string path)
            {
                string[] files = Directory.GetFiles(path);
                return files;
            }
        }
        public static List<string> ReadMe(string path)
        {
            // string path = @"C:\SomeDir\hta.txt";
            List<string> codes = new List<string>();
            try
            {

                Console.WriteLine();
                Console.WriteLine("******считываем построчно********");
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        codes.Add(line);
                        //Console.WriteLine(line);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return codes;

        }
        public static List<string> PromoCodes(List<string> codes)
        {
            List<string> res = new List<string>();
            foreach (var code in codes)
            {
                if (!code.Equals(String.Empty))
                {
                    var delimitedValues = code.Split(',');
                    var promoCode = delimitedValues[1].Replace('"', ' ').Trim();
                    res.Add(promoCode);
                }
            }
            return res;
        }

        public static List<string> PromoCodesAdd(List<string> codes)
        {
            List<string> res = new List<string>();
            foreach (var code in codes)
            {
                if (!code.Equals(String.Empty))
                {
                    var delimitedValues = code.Split(',');
                    var promoCode =delimitedValues[0].Trim();
                    res.Add(promoCode);
                }
            }
            return res;
        }
        public static List<string> PromoCodes1(List<string> codes)
        {
            List<string> res = new List<string>();
            foreach (var code in codes)
            {
                var delimitedValues = code.Split(',');
                var promoCode = delimitedValues[0].Trim();
                res.Add(promoCode);
            }
            return res;
        }

        public static List<string> TakePromoCodes(List<string> codes)
        {
            List<string> res = new List<string>();
            foreach (var code in codes)
            {
                var delimitedValues = code.Split(',');
                var promoCode = delimitedValues[0].Trim();
                var expiryDate = delimitedValues[3].Replace('"', ' ').Trim();
                var programGrouping = delimitedValues[4].Replace('"', ' ').Trim();
                string expiry = "31/1/2021 00:00:00";
                if (expiryDate == expiry )
                    res.Add(promoCode);
            }
            return res;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<String> expected = new List<String>();
            expected.Add("ABC");
            String clientCode = "SZGR";
            DateTime expiryDate = DateTime.Now;
            DateTime creationDate = DateTime.Now;
            List<String> codesClass = new List<String>();
            //codesClass.Add("ABC");
            //codesClass.Add("AQWEC");
            //codesClass.Add("ABC");
            PromoCodes promoCodesClass = new PromoCodes(clientCode, expiryDate, creationDate, codesClass);
            string connectionString = @"server=snowball-uat-rds.c2pbc0gedoix.us-east-1.rds.amazonaws.com;userid=service;password=lucent-great-yuletide-midst-crime-marlin;database=snowball";
          //  promoCodesClass.ReadCodesFromMariaDB(connectionString, "SZGR");
            PromoCodesManipulation promoCodesManipulation = new PromoCodesManipulation(promoCodesClass);
           // promoCodesManipulation.ReadCodesFromFile(@"D:\SOFT_SERVE_OLD_PC\Promo codes\szgr_10011.txt");


            // Act
            List<String> duplicates = promoCodesClass.DuplicatesInOneFile();
            //szgr
            Console.WriteLine("Hello World!");
            List<string> codes = Calculation.ReadMe(@"D:\SOFT_SERVE_OLD_PC\Promo codes\szgr_10011.txt");
            //List<string> promoCodes = Calculation.ReadMe(@"D:\SOFT_SERVE_OLD_PC\Promo codes\Today\NAPUS_SD_46861.csv");

            var promoCodes = Calculation.PromoCodes(codes);
            WriteToFile(promoCodes);
            WriteToFileToCheckInDB(promoCodes);
            Console.WriteLine("the end");
            //Console.Read();

            //wpcdn
            //Console.WriteLine("Hello WPCDN World!");
            //List<string> codes = Calculation.ReadMe(@"D:\SOFT_SERVE_OLD_PC\Promo codes\wpcdn_1.txt");
            //WriteToFileToCheckInDB(codes);
            //Console.WriteLine("WPCDN the end");

            //ccus,cmus,cmca add comment
            //List<string> codes = Calculation.ReadMe(@"D:\cmca_deactivation_2022.txt");
            //WriteToFile_DeactivationFormat(codes);
            //Console.WriteLine("the end");
            //Console.Read();

            //ccus,cmus,cmca add change enrollment level
            //List<string> ids = Calculation.ReadMe(@"D:\cmca_ids_2.txt");
            //List<string> levels = Calculation.ReadMe(@"D:\cmca_levels_2.txt");
            //WriteToFile_ChangeEnrollmentLevelFormat(ids, levels, @"D:\cmca_new_enrollment_level_format_2.txt");
            //Console.WriteLine("file with dealer ids and new enrollment levels created!");


            //List<string> client_codes = Calculation.ReadMe(@"D:\client_codes_1.txt");
            //WriteToFile_EnableHTMLFormat(client_codes, @"D:\enable_html_format_1.txt");
            //Console.WriteLine("file with clients for each required html notification view was created!");

            Console.Read();



            //Calculation.IDirectoryHelper directoryHelper = new Calculation.DirectoryHelper();
            //ICollection<string> files = directoryHelper.GetFiles(@"D:\");

            //foreach (var file in files)
            //    Console.WriteLine(file);

            //Console.WriteLine("\r\nPress any key...");
            //Console.ReadKey();

        }

        private static void WriteToFile(List<string> promoCodes)
        {
            using (StreamWriter sw = new StreamWriter(@"D:\SOFT_SERVE_OLD_PC\Promo codes\SZGR\SZGR_99.txt", false, System.Text.Encoding.Default))
            {
                foreach (var code in promoCodes)
                    sw.WriteLine(code);
            }
        }
        private static void WriteToTwoSeparatedFile(List<string> promoCodes)
        {
            using (StreamWriter sw = new StreamWriter(@"D:\SOFT_SERVE_OLD_PC\Promo codes\SONYU\SONYU_1.txt", false, System.Text.Encoding.Default)) 
            using (StreamWriter sw1 = new StreamWriter(@"D:\SOFT_SERVE_OLD_PC\Promo codes\SONYU\SONYU_2.txt", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < 200000; i++)
                {
                    sw.WriteLine(promoCodes[i]);
                }
                for (int i = 200000; i < promoCodes.Count; i++)
                {
                    sw1.WriteLine(promoCodes[i]);
                }
            }
        }
        private static void WriteToFileToCheckInDB(List<string> promoCodes)
        {
            using (StreamWriter sw = new StreamWriter(@"D:\szgr_pregood.txt", false, System.Text.Encoding.Default))
            {
                foreach (var code in promoCodes)
                    sw.WriteLine("'"+code+"',");
            }
        }

        private static void WriteToFile_DeactivationFormat(List<string> promoCodes)
        {
            using (StreamWriter sw = new StreamWriter(@"D:\cmca_deactivation_format.txt", false, System.Text.Encoding.Default))
            {
                foreach (var code in promoCodes)
                    sw.WriteLine("(" + code + "),");
            }
        }

        private static void WriteToFile_ChangeEnrollmentLevelFormat(List<string> ids, List<string> levels, string pathToBeSave)
        {
            using (StreamWriter sw = new StreamWriter(pathToBeSave, false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    sw.WriteLine("(" + ids[i] + ", '" + levels[i].ToLower() +  "'),");
                }     
            }
        }

        private static void WriteToFile_EnableHTMLFormat(List<string> clien_codes, string pathToBeSave)
        {
            using (StreamWriter sw = new StreamWriter(pathToBeSave, false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < clien_codes.Count; i++)
                {
                    sw.WriteLine("'" + clien_codes[i].ToLower() + "',");
                }
            }
        }
    }
}
