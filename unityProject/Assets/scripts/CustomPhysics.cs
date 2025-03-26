using System.Collections.Generic;
using UnityEngine;

public static class CustomPhysics
{
    public static List<Vector3> CalculateTrajectory(Vector3 startPos, Vector3 direction, float power, Vector3 gravity,
        float timeStep, int maxSteps, float bounceDamping, int maxBounces, LayerMask collisionLayers)
    {
        List<Vector3> trajectoryPoints = new List<Vector3>();
        Vector3 velocity = direction.normalized * power;
        Vector3 currentPosition = startPos;
        int bounceCount = 0;

        for (int i = 0; i < maxSteps; i++)
        {
            trajectoryPoints.Add(currentPosition);
            Vector3 nextPosition = currentPosition + velocity * timeStep;

            if (bounceCount == maxBounces)
                break;

            if (bounceCount < maxBounces && Physics.Raycast(currentPosition, velocity.normalized, out RaycastHit hit,
                    (nextPosition - currentPosition).magnitude, collisionLayers))
            {
                trajectoryPoints.Add(hit.point);
                velocity = Vector3.Reflect(velocity, hit.normal) * bounceDamping;
                currentPosition = hit.point + hit.normal * 0.01f;
                bounceCount++;
            }
            else
            {
                currentPosition = nextPosition;
                velocity += gravity * timeStep;
            }
        }

        return trajectoryPoints;
    }
}