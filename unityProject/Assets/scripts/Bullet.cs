using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public MeshFilter meshFilter;
    public float offset = 0.1f;
    public float timeStep = 0.01f;
    
    private List<Vector3> path;
    private Coroutine moveCoroutine;
    private float _bulletSpeed;

    private void Start()
    {
        meshFilter.mesh = GenerateRandomCube();
    }
    

    public void StartMove(List<Vector3> points, float bulletSpeed)
    {
        _bulletSpeed = bulletSpeed;
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        path = points;
        moveCoroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        if (path == null || path.Count == 0)
            yield break;

        foreach (Vector3 target in path)
        {
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, _bulletSpeed * timeStep);
                yield return new WaitForSeconds(timeStep);
            }
        }

        Destroy(gameObject);
    }


    private Mesh GenerateRandomCube()
    {
        Vector3[] originalVertices = (Vector3[])CubeGeometry.originalVertices.Clone();
        int[] triangles = CubeGeometry.triangles;

        for (int i = 0; i < originalVertices.Length; i++)
        {
            originalVertices[i] += Random.insideUnitSphere * offset;
        }
        
        Mesh mesh = new Mesh
        {
            vertices = originalVertices,
            triangles = triangles,
        };

        mesh.RecalculateNormals();

        return mesh;
    }
}