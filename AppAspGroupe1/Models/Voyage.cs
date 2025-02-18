using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppAspGroupe1.Models
{
    public class Voyage
    {
        [Key]
        public int IdVoyage { get; set; }


      

        [Display(Name = "Prix"), Required(ErrorMessage = "*")]
        public float Prix { get; set; }
    }
}