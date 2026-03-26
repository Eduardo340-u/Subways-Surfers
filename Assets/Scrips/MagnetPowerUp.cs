using UnityEngine;
using System.Collections;

public class MagnetPowerUp : MonoBehaviour
{
    [SerializeField]
    private GameObject magnet;
    [SerializeField]
    private float duration = 5f;
    [SerializeField]
    private Collider magnetCollider;
    private Coroutine desactivateCoroutine; 
    public void Activate()
    {
        magnet.SetActive(true);
        magnetCollider.enabled = true;
        if(desactivateCoroutine !=null)
        {
            StopCoroutine(desactivateCoroutine);
        }
        desactivateCoroutine = StartCoroutine(DeactivateAfterDuration());
    }
    public void Desactivate()
    {
        if (desactivateCoroutine != null)
        {
            StopCoroutine(desactivateCoroutine);
            desactivateCoroutine = null;
        }
        magnet.SetActive(false);
        magnetCollider.enabled = false;
    }
    private IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        Desactivate();
    }
}
