using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services.Camera
{
    public interface ICameraService : IService
    {
        public UnityEngine.Camera Camera { get; }
        public void SetCamera(UnityEngine.Camera camera);

        public void ShakeCamera();
    }

    public class CameraService : ICameraService
    {
        public UnityEngine.Camera Camera { get; private set; }
        private CameraShakeStaticData _cameraSetting;

        public CameraService()
        {
            _cameraSetting = AllServices.Container.Single<IStaticDataService>()
                .GetStaticData<CameraShakeStaticData>();
        }

        public void SetCamera(UnityEngine.Camera camera)
        {
            Camera = camera;
        }

        public void ShakeCamera()
        {
            // DOTween.Kill(Camera);
            // DOTween.Sequence(Camera.DOShakePosition(_cameraSetting.Duration, _cameraSetting.Strength,
            //     _cameraSetting.Vibrato, _cameraSetting.Randomness));
        }
    }
}