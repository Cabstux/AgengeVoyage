using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppAspGroupe1.Models
{
    public class Flotte
    {
        [Key]
        public int IdFlotte { get; set; }


        [Display(Name = "TypeFlotte"), Required(ErrorMessage = "*"), MaxLength(50)]
        public string TypeFlotte { get; set; }
        [Display(Name = "MatriculeFlotte"), Required(ErrorMessage = "*"), MaxLength(50)]
        public string MatriculeFlotte { get; set; }
    }
}