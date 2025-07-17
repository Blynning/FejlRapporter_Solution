using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ErrorReport
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Skriv dit navn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Skriv din E-Mail"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Skriv dit telefon-nummer"), Phone]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Telefonnummer skal være 8 cifre")]
        public string Phonenumber { get; set; }

        [Required(ErrorMessage = "Beskriv problemet")]
        public string ErrorDescription { get; set; }

        public ErrorStatus Status { get; set; } = ErrorStatus.Ny;
    }
    public enum ErrorStatus
    {
        Ny = 0,
        Fuldført = 1,
        Uløselig = 2,
        Slettet = 3
    }
}
