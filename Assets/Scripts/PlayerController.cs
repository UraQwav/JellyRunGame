using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Components")]
    [SerializeField] private Rigidbody mRigidbody = null;
    [SerializeField] private Vector3 mAddForwardDistance = Vector3.forward;
    [SerializeField] private float mSpeed = 1.5f;

    [Header("Efects Components")]
    [SerializeField] private GameObject mJellyPaintSprite = null;
    [SerializeField] private int mMaxJellyPaintSpriteCount = 5;
    [SerializeField] private ParticleSystem mJellyParticle = null;

    private List<GameObject> mJellyPaintSpriteList = new List<GameObject>();

    private Vector3 mTargetPosition;

    private bool mIsOnPlatform;
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
                if (mIsOnPlatform)
                {
                    if (mJellyPaintSpriteList.Count < mMaxJellyPaintSpriteCount)
                    {
                        var jellyPaint = Instantiate(mJellyPaintSprite, new Vector3(transform.position.x, mJellyPaintSprite.transform.position.y, transform.position.z), Quaternion.identity, transform.parent) as GameObject;
                        mJellyPaintSpriteList.Add(jellyPaint);
                    }
                    else
                    {
                        var distance = 0f;
                        var index = 0;
                        for (int i = 0; i < mJellyPaintSpriteList.Count; i++)
                        {
                            if (distance < Vector3.Distance(transform.position, mJellyPaintSpriteList[i].transform.position))
                            {
                                distance = Vector3.Distance(transform.position, mJellyPaintSpriteList[i].transform.position);
                                index = i;
                            }
                        }
                        mJellyPaintSpriteList[index].transform.position = new Vector3(transform.position.x, mJellyPaintSprite.transform.position.y, transform.position.z);
                        mJellyPaintSpriteList[index].transform.parent = transform.parent;
                    }
                    mJellyParticle.Play();
                }
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
                if (mIsOnPlatform)
                {
                    if (mJellyPaintSpriteList.Count < mMaxJellyPaintSpriteCount)
                    {
                        var jellyPaint = Instantiate(mJellyPaintSprite, new Vector3(transform.position.x, mJellyPaintSprite.transform.position.y, transform.position.z), Quaternion.identity, transform.parent) as GameObject;
                        mJellyPaintSpriteList.Add(jellyPaint);
                    }
                    else
                    {
                        var distance = 0f;
                        var index = 0;
                        for (int i = 0; i < mJellyPaintSpriteList.Count; i++)
                        {
                            if (distance < Vector3.Distance(transform.position, mJellyPaintSpriteList[i].transform.position))
                            {
                                distance = Vector3.Distance(transform.position, mJellyPaintSpriteList[i].transform.position);
                                index = i;
                            }
                        }
                        mJellyPaintSpriteList[index].transform.position = new Vector3(transform.position.x, mJellyPaintSprite.transform.position.y, transform.position.z);
                        mJellyPaintSpriteList[index].transform.parent = transform.parent;
                    }
                    mJellyParticle.Play();
                }
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
            transform.Translate(-Vector3.forward * mSpeed * Time.deltaTime, Space.World);
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
    }
    
    void OnTriggerEnter(Collider other)
    {
        mIsOnPlatform = true;
        if (other.CompareTag("MovePlatform"))
            transform.parent = other.transform.parent;
    }
    void OnTriggerExit(Collider other)
    {
        mIsOnPlatform = false;
        if (other.CompareTag("MovePlatform"))
            transform.parent = null;
    }
}
