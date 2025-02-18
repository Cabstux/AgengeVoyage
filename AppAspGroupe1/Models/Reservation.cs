using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppAspGroupe1.Models
{
    public class Reservation
    {
        [Key]
        public int IdReservation { get; set; }
    }
}