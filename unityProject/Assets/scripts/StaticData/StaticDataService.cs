using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataPath = "StaticData";
        
        private readonly List<ScriptableObject> _levels;

        public StaticDataService()
        {
            _levels = Resources
                .LoadAll<ScriptableObject>(StaticDataPath)
                .ToList();
        }

        public T GetStaticData<T>() where T : ScriptableObject
        {
            var staticData =  _levels.FirstOrDefault(x => x is T) as T;

            if (staticData == null)
                throw new System.Exception($"StaticData not found for {typeof(T)}");
            
            return staticData;
        }
    }
}