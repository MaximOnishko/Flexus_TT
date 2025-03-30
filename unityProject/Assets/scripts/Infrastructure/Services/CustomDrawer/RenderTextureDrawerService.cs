using System.Collections;
using System.Collections.Generic;
using CodeBase.StaticData;
using Infrastructure.Services.CustomPhysics;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.CustomDrawer
{
    public class RenderTextureDrawerService : IDrawerService
    {
        private const int _hitTextureHeight = 84;
        private const int RendererTextureSize = 512;

        private const string RenderTex = "_RenderTex";

        private readonly Dictionary<int, RenderTexture> _renderTextures = new();
        private readonly BulletStaticData _bulletData;
        private readonly ICoroutineRunner _coroutineRunner;

        public RenderTextureDrawerService(IStaticDataService staticDataService)
        {
            _bulletData = staticDataService.GetStaticData<CannonStaticData>().BulletStaticData;
            _coroutineRunner = AllServices.Container.Single<ICoroutineRunner>();
        }

        public void DrawHit(TrajectoryData.HitData hitData)
        {
            var compensativeScale = 1f / hitData.Collider.transform.localScale.x;
            _coroutineRunner.StartCoroutine(HandleHitEffect(hitData.UV, GetRendererTexture(hitData.Collider),
                compensativeScale));
        }

        private RenderTexture GetRendererTexture(Collider collider)
        {
            return _renderTextures.TryGetValue(collider.GetInstanceID(), out var texture)
                ? texture
                : CreateNewRenderTexture(collider);
        }

        private RenderTexture CreateNewRenderTexture(Collider collider)
        {
            var rt = new RenderTexture(RendererTextureSize, RendererTextureSize, 32, RenderTextureFormat.ARGB32);
            rt.Create();

            var meshRenderer = collider.GetComponent<MeshRenderer>();
            var newMaterial = new Material(collider.GetComponent<MeshRenderer>().material);
            meshRenderer.material = newMaterial;

            newMaterial.SetTexture(RenderTex, rt);
            ClearRenderTexture(rt);

            _renderTextures.Add(collider.GetInstanceID(), rt);
            return rt;
        }

        private void ClearRenderTexture(RenderTexture rt)
        {
            RenderTexture.active = rt;
            GL.Clear(true, true, new Color(0, 0, 0, 0));
            RenderTexture.active = null;
        }

        private void DrawTexture(Vector2 uv, RenderTexture rendererTexture, float compensativeScale,
            Texture2D hitTexture, Material material)
        {
            RenderTexture.active = rendererTexture;
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, rendererTexture.width, rendererTexture.height, 0);

            float posX = uv.x * rendererTexture.width - (_hitTextureHeight * compensativeScale) / 2f;
            float posY = uv.y * rendererTexture.height - _hitTextureHeight / 2f;

    
            Graphics.DrawTexture(
                new Rect(posX, posY, _hitTextureHeight * compensativeScale, _hitTextureHeight),
                hitTexture,material);

            GL.PopMatrix();
            RenderTexture.active = null;
        }

        private IEnumerator HandleHitEffect(Vector2 uv, RenderTexture rendererTexture, float compensativeScale)
        {
            var material = new Material(Shader.Find("Sprites/Default"));
            material.color = GetRandomColor();

            DrawTexture(uv, rendererTexture, compensativeScale, _bulletData.HitTexture, material);

            float speed = 0f;
            float speedLerp = 0;
            float maxSpeedLerp = Random.Range(0.5f, 1.5f);

            float moveDown = Random.Range(0.05f, 0.2f);
            float startPos = uv.y;

            if (_bulletData.HitTextureMoveDownSpeed != 0)
            {
                while (uv.y > startPos - moveDown)
                {
                    // Debug.Log($"StartPos: {startPos}, MoveTo {startPos - moveDown}, Cur {uv.y}");

                    uv.y -= _bulletData.HitTextureMoveDownSpeed * Time.deltaTime;

                    DrawTexture(uv, rendererTexture, compensativeScale, _bulletData.TransparentHitTexture,
                        material);

                    yield return new WaitForSeconds(0.01f);
                }
            }

            yield return null;
        }
        
        Color GetRandomColor()
        {
            return new Color(Random.value, Random.value, Random.value, 0.5f);
        }
    }
}