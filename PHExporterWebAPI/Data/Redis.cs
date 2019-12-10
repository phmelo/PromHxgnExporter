using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Linq;
using PHExporterWebAPI.Models;


namespace PHExporterWebAPI.Data
{
    public class Redis
    {
        static public Dictionary<String, AgencyEvent> GetEventData(string strconn)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(strconn);
            //var server = redis.GetServer(someServer);
            IDatabase db = redis.GetDatabase();
            var hashKey = "cad-events";
            var AgencyEventRedisResult = db.HashGetAll(hashKey).ToDictionary(
                            x => x.Name.ToString(),
                            x => JsonConvert.DeserializeObject<AgencyEvent>(x.Value.ToString()),
                            StringComparer.Ordinal);

            var RedisStatics = redis.GetCounters();
            var RedisConfig = redis.Configuration;

            redis.Close();
            redis.Dispose();
            return AgencyEventRedisResult;
            // double RedisTotEvt = db.HashLength(hashKey);

        }

        static public Dictionary<String, Unit> GetUnitData(string hashKey, string strconn)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(strconn);
            //var server = redis.GetServer(someServer);
            IDatabase db = redis.GetDatabase();
            var RedisResult = db.HashGetAll(hashKey).ToDictionary(
                            x => x.Name.ToString(),
                            x => JsonConvert.DeserializeObject<Unit>(x.Value.ToString()),
                            StringComparer.Ordinal);

            var RedisStatics = redis.GetCounters();
            var RedisConfig = redis.Configuration;

            redis.Close();
            redis.Dispose();
            return RedisResult;

        }

        static public Dictionary<String, SessionRedisEntries> GetSessiontData(string strconn)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(strconn);
            //var server = redis.GetServer(someServer);
            var hashKey = "session";
            IDatabase db = redis.GetDatabase();
            var dtResult = new Dictionary<string, Models.SessionRedisEntries>();

            var redisR = db.HashGetAll(hashKey);
            foreach (var item in redisR)
            {
                var t = JsonConvert.DeserializeObject<Models.SessionRedis>(item.Value.ToString());
                if (dtResult.ContainsKey(t.Entries.employeeId))
                {
                    var SessionDuplicated = dtResult[t.Entries.employeeId];
                    SessionDuplicated.SessionCount++;
                    SessionDuplicated.positionIds += " " + SessionDuplicated.positionId;
                    SessionDuplicated.positionIds = SessionDuplicated.positionIds.Trim();
                    dtResult[t.Entries.employeeId] = SessionDuplicated;
                }
                else
                {
                    t.Entries.SessionCount = 0;
                    dtResult.Add(t.Entries.employeeId, t.Entries);
                }
            }

            redis.Close();
            redis.Dispose();
            return dtResult;

        }

        static public int GetBusLen(string strconn)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(strconn);

            IServer test = redis.GetServer("10.40.11.43:6389");
            //var server = redis.GetServer(someServer);
            //IDatabase db = redis.GetDatabase();
            //var RedisResult = db.HashGetAll(hashKey).ToDictionary(
            //                x => x.Name.ToString(),
            //                x => JsonConvert.DeserializeObject<ConsoleAppPrometheusNet.Data.Model.UnitRedis>(x.Value.ToString()),
            //                StringComparer.Ordinal);

            //var RedisStatics = redis.GetCounters();
            //var RedisConfig = redis.Configuration;

            redis.Close();
            redis.Dispose();
            return 0;

        }

    }
}
