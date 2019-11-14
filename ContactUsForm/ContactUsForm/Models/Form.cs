using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactUsForm.Models
{
    public class Form
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [RegularExpression(@"([^!@#$%^&*()_+\-=`~0123456789.\/\\|;<>:])", ErrorMessage = "First Name is Invalid")]
        public string FirstName { get; set; } 

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"([^!@#$%^&*()_+\-=`~0123456789.\/\\|;<>:])", ErrorMessage ="Last Name is Invalid")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"((^\d{5})|(^\d{5}-\d{4})|(^\d{9}$)|([a-zA-Z])\d([a-zA-Z])(.?)\d([a-zA-Z])\d)", ErrorMessage = "Postal Code is Invalid in US/Canada")]
        public string PostalCode { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+/.[A-Z]{2,}$", ErrorMessage = "Email Address is Invalid")]
        public string Email { get; set; }

        [Required]
        public string Topic { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone Number is Invalid")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Questions { get; set; }
    }
}
