using Cannon;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void Cleanup();
        GameObject Instantiate(string prefabPath);
        GameObject Instantiate(string prefabPath, Vector3 position);
        CannonController GetCannon(Vector3 position);
        T Instantiate<T>(string prefabPath) where T : MonoBehaviour;
        T Instantiate<T>(string prefabPath, Vector3 position) where T : MonoBehaviour;
    }
}