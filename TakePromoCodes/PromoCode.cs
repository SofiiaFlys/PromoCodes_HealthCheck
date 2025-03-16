using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TakePromoCodes
{
    class PromoCode : ICodeValidator
    {
        [Required(ErrorMessage = "Code field is required.")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Only alphanumeric characters (0-9, A-Z, a-z) are allowed.")]
        public String Code { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [TodayOrFutureDate(ErrorMessage = "The expiry date must be today or a future date.")]
        public DateTime ExpiryDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        public bool IsValid()
        {
            DateTime todayDate = DateTime.Now;
            bool isValid = todayDate >= CreationDate && todayDate <= ExpiryDate ? true : false;
            return isValid;
        }
    }
}
