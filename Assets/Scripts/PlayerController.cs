using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody mRigidbody = null;

    [SerializeField] private float mSpeed = 1.5f;

    [SerializeField] private Vector3 mAddForwardDistance = Vector3.forward;
    
    private Vector3 mTargetPosition;

    private bool mIsMoveDown;
    private bool mIsMoveLeft;
    private bool mIsMoveRight;
    private bool mIsMoveUp;
    private enum MoveDirection
    {
        MoveDown,
        MoveLeft,
        MoveRight,
        MoveUp,
        Stay
    }

    private MoveDirection moveDirection = MoveDirection.Stay;
    
    void Start()
    {
        SwipeHandler.OnSwipeDown += SwipeHandler_OnSwipeDown;
        SwipeHandler.OnSwipeLeft += SwipeHandler_OnSwipeLeft;
        SwipeHandler.OnSwipeRight += SwipeHandler_OnSwipeRight;
        SwipeHandler.OnSwipeUp += SwipeHandler_OnSwipeUp;
    }

    private void SwipeHandler_OnSwipeUp()
    {
        moveDirection = MoveDirection.MoveUp;
        mTargetPosition = transform.position + mAddForwardDistance;
    }

    private void SwipeHandler_OnSwipeRight()
    {
        if (moveDirection == MoveDirection.Stay)
        {
            moveDirection = MoveDirection.MoveRight;
            transform.DOJump(2f*Vector3.forward + Vector3.right + transform.position, mSpeed, 1, 1f).OnComplete(()=> {
                moveDirection = MoveDirection.Stay;
            });
            transform.DORotate(new Vector3(180f, 180f, 180f),1f);
        }
    }

    private void SwipeHandler_OnSwipeLeft()
    {
        if (moveDirection == MoveDirection.Stay)
        {
            moveDirection = MoveDirection.MoveLeft;
            transform.DOJump(2f*Vector3.forward + Vector3.left + transform.position, mSpeed, 1, 1f).OnComplete(() => {
                moveDirection = MoveDirection.Stay;
            });
            transform.DORotate(new Vector3(180f, 180f, 180f), 1f);
        }
    }

    private void SwipeHandler_OnSwipeDown()
    {
        moveDirection = MoveDirection.MoveDown;
        mTargetPosition = transform.position - mAddForwardDistance;
    }

    void Update()
    {
        if (moveDirection == MoveDirection.MoveDown)
        {
            transform.Translate(Vector3.forward * -mSpeed * Time.deltaTime);
            if (transform.position.z < mTargetPosition.z)
            {
                moveDirection = MoveDirection.Stay;
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (moveDirection == MoveDirection.MoveUp)
        {
            mRigidbody.AddForce(Vector3.forward * mSpeed * 30f);
            if (transform.position.z > mTargetPosition.z)
            {
                moveDirection = MoveDirection.Stay;
            }
        }
    }
   
    private void OnDestroy() {
        SwipeHandler.OnSwipeDown -= SwipeHandler_OnSwipeDown;
        SwipeHandler.OnSwipeLeft -= SwipeHandler_OnSwipeLeft;
        SwipeHandler.OnSwipeRight -= SwipeHandler_OnSwipeRight;
        SwipeHandler.OnSwipeUp -= SwipeHandler_OnSwipeUp;
        print("drop");
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovePlatform"))
            transform.parent = other.transform.parent;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovePlatform"))
            transform.parent = null;
    }
}
