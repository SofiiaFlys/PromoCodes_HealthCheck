﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Net.Http;

namespace TakePromoCodes
{
    public class PromoCodes: BaseCodes
    {
        private String m_clientCode;
        private DateTime m_expiryDate;
        private DateTime m_creationDate;
        private List<String> m_codes;
        private List<String> m_duplicated_codes;
        public String ClientCode{ get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime creationDate { get; set; }

        public Func<String, String> Format;
        //public List<String> Codes { get; set; }
        public List<String> DuplicatedCodes
        {
            get { return m_duplicated_codes; }
            set { m_duplicated_codes = value; }
        }
        public override List<String> Codes
        {
            get { return m_codes; }
            set { m_codes = value; }
        }


        private PromoCodes() 
        {
            m_codes = new List<String>();
            m_duplicated_codes = new List<string>();
        }

        public PromoCodes(List<String> codes)
        {
            m_clientCode = "default";
            m_expiryDate = DateTime.Now;
            m_creationDate = DateTime.Now;
            m_codes = new List<String>();
            foreach (var code in codes)
            {
                m_codes.Add(code);
            }
            m_duplicated_codes = new List<string>();
        }

        public PromoCodes(String clientCode, DateTime expiryDate, DateTime creationDate, List<String> codes)
        {
            m_clientCode = clientCode;
            m_expiryDate = expiryDate;
            m_creationDate = creationDate;
            m_codes = new List<String>();
            foreach (var code in codes)
            {
                m_codes.Add(code);
            }
            m_duplicated_codes = new List<string>();
        }

        public void ReadCodesFromFile(String path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!line.Equals(String.Empty) && !String.IsNullOrWhiteSpace(line))
                            Codes.Add(line);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<bool> DownloadFileFromApiAsync(string apiUrl, string localFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    using (FileStream fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fileStream);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error downloading file: {ex.Message}");
                    return false;
                }
            }
        }

        public async Task ReadCodesFromFileApiAsync(String apiUrl)
        {
            String localFilePath = "downloadedPromoCodes.txt";

            bool downloadSuccess = await DownloadFileFromApiAsync(apiUrl, localFilePath);

            if (downloadSuccess && File.Exists(localFilePath))
            {
                await ReadCodesFromFileAsync(localFilePath);
            }
            else
            {
                Console.WriteLine("Failed to download or access the file.");
            }
        }

        public async Task ReadCodesFromFileAsync(String filePath)
        {
            Console.WriteLine("Reading file line by line:");

            using (StreamReader reader = new StreamReader(filePath))
            {
                String line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (!line.Equals(String.Empty) && !String.IsNullOrWhiteSpace(line))
                        Codes.Add(line);
                }
            }
        }

        public void ReadCodesFromFile_WithoutTryCatch(String path)
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.Equals(String.Empty) && !String.IsNullOrWhiteSpace(line))
                        Codes.Add(line);
                }
            }
        }

        public List<String> CheckIfRequiredCodesAlreadyExistInMariaDB(string connectionString, String requiredPromoCodesQueryFormat)
        {
            List<String> existedRequiredPromoCodes = new List<String>();
            using (var con = new MySqlConnection(connectionString))
            {
                con.Open();
                String sql = String.Format("SELECT * FROM PromoCodes where Code in ({0})",requiredPromoCodesQueryFormat);
                var cmd = new MySqlCommand(sql, con);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    existedRequiredPromoCodes.Add(rdr.GetString(1));
                }
                con.Close();
            }
            return existedRequiredPromoCodes;
        }
        public void DuplicatesInOneFile()
        {
            DuplicatedCodes = Codes.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();
            if (DuplicatedCodes.Count > 0)
                throw new PromoCodesException("Duplicated codes are found in file", DuplicatedCodes);
        }

        public void RemoveDuplicates()
        {
            List<String> unique = new List<String>();
            unique.AddRange(Codes.Distinct());
            Codes = unique;
        }

        public List<String> FindAlreadyExistingPromoCodes(PromoCodes existingPromoCodes)
        {
            var alreadyExistingPromoCodes =
                from existingCode in existingPromoCodes.Codes
                where Codes.Any(x => x == existingCode)
                select existingCode;
            return alreadyExistingPromoCodes.ToList();
        }

        public String PromoCodeFormattedForDB(String code)
        {
            String requiredFormat = String.Empty;
            requiredFormat += String.Format("\'{0}\'", code);
            return requiredFormat;
        }

        public String MakePromoCodesFormatForDBCheckQuery()
        {
            Format = PromoCodeFormattedForDB;
            List<String> codeFormattedForDB = Codes.Select(str => Format(str)).ToList();
            String query = String.Join(',', codeFormattedForDB);
            return query;
        }
        
        public String PromoCodeFormatted (String code)
        {
            String requiredPromoCode = String.Empty;
            if (!code.Equals(String.Empty))
            {
                var delimitedValues = code.Split(',');
                if (delimitedValues.Length == 2)
                {
                    requiredPromoCode = delimitedValues[1].Replace('"', ' ').Trim();
                }
                else
                {
                    requiredPromoCode = delimitedValues[0];
                }
            }
            return requiredPromoCode;
        }

        public void MakePromoCodesInRequiredFormat()
        {
            Format = PromoCodeFormatted;
            List<String> Formatted = Codes.Select(str => Format(str)).ToList();
            Codes = Formatted;
        }
    }
}
