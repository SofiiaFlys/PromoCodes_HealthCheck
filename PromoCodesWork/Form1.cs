using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using TakePromoCodes;

namespace PromoCodesWork
{
    public partial class Form1 : Form
    {
        PromoCodes promoCodes;
        public Form1()
        {
            InitializeComponent();


        }

        private async void ChoseFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog;
            openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String clientCode = textBox1.Text;
                DateTime expiryDate = DateTime.Now;
                DateTime creationDate = DateTime.Now;
                List<String> codes = new List<String>();
                promoCodes = new PromoCodes(clientCode, expiryDate, creationDate, codes);
                await promoCodes.ReadCodesFromFilAsync(openFileDialog.FileName);
                if (promoCodes.Codes.Count==0)
                    MessageBox.Show(String.Format("There are no promo code in choosen file {0}. Could you please load another file with promo codes!", openFileDialog.FileName));
                else
                    MessageBox.Show(String.Format("Promo codes from choosen file {0} were loaded!", openFileDialog.FileName));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            if (promoCodes == null || promoCodes.Codes.Count == 0)
            {
                MessageBox.Show("There are no promo codes to check on uniqueness. Could you please load file with promo codes!");
            }
            else
            {
                try
                {
                    promoCodes.DuplicatesInOneFile();
                    if (promoCodes.DuplicatedCodes.Count == 0)
                        MessageBox.Show("No duplicates in one file. All promo codes are unique!");
                }
                catch (PromoCodesException ex)
                {
                    String duplicatedCodes = String.Join(", ", ex.PromoCode.ToArray());
                    MessageBox.Show(String.Format(ex.Message) + ": " + duplicatedCodes);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (promoCodes == null || promoCodes.Codes.Count==0)
                {
                MessageBox.Show("There are no promo codes to check if they are already existing. Could you please browse file with promo codes!");
            }
            else
            {
                String requiredPromoCodesQueryFormat = promoCodes.MakePromoCodesFormatForDBCheckQuery();
                string connectionString = @"server=snowball-uat-rds.c2pbc0gedoix.us-east-1.rds.amazonaws.com;userid=service;password=lucent-great-yuletide-midst-crime-marlin;database=snowball";
                List<String> existedPromoCodes = promoCodes.CheckIfRequiredCodesAlreadyExistInMariaDB(connectionString,requiredPromoCodesQueryFormat);
                if (existedPromoCodes.Count == 0)
                    MessageBox.Show("There are any required promo code which is already existing. All promo codes are new!");
                else
                {
                    String existedCodes = String.Join(", ", existedPromoCodes.ToArray());
                    MessageBox.Show(String.Format("There are {0} alredy existed promo codes from file: {1}", existedPromoCodes.Count.ToString(), existedCodes));
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (promoCodes == null || promoCodes.Codes.Count == 0)
            {
                MessageBox.Show("There are no promo codes. Could you please browse file with promo codes!");
            }
            else
            {
                promoCodes.MakePromoCodesInRequiredFormat();
                MessageBox.Show("Promo Codes in Required Format!");
            }
        }
    }
}
