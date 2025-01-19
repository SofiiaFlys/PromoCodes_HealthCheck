using System;
using System.Collections.Generic;
using System.Text;

namespace TakePromoCodes
{
    public class PromoCodesException : Exception
    {
        public List<String> PromoCode { get; }

        public PromoCodesException() { }

        public PromoCodesException(string message)
            : base(message) { }

        public PromoCodesException(string message, Exception inner)
            : base(message, inner) { }

        public PromoCodesException(string message, List<String> promoCode)
            : this(message)
        {
            PromoCode = promoCode;
        }
    }
}
