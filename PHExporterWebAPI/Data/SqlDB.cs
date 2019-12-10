using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PHExporterWebAPI.Models;

namespace PHExporterWebAPI.Data
{
    public class SqlDB
    {
       static public IEnumerable<AgencyEvent> GetEventData(string strconn)
       {
            string sqlEvents = "select ag_id Agency,num_1 AgencyEventId,dgroup DispatchGroup,status_code EventStatusCode,priority Priority from agency_event WITH (NOLOCK) where is_open = 'T'";
            using (SqlConnection conexao = new SqlConnection(strconn))
            {
                return conexao.Query<AgencyEvent>(sqlEvents);
            }
       }

        static public List<AgencyEvent> GetEventDataList(string strconn)
        {
            string sqlEvents = "select ag_id Agency,num_1 AgencyEventId,dgroup DispatchGroup,status_code EventStatusCode,priority Priority from agency_event WITH (NOLOCK) where is_open = 'T'";

            using (SqlConnection conexao = new SqlConnection(strconn))
            {
                return conexao.Query<AgencyEvent>(sqlEvents).ToList();
            }
        }

        static public IEnumerable<Unit> GetUnitData(string strconn)
        {
            string sqlUnits = "select ag_id Agency,num_1 AssignedEventId,dgroup DispatchGroup,unid UnitId, unit_status CurrentStatus from cd_units WITH (NOLOCK)";
            using (SqlConnection conexao = new SqlConnection(strconn))
            {
                return conexao.Query<Unit>(sqlUnits);
            }
        }

        static public IEnumerable<Unit> GetTotalUnitsPerStatus(string strconn)
        {
            string sqlUnits = "select ag_id Agency,num_1 AssignedEventId,dgroup DispatchGroup,unid UnitId, unit_status CurrentStatus from cd_units WITH (NOLOCK)";
            using (SqlConnection conexao = new SqlConnection(strconn))
            {
                return conexao.Query<Unit>(sqlUnits);
            }
        }

        static public int GetEventCreatedWithoutCallerIdReceived(string strconn)
        {
            string sql = "select ABS(DATEDIFF(MINUTE,(select max (cdts2) from an_al WITH (NOLOCK)),(select max (ad_ts2) from agency_event WITH (NOLOCK))))";
            using (SqlConnection conexao = new SqlConnection(strconn))
            {
                var result = conexao.ExecuteScalar<int>(sql);
                return result;
            }
        }
    }
}
