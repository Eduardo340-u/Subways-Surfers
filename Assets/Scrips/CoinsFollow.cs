using UnityEngine;

public class CoinsFollow : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private float followSpeed = 5f;
    [SerializeField]
    private float mininumDistance = 0.05f;
    private bool canFollow = true;
    private Vector3 orinalPosition = Vector3.zero;
    private void Awake()
    {
        orinalPosition = transform.localPosition;
    }
    private void OnEnable()
    {
        canFollow = true;
        player = null;
        if (orinalPosition != Vector3.zero) transform.localPosition = orinalPosition;
    }
    public void StartFollowing(Transform playerTransform)
    {
        if (!canFollow) return;
        canFollow = false;
        player= playerTransform;
    }
    public void Update ()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < mininumDistance)
            {
                player = null;
            }
        }
    }
}
