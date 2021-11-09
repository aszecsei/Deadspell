using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Faction")]
    public class Faction : SerializedScriptableObject
    {
        public enum Response
        {
            Ignore,
            Attack,
            Flee,
        }
        
        public string Name;
        public Response DefaultResponse = Response.Ignore;
        public Dictionary<Faction, Response> Responses = new Dictionary<Faction, Response>();

        public Response ResponseTo(Faction other)
        {
            if (Responses.TryGetValue(other, out var response))
            {
                return response;
            }

            return DefaultResponse;
        }
    }
}