using UnityEngine;
using UnityEngine.UI;

public class CoinsUi : MonoBehaviour
{
    [SerializeField]
    private Text coinsText;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string animationName = "Wiggle";
    public void UpdateCoinsText(string coins)
    {
        animator.Play(animationName, 0, 0f);
        coinsText. text = coins;
    }
}
