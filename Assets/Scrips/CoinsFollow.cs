using UnityEngine;

public class CoinsFollow : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private float followSpeed = 5f;
    [SerializeField]
    private float mininumDistance = 0.05f;
    private bool isFollowing = false;
    private Vector3 orinalPosition;
    public void StartFollowing(Transform playerTransform)
    {
        orinalPosition = transform.localPosition;
        player= playerTransform;
        isFollowing = true;
    }
    public void Update ()
    {
        if (isFollowing && player != null)
        {
            Vector3 targetPosition = player.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < mininumDistance)
            {
                player = null;
                isFollowing = false;
                transform.localPosition = orinalPosition;
            }
        }
    }
}
