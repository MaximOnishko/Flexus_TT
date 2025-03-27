using UnityEngine;

public static class CubeGeometry
{
    public static readonly Vector3[] originalVertices =
    {
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, 0.5f, -0.5f),
        new Vector3(0.5f, 0.5f, -0.5f),
        new Vector3(0.5f, -0.5f, -0.5f),

        new Vector3(-0.5f, -0.5f, 0.5f),
        new Vector3(-0.5f, 0.5f, 0.5f),
        new Vector3(0.5f, 0.5f, 0.5f),
        new Vector3(0.5f, -0.5f, 0.5f),
    };

    public static readonly int[] triangles = new[]
    {
        0, 1, 2, 2, 3, 0,
        1, 5, 6, 6, 2, 1,
        3, 2, 6, 6, 7, 3, 
        4, 0, 3, 3, 7, 4,
        4, 5, 1, 1, 0, 4,
        7, 6, 5, 5, 4, 7,
    };
}