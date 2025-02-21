using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using AppAspGroupe1.Models;
using System.Diagnostics;

namespace AppAspGroupe1.App_Start
{
    public class Utils
    {
        BdAgenceVoyageContext db=new BdAgenceVoyageContext();

        public  void WriteDataError(string TitreErreur, string erreur)
        {
            try
            {
                Td_Erreur log = new Td_Erreur();
                log.DateErreur = DateTime.Now;
                log.TitreErreur = erreur.Length > 1000 ? erreur.Substring(0, 1000) : erreur ;
                log.TitreErreur = TitreErreur;
                db.td_Erreurs.Add(log);
                db.SaveChanges();
            }
            catch (Exception ex) {

                WriteLogSystem(erreur, TitreErreur);
                WriteLogSystem(ex.ToString(), "WriteDataError");
            }
        }
        public static void WriteLogSystem(string erreur, string libelle)
        {
            using (EventLog eventlog = new EventLog("Application"))
            {
                eventlog.Source = "SenDiop";
                eventlog.WriteEntry(string.Format("date:{0}, libelle:{1}"));
            }
        }
    }
}