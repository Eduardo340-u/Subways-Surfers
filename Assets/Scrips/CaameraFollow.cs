using UnityEngine;

public class CaameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float smoothSpeed = 0.125f;
    private Vector3 offset;
    private void Start()
    {
        Vector3 initialPosition = transform.position;
        offset = target.position - initialPosition;
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
