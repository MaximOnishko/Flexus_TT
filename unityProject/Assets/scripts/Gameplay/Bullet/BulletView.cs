using UnityEngine;

namespace Gameplay.Bullet
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        
        public void SetMesh(Mesh mesh)
        {
            meshFilter.mesh = mesh;
        }
    }
}