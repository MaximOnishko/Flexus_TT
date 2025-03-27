using System.Collections.Generic;
using CodeBase.StaticData;
using Infrastructure.Services.CustomPhysics;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.CustomDrawer
{
    public class RenderTextureDrawerService : IDrawerService
    {
        private const int _hitTextureHeight = 64;
        private const int RendererTextureSize = 512;
        
        private const string RenderTex = "_RenderTex";

        private readonly Dictionary<int, RenderTexture> _renderTextures = new();
        private readonly BulletStaticData _bulletData;

        public RenderTextureDrawerService(IStaticDataService staticDataService)
        {
            _bulletData = staticDataService.GetStaticData<CannonStaticData>().BulletStaticData;
        }

        public void DrawHit(TrajectoryData.HitData hitData)
        {
            var compensativeScale = 1f / hitData.Collider.transform.localScale.x;
            DrawTexture(hitData.UV, GetRendererTexture(hitData.Collider), compensativeScale);
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
        
        private void DrawTexture(Vector2 uv, RenderTexture rendererTexture, float compensativeScale)
        {
            RenderTexture.active = rendererTexture;
            GL.PushMatrix();
            GL.LoadPixelMatrix(0, rendererTexture.width, rendererTexture.height, 0);
            
            float posX = uv.x * rendererTexture.width - (_hitTextureHeight * compensativeScale) / 2f;
            float posY = (1 - uv.y) * rendererTexture.height - _hitTextureHeight / 2f;

            Graphics.DrawTexture(
                new Rect(posX, posY, _hitTextureHeight * compensativeScale, _hitTextureHeight),
                _bulletData.HitTexture);

            GL.PopMatrix();
            RenderTexture.active = null;
        }
    }
}