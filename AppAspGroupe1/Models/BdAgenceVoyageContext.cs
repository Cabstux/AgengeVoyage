using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AppAspGroupe1.Models
{
    public class BdAgenceVoyageContext:DbContext
    {
        public BdAgenceVoyageContext():base("ConnAgenceVoyage")
        {

        }

        public DbSet <Chauffeur> Chauffeurs { get; set; }

        public DbSet<Utilisateur> utilisateurs { get; set; }
        public DbSet<Gestionnaire> gestionnaires { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Offre> Offres { get; set; }
        public DbSet<Agence> Agences { get; set; }

        public DbSet<Annonce> annonces { get; set; }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Td_Erreur> td_Erreurs { get; set; }

        



    }
}