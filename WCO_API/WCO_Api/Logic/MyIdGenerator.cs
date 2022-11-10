using WCO_Api.Models;
using WCO_Api.Repository;
using WCO_Api.WEBModels;

/*
 * Clase que servirá para poder generar y validar el identificador alfanumérico de los torneos 
*/

namespace WCO_Api.Logic
{
    public class MyIdGenerator
    {

        //Se crea un managementRepositiry para hacer consultas a la base de datos en caso de ser necesario
        TournamentRepository tournamentRepository = new TournamentRepository();


        /* Función que me permite crear una llave alfanumérica única
         * Entradas: Ninguna
         * Salidas: Una llave alfanumérica
         * Restricciones: Ninguna
         */
        public string GetUUID()
        {

            var uuid = Guid.NewGuid().ToString();

            //Shorten to 6 characters 
            uuid = uuid.Substring(0, 6);

            while (!isUUIDUnique(uuid).Result)
            {

                uuid = GetUUID();
            }

            return uuid;

        }

        /* Función que me permite avergiguar si una llave alfanumérica creada es igual a otra
         * existente en la lista de torneos
         * Entradas: Una llave alfanumérica como string
         * Salidas: booleano indicando si está repetida o no
         * Restricciones: Entrada debe ser un string
         */

        private async Task<bool> isUUIDUnique(string uuid)
        {

            //Se revisa si existe el UUID en la base de datos, en este caso se revisa tournaments
            //de manera local

            IEnumerable<TournamentOut> tournaments;
            tournaments = await tournamentRepository.getTournaments();

            foreach (var dbTournament in tournaments)
            {
                if (dbTournament.ToId == uuid)
                {
                    return false;
                }
            }

            return true;

        }

    }
}
