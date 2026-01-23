using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Swipe : MonoBehaviour
{
   [SerializeField]
   private bool isActive = true;
   [SerializeField]
   private float minswipeDistance = 50f;
   [SerializeField]
   private UnityEvent onSwipeUp;
   [SerializeField]
   private UnityEvent onSwipeDown;
   [SerializeField]
   private UnityEvent onSwipeLeft;
   [SerializeField]
   private UnityEvent onSwipeRigth;
   private Vector2 startPosition;
   private void Update() 
   {
        if (!isActive) return;

        if(Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 endposition = Input.mousePosition;
            Vector2 swipeVcetor = endposition - startPosition;
            if (swipeVcetor.magnitude >= minswipeDistance)
            {
                DetectSwiprDirection(swipeVcetor);
            }
        }
    }
    private void DetectSwiprDirection(Vector2 swipeVector)
    {
        float angle = Vector2.SignedAngle(Vector2.right, swipeVector);
        if (angle >= -45f && angle <= 45f)
        {
            onSwipeRigth?.Invoke();
        }
        else if (angle >= 135f && angle <= 135f)
        {
            onSwipeLeft?.Invoke();
        }
        else if (angle > -45f && angle < 135f)
        {
            onSwipeDown?.Invoke();
        }
    }
}
