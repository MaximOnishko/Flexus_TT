using Cannon;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticDataService;

        public GameFactory(IAssetProvider assetsProvider, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _assets = assetsProvider;
        }

        public void Cleanup()
        {
        }

        public CannonController GetCannon(Vector3 position)
        {
            var cannon = new CannonController();
            var view = Instantiate<CannonView>(AssetsAddress.CannonPath, position);

            cannon.Init(view, _staticDataService.GetStaticData<CannonStaticData>());

            return cannon;
        }

        public GameObject Instantiate(string prefabPath) =>
            Object.Instantiate(_assets.Instantiate(prefabPath));

        public GameObject Instantiate(string prefabPath, Vector3 position)
        {
            var go = Object.Instantiate(_assets.Instantiate(prefabPath));
            go.transform.position = position;
            return go;
        }

        public GameObject Instantiate(string prefabPath, Vector3 position, Transform parent)
        {
            var go = Object.Instantiate(_assets.Instantiate(prefabPath), parent);
            go.transform.position = position;
            return go;
        }

        public T Instantiate<T>(string prefabPath) where T : MonoBehaviour
        {
            var go = Object.Instantiate(_assets.Instantiate(prefabPath));
            return go.GetComponent<T>();
        }

        public T Instantiate<T>(string prefabPath, Vector3 position) where T : MonoBehaviour
        {
            var go = Object.Instantiate(_assets.Instantiate(prefabPath));
            go.transform.position = position;
            return go.GetComponent<T>();
        }

        public T Instantiate<T>(string prefabPath, Vector3 position, Transform parent) where T : MonoBehaviour
        {
            var go = Object.Instantiate(_assets.Instantiate(prefabPath), parent);
            go.transform.position = position;
            return go.GetComponent<T>();
        }
    }
}