using CodeBase.Infrastructure.Services;

namespace Gameplay.BulletFactory
{
    public interface IBulletPool : IService
    {
        public Bullet GetBullet();
        public void ReturnBullet(Bullet bullet);
        void Init();
    }
}