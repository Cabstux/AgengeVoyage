using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppAspGroupe1.Models
{
    public class Gestionnaire:Utilisateur

    {
        

        [Display(Name = "CNI"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string CNIGestionnaire { get; set; }

        public virtual ICollection <Agence> Agences { get; set; }



    }
}