using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace TakePromoCodes
{
    public class PromoCodes
    {
        private String m_clientCode;
        private DateTime m_expiryDate;
        private DateTime m_creationDate;
        private List<String> m_codes;
        public String ClientCode{ get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime creationDate { get; set; }
        //public List<String> Codes { get; set; }
        public List<String> Codes
        {
            get { return m_codes; }
            set { m_codes = value; }
        }


        private PromoCodes() 
        {
            m_codes = new List<String>();
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
        }

        public void ReadCodesFromFile(String path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    String line;
                    line = sr.ReadLine();
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
        public List<String> DuplicatesInOneFile()
        {
            List<String> duplicates = new List<String>();
            for (int i = 0; i < Codes.Count; i++)
            {
                for (int j = i + 1; j < Codes.Count; j++)
                {
                    if (i != j && Codes[i] == Codes[j])
                    {
                        if (!duplicates.Contains(Codes[i]))
                            duplicates.Add(Codes[i]);
                            //Codes.RemoveAt(i);
                        break;
                    }
                }
            }
            return duplicates;
        }

        public List<String> FindAlreadyExistingPromoCodes(PromoCodes existingPromoCodes)
        {
            List<String> alreadyExistingPromoCodes = new List<String>();
            foreach (var code in Codes)
            {
                foreach (var existingCode in existingPromoCodes.Codes)
                {
                    if (code == existingCode)
                    {
                        alreadyExistingPromoCodes.Add(code);
                        break;
                    }
                }
            }
            return alreadyExistingPromoCodes;
        }

        public String MakePromoCodesFormatForDBCheckQuery()
        {
            String requiredFormat = String.Empty;
            foreach(var code in Codes)
            {
                requiredFormat += String.Format("\'{0}\',", code);
            }
            int pos = (requiredFormat.Length) - 1;
            requiredFormat = requiredFormat.Remove(pos);
            return requiredFormat;
        }

        public void MakePromoCodesInRequiredFormat()
        {
            List<string> requiredFormat = new List<string>();
            foreach (var code in Codes)
            {
                if (!code.Equals(String.Empty))
                {
                    var delimitedValues = code.Split(',');
                    if (delimitedValues.Length == 2)
                    {
                        var promoCode = delimitedValues[1].Replace('"', ' ').Trim();
                        requiredFormat.Add(promoCode);
                    }
                    else
                    {
                        requiredFormat.Add(delimitedValues[0]);
                    }
                }
            }
            Codes = requiredFormat;
        }
    }
}
