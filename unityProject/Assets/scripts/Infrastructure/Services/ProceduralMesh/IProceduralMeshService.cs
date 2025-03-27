using UnityEngine;

namespace CodeBase.Infrastructure.Services.ProceduralMesh
{
    public class IProceduralMeshService : IService
    {
        
    }

    public class ProceduralMeshService : IProceduralMeshService
    {
        public Mesh GetRandomCubeMesh(float geometryOffset)
        {
            Vector3[] originalVertices = (Vector3[])CubeGeometry.originalVertices.Clone();

            for (int i = 0; i < originalVertices.Length; i++) 
                originalVertices[i] += Random.insideUnitSphere * geometryOffset;
        
            Mesh mesh = new Mesh
            {
                vertices = originalVertices,
                triangles = CubeGeometry.triangles,
            };

            mesh.RecalculateNormals();

            return mesh;
        }
    }
}