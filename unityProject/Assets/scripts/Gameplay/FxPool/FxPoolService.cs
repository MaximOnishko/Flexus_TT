using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.StaticData;
using Gameplay.Bullet;
using UnityEngine;

namespace Gameplay.FxPool
{
    public class FxPoolService : IFxPoolService
    {
        private const int SIZE = 10;
        private Transform _parent;

        private readonly List<GameObject> _fxs = new(SIZE);
        private readonly Vector3 _defaultSpawnPos = Vector3.down * 10f;
        
        private readonly IGameFactory _gameFactory;


        public FxPoolService(IGameFactory gameFactory)
        {
         
            _gameFactory = gameFactory;
        }

        public void Init()
        {
            _parent = new GameObject("[FxPool]").transform;

            for (int i = 0; i < SIZE; i++)
                AddFx(_defaultSpawnPos);
        }

        public GameObject GetFx(Vector3 at)
        {
            foreach (var bullet in _fxs)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    bullet.transform.position = at;
                    bullet.gameObject.SetActive(true);
                    return bullet;
                }
            }

            return AddFx(at, true);
        }

        public void ReturnFx(GameObject fx)
        {
            fx.gameObject.SetActive(false);
        }

        private GameObject AddFx(Vector3 at, bool setActive = false)
        {
            var fx = _gameFactory.Instantiate(AssetsAddress.BulletExplosionFxPath, at, _parent);
            fx.gameObject.SetActive(setActive);

            _fxs.Add(fx);

            return fx;
        }
    }
}