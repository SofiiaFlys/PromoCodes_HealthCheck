using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TakePromoCodes
{
    public class PromoCodesManipulation
    {
        private PromoCodes m_requiredPromoCodes;
        public PromoCodes RequiredPromoCodes
        {
            get { return m_requiredPromoCodes; }
            set { m_requiredPromoCodes = value; }
        }

        public PromoCodesManipulation()
        {
            RequiredPromoCodes.Codes = new List<String>();
        }

        public PromoCodesManipulation(PromoCodes requiredPromoCodes)
        {
            m_requiredPromoCodes = requiredPromoCodes;
        }

        public void ChangePromoCodesFormat_SZGR()
        {
            List<string> replacedCodesInRightFormat = new List<string>();
            foreach (var code in RequiredPromoCodes.Codes)
            {
                if (!code.Equals(String.Empty))
                {
                    var delimitedValues = code.Split(',');
                    var promoCode = delimitedValues[1].Replace('"', ' ').Trim();
                    replacedCodesInRightFormat.Add(promoCode);
                }
            }
            RequiredPromoCodes.Codes = replacedCodesInRightFormat;
        }

    }
}
