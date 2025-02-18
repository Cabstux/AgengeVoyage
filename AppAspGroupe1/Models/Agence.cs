using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAspGroupe1.Models
{
    public class Agence
    {
        [Key]
        public int IdAgence { get; set; }

        [Display(Name = "AddresseAgence"), Required(ErrorMessage = "*"), MaxLength(150)]
        public string AddresseAgence { get; set; }

        [Display(Name = "Lattitude")]
        public float? Lattitude { get; set; }

        [Display(Name = "Longitude")]
        public float Longitude { get; set; }

        [Display(Name = "Ninea"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string NineaGestionnaire { get; set; }

        [Display(Name = "Rcm"), Required(ErrorMessage = "*"), MaxLength(20)]
        public string RcmGestionnaire { get; set; }

        //pour avoir tous les offres de l'agence direct sans faire de requetes
        public virtual ICollection<Offre> Offres { get; set; }
        public int? IdGestionnaire { get; set; } 
        [ForeignKey("IdGestionnaire")]
        public virtual Gestionnaire Gestionnaire { get; set; }


       



    }
}