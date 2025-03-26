using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.StaticData
{
    public interface IStaticDataService : IService
    {
        public T GetStaticData<T>() where T : ScriptableObject;
    }
}