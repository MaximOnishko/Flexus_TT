using System.Collections.Generic;
using CodeBase.StaticData;
using Infrastructure.Services.CustomPhysics;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.CustomDrawer
{
    public class RenderTextureDrawerService : IDrawerService
    {
        private Dictionary<int, RenderTexture> _renderTextures = new();

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
           // Debug.Log($"{collider.GetInstanceID()}");
            
            return _renderTextures.TryGetValue(collider.GetInstanceID(), out var texture)
                ? texture
                : CreateNewRenderTexture(collider);
        }

        private RenderTexture CreateNewRenderTexture(Collider collider)
        {
            var rt = new RenderTexture(512, 512, 32, RenderTextureFormat.ARGB32);
            rt.Create();

            var meshRenderer = collider.GetComponent<MeshRenderer>();
            var newMaterial = new Material(collider.GetComponent<MeshRenderer>().material);
            meshRenderer.material = newMaterial;

            newMaterial.SetTexture("_RenderTex", rt);
            ClearRenderTexture(rt);

            _renderTextures.Add(collider.GetInstanceID(), rt);
            return rt;
        }

        void ClearRenderTexture(RenderTexture rt)
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

            float posX = uv.x * rendererTexture.width - (64 * compensativeScale) / 2f;
            float posY = (1 - uv.y) * rendererTexture.height - 64 / 2f;

            Graphics.DrawTexture(
                new Rect(posX, posY, 64 * compensativeScale, 64),
                _bulletData.HitTexture);

            GL.PopMatrix();
            RenderTexture.active = null;
        }
    }
}