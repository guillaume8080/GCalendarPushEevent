using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EcrireEventGCalendar
{
    class DAO
    {
        public static SqlConnection GetDBConnection(string datasource, string database, string username, string password)
        {
            //
            // Data Source=TRAN-VMWARE\SQLEXPRESS;Initial Catalog=simplehr;Persist Security Info=True;User ID=sa;Password=12345
            //
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }

        public static List<EvenementFormat> exportEvent()
        {
            List<EvenementFormat> listeEventsFormat = new List<EvenementFormat>();
            SqlConnection maco = null;
            string sql = @"select Collaborateur.Email, RDV.Libelle, RDV.Date_Debut, RDV.Date_Fin , RDV.Lieu,RDV.Notes
                           from RDV
                           inner join Affaire on Affaire.IdAffaire = RDV.IdAffaire
                           inner join Affaire_has_Collaborateur on Affaire_has_Collaborateur.IdAffaire = Affaire.IdAffaire
                           inner join Collaborateur on Collaborateur.IdCollaborateur = Affaire_has_Collaborateur.IdCollaborateur
                           inner join contact on Contact.IdContact =Affaire.IdContact
                           where RDV.Date_Debut >= Convert(datetime, '2021-04-01' )
                           and Collaborateur.IdCollaborateur = 972
                                                                                                                                     ";
            try
            {
                maco = GetDBConnection("192.168.0.207,49179", "orkeis-cf-test", "usr_lecture", "lecture");
                //Requête
                SqlCommand selectCommand = new SqlCommand
                {
                    Connection = maco,
                    CommandText = sql
                };
                SqlDataAdapter adapter = new SqlDataAdapter(selectCommand); // Permet de lire les données
                DataSet data = new DataSet(); // Contiendra les données
                maco.Open();
                adapter.Fill(data, "tableRetour"); //Récupère les données
                if (data.Tables["tableRetour"].Rows.Count > 0)
                {

                    for (int i = 0; i < data.Tables["tableRetour"].Rows.Count; i++)
                    {
                        EvenementFormat evFor = new EvenementFormat();

                        if (!DBNull.Value.Equals(data.Tables["tableRetour"].Rows[i]["Email"]))
                        {
                            evFor.Detenteteur = (string)data.Tables["tableRetour"].Rows[i]["Email"];
                        }
                        if (!DBNull.Value.Equals(data.Tables["tableRetour"].Rows[i]["Libelle"]))
                        {
                            evFor.intitule = (string)data.Tables["tableRetour"].Rows[i]["Libelle"];
                        }
                        if (!DBNull.Value.Equals(data.Tables["tableRetour"].Rows[i]["Date_Debut"]))
                        {
                            DateTime dt = DateTime.Parse(data.Tables["tableRetour"].Rows[i]["Date_Debut"].ToString());
                            evFor.Debut = dt.ToString("dd/MM/yyyy HH:mm:ss");
                        }
                        if (!DBNull.Value.Equals(data.Tables["tableRetour"].Rows[i]["Lieu"]))
                        {
                            evFor.adresse = (string)data.Tables["tableRetour"].Rows[i]["Lieu"];
                        }
                        if (!DBNull.Value.Equals(data.Tables["tableRetour"].Rows[i]["Notes"]))
                        {
                            evFor.descritpion = (string)data.Tables["tableRetour"].Rows[i]["Notes"];
                        }
                        if (!DBNull.Value.Equals(data.Tables["tableRetour"].Rows[i]["Date_Fin"]))
                        {
                         
                            DateTime dt = DateTime.Parse(data.Tables["tableRetour"].Rows[i]["Date_Fin"].ToString());
                            evFor.Fin = dt.ToString("dd/MM/yyyy HH:mm:ss");
                        }
                        listeEventsFormat.Add(evFor);

                    }

                    maco.Close();
                }
            }




            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);

            }

            return listeEventsFormat;

        }

    }
}
