using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace Gameplay.BulletFactory
{
    public class BulletPoolService : IBulletPool
    {
        private const int SIZE = 10;
        private readonly IGameFactory _gameFactory;

        private readonly List<Bullet> _bullets = new(SIZE);
        private Transform _parent;


        public BulletPoolService(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
          
        }

        public void Init()
        {
            _parent = new GameObject("[BulletPool]").transform;

            for (int i = 0; i < SIZE; i++)
                AddBullet();
        }

        public void ReturnBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        public Bullet GetBullet()
        {
            foreach (var bullet in _bullets)
            {
                if (!bullet.gameObject.activeSelf)
                    return bullet;
            }
            
            return AddBullet();
        }

        private Bullet AddBullet()
        {
            var bullet = _gameFactory.Instantiate<Bullet>(AssetsAddress.BulletPath, Vector3.down * 10f, _parent);
            bullet.gameObject.SetActive(false);
            _bullets.Add(bullet);

            return bullet;
        }
    }
}