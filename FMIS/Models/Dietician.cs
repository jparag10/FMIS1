using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FMIS.Models
{
    public class Dietician
    {
        
        [Key]
        public int did { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Index(IsUnique = true)]
        [Column(TypeName = "Varchar")]
        [StringLength(40)]
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Your email address is not in valid format. Ex. of correct email: paragjain@gmail.com")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{6,}", ErrorMessage = "Your password must be at least 6 characters long and contain at least 1 letter and 1 number")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your Contact no")]
        [RegularExpression(@"^([6-9]{1}[0-9]{9})$", ErrorMessage = "Please enter valid phone number")]
        public long Contact { get; set; }

        [Required(ErrorMessage = "Please enter your Location")]
        [RegularExpression(@"^[A-Za-z ]*$", ErrorMessage = "Please enter valid Location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please enter your Experience")]
        [RegularExpression(@"^[0-8]{0,1}[0-9]{0,1}", ErrorMessage = "Please enter valid experience")]
        public int Experience { get; set; }

        public string filepath { get; set; }
    }
}