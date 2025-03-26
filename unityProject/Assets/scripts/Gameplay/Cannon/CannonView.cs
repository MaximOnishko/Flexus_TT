using UnityEngine;

public class CannonView : MonoBehaviour
{
    [field: SerializeField] public TrajectoryView TrajectoryView { get; private set; }
    [field: SerializeField] public CannonAnimator Animator { get; private set; }
    [field: SerializeField] public Transform SpawnBulletPos { get; private set; }

    
    [Header("Rotation")]
    [SerializeField] private float minXRotation = -45f;
    [SerializeField] private float maxXRotation = 45f;
    [SerializeField] private float minYRotation = -60f;
    [SerializeField] private float maxYRotation = 60f;
    
    [SerializeField] private Transform rotatablePart;
    [SerializeField] private float rotateSpeed = 50f;
    
    private float currentXRotation;
    private float currentYRotation;
    
    public void RotateTo(Vector3 dir)
    {
        currentYRotation += dir.y * rotateSpeed * Time.deltaTime;
        currentYRotation = Mathf.Clamp(currentYRotation, minYRotation, maxYRotation);
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        
        currentXRotation -= dir.x * rotateSpeed * Time.deltaTime;
        currentXRotation = Mathf.Clamp(currentXRotation, minXRotation, maxXRotation);
        rotatablePart.localRotation = Quaternion.Euler(currentXRotation, 0, 0);
    }
}