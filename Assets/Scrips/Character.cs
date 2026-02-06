using UnityEngine;
using DG. Tweening;
using System.Collections;

public class Character : MonoBehaviour
{
    private Rigidbody characterRigidbody;
    [SerializeField]
    private Data characterData;
    [SerializeField]
    private Animator characterAnimator;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float distanceToMove = 2f;
    [SerializeField]
    private float moveDuration = 0.2f;
    [SerializeField]
    private Transform ChararacterStartPivot;
    private bool isGrounded= true;
    private bool isMoving = false;
    private bool isRolling = false;
    private bool isActive = false;
    private void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody>();
    }
    public void StartGame()
    {
        isRolling = false;
        isMoving = false;
        isActive = false;
        characterAnimator.Play(characterData.runAnimationName, 0, 0f);
        transform.position= ChararacterStartPivot.position;
    }
    private void Start()
    {
        isActive = true;
        characterAnimator.Play(characterData.runAnimationName, 0, 0f);
        characterRigidbody = GetComponent<Rigidbody>();
    }
    public void Lose()
    {
        isActive = false;
        StopAllCoroutines();
        characterAnimator.Play(characterData.loseAnimationName, 0, 0f);
    }
    public void Jump()
    {
        if (!isActive) return;
        if (isGrounded)
        {
            characterAnimator.Play(characterData.jumpAnimationName, 0, 0f);
            characterRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    public void MoveDown()
    {
        if (!isActive || isRolling) return;
        if (!isGrounded)
        {
            characterRigidbody.AddForce(Vector3.down * jumpForce * 2, ForceMode.Impulse);
        }
        characterAnimator.Play(characterData.rollAnimationName, 0, 0f);
        isRolling = true;
        StartCoroutine(ResetRoll());
    }
    public void MoveLeft()
    {
        if (transform.position.x <= -distanceToMove) return;
        Move(Vector3.left);
    }
    public void MoverRight()
    {
        if (transform.position.x >= distanceToMove) return;
        Move(Vector3.right);
    }
    private void Move(Vector3 direction)
    {
        if (isMoving || !isActive) return;
        characterAnimator.Play(characterData.moveAnimationName, 0, 0f);
        isMoving= true;
        Vector3 targetPosition = transform.position + direction * distanceToMove;

        transform.DOMove(targetPosition, moveDuration).SetEase(Ease.OutQuad).OnComplete(()=>
        {
           isMoving = false; 
        });
    }
    private IEnumerator ResetRoll()
    {
        yield return new WaitForSeconds(characterAnimator.GetCurrentAnimatorStateInfo(0).length);
        isRolling = false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (isActive && collision.gameObject.CompareTag("Ground"))
        {
            if (!isRolling)
            {
                characterAnimator.Play(characterData.runAnimationName, 0, 0f);
            }
            isGrounded = true;
        }
    }
}
