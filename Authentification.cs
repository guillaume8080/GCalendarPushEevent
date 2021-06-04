using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Services;



namespace EcrireEventGCalendar
{
    class Authentification
    {
        public static UserCredential makeAuthentication()
        {
            UserCredential cred = null;
           
                //Le jeton sera généré au chemin : C:\Users\<votreUtilisateur>\source\repos\<votreProjet>\bin\Debug\<laString>
                string credPath = "token.json";
                //objet identifiant l'user détenant du projet google nécessaire à toutes requetes
                cred = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        //Ces identifiants sont à récupérer à un endroit détaillé par la doc
                        ClientId = "555800897684-9a00bid8s5agkureq4rjl2rdj74uo8vs.apps.googleusercontent.com",
                        ClientSecret = "WMujgPJ4DVN8P-6I8bXVmoKh"

                    },
                        
                        new[] { CalendarService.Scope.CalendarEvents },
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;


                return cred;
            }
        }
}
