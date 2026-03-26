using UnityEngine;

public class MagnetCollider : MonoBehaviour
{
    [SerializeField]
    private Transform character;
    private void OnTriggerEnter(Collider other)
    {
        if (other. CompareTag("Coin"))
        {
            CoinsFollow coinsFollow = other.GetComponent<CoinsFollow>();
            if (coinsFollow != null)
            {
                coinsFollow.StartFollowing(character);
            }
        }
    }
}
