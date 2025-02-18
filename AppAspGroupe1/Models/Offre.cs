using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppAspGroupe1.Models
{
    public class Offre
    {
        [Key]
        public int IdOffre { get; set; }

        [Display(Name = "description")]
        public string DescriptionOffre { get; set; }

        [Display(Name = "Prix Journalier"),Required(ErrorMessage ="*")]
        public float PrixJourOffre { get; set; }

        [Display(Name = "Disponibilite"), MaxLength(20)]

        public string Disponibilite { get; set; }

        public int IdAgence { get; set; }
        [ForeignKey("IdAgence")]

        public virtual Agence Agence { get; set; }

        public virtual ICollection<Agence> Agences { get; set; }

    }
}