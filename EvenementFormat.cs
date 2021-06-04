using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcrireEventGCalendar
{
    class EvenementFormat
    {
        private string _detenteur;
        private string _debut;
        private string _description;
        private string _adresse;
        private string _intitule;
        private string _fin;

        //TODO: contact lié au rdv

        public string Detenteteur
        {
            get => _detenteur;
            set => _detenteur = value;
        }

        public string Debut
        {
            get => _debut;
            set => _debut = value;

        }

        public string Fin
        {
            get => _fin;
            set => _fin = value;

        }

        public string descritpion
        {
            get => _description;
            set => _description = value;
        }

        public string adresse
        {
            get => _adresse;
            set => _adresse = value;
        }
        public string intitule
        {
            get => _intitule;
            set => _intitule = value;
        }


    }
}
