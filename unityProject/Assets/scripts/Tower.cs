using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float power = 10f;
    public float timeStep = 0.01f;
    public float bounceDamping = 0.8f;
    public float sphereCastRadius = 0.2f;

    public int maxSteps = 100;
    public int maxBounces = 2;

    [Header("Bullet")] 
    public LayerMask collisionLayers;
    public Bullet bulletPrefab;
    public float bulletSpeed = 10f;
    public float offsetCube = 0.1f;


    private readonly Vector3 _gravity = new(0, -9.81f, 0);
    private List<Vector3> _trajectory;


    private void Update()
    {
        _trajectory = CustomPhysics.CalculateTrajectory(transform.position, transform.forward, power,
            _gravity, timeStep, maxSteps, bounceDamping, maxBounces, collisionLayers);

        lineRenderer.positionCount = _trajectory.Count;
        lineRenderer.SetPositions(_trajectory.ToArray());

        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }
    }


    private void SpawnBullet()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.StartMove(_trajectory, bulletSpeed);
    }
}