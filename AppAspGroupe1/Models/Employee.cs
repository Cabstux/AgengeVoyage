using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppAspGroupe1.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Display(Name = "Nom Prenom"), Required(ErrorMessage = "*"), MaxLength(150)]
        public string Name { get; set; }

        [Display(Name = "Age"), Required(ErrorMessage = "*")]
        public int Age { get; set; }

        [Display(Name = "Etat"), Required(ErrorMessage = "*")]
        public string State { get; set; }

        [Display(Name = "Pays"), Required(ErrorMessage = "*"), MaxLength(150)]
        public string Country { get; set; }
    }

}