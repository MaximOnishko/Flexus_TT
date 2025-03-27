using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.ProceduralMesh;
using CodeBase.StaticData;
using Gameplay.Bullet;
using UnityEngine;

namespace Gameplay.BulletFactory
{
    public class BulletPoolService : IBulletPool
    {
        private readonly IGameFactory _gameFactory;
        private readonly IProceduralMeshService _proceduralMeshService;
      
        private readonly List<BulletView> _bullets = new(SIZE);
        private readonly Vector3 _defaultSpawnPos = Vector3.down * 10f;
        private readonly float _bulletMeshRandom;
        
        private const int SIZE = 10;

        private Transform _parent;


        public BulletPoolService(IGameFactory gameFactory, IProceduralMeshService proceduralMeshService,
            IStaticDataService staticDataService)
        {
            _bulletMeshRandom = staticDataService.GetStaticData<CannonStaticData>().BulletStaticData.BulletMeshRandomOffset;
            _proceduralMeshService = proceduralMeshService;
            _gameFactory = gameFactory;
        }

        public void Init()
        {
            _parent = new GameObject("[BulletPool]").transform;

            for (int i = 0; i < SIZE; i++)
                AddBullet(_defaultSpawnPos);
        }

        public void ReturnBullet(BulletView bulletView)
        {
            bulletView.gameObject.SetActive(false);
        }

        public BulletView GetBullet(Vector3 at)
        {
            foreach (var bullet in _bullets)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    bullet.transform.position = at;
                    bullet.gameObject.SetActive(true);
                    return bullet;
                }
            }

            return AddBullet(at, true);
        }

        private BulletView AddBullet(Vector3 at, bool setActive = false)
        {
            var bullet = _gameFactory.Instantiate<BulletView>(AssetsAddress.BulletPath, at, _parent);
            bullet.gameObject.SetActive(setActive);
            bullet.SetMesh(_proceduralMeshService.GetRandomCubeMesh(_bulletMeshRandom));
            
            _bullets.Add(bullet);

            return bullet;
        }
    }
}