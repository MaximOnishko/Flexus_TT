using UnityEngine;

namespace CodeBase.Infrastructure.Services.ProceduralMesh
{
    public interface IProceduralMeshService : IService
    {
        Mesh GetRandomCubeMesh(float geometryOffset);
    }
}