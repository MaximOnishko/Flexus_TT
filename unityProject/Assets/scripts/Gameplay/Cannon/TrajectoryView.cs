using Infrastructure.Services.CustomPhysics;
using UnityEngine;

public class TrajectoryView : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    public void UpdateTrajectory(TrajectoryData trajectory)
    {
        lineRenderer.positionCount = trajectory.FirstHitIndex;
        lineRenderer.SetPositions(trajectory.Points);
    }
}