using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIS.Models
{
    public class Login
    {
        [Key]
        public int loginid { get; set; }
        [Index(IsUnique = true)]
        [Column(TypeName = "Varchar")]
        [StringLength(40)]
        [Required(ErrorMessage = "Please enter your Email Address")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in valid format. Ex. of correct email: paragjain@gmail.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{6,}", ErrorMessage = "Your password must be at least 6 characters long and contain at least 1 letter and 1 number")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your Type")]
        public string type { get; set; }
    }

    //public enum Type
    //{
    //    User = 1,
    //    Dietician = 2
    //}
}