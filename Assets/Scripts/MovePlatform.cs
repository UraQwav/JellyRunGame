using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private List<Transform> mMovePoints = new List<Transform>();

    [SerializeField] private float mTimeToMove = 3f;

    [SerializeField] private MovePlatformType mMovePlatformType = MovePlatformType.LeftRight;

    private Rigidbody mRigidbody;
    private Sequence mSequence;
    private int mCurrentPoint = 0;

    private enum MovePlatformType
    {
        LeftRight,
        ForwardBack,
        RotateByPoint,
        RotateByXAxis,
        RightDiaogonal,
        UpDown,
        UpDownVelocity
    }

    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        if (mMovePlatformType == MovePlatformType.LeftRight)
        {
            mSequence = DOTween.Sequence().Append(transform.DOMove(mMovePoints[0].position, mTimeToMove))
                                          .Append(transform.DOMove(mMovePoints[1].position, mTimeToMove))
                                          .SetLoops(-1, LoopType.Yoyo);
        }
    }
    void Update()
    {
        switch (mMovePlatformType)
        {
            case MovePlatformType.ForwardBack:
                if (transform.position.z != mMovePoints[mCurrentPoint].position.z)
                    transform.position = Vector3.MoveTowards(transform.position, mMovePoints[mCurrentPoint].position, mTimeToMove * Time.deltaTime);

                if (transform.position.z == mMovePoints[mCurrentPoint].position.z)
                    mCurrentPoint += 1;

                if (mCurrentPoint >= mMovePoints.Count)
                    mCurrentPoint = 0;
                break;
            case MovePlatformType.RightDiaogonal:
                if (transform.position.x != mMovePoints[mCurrentPoint].position.x)
                    transform.position = Vector3.Lerp(transform.position, mMovePoints[mCurrentPoint].position, mTimeToMove * Time.deltaTime);

                if (transform.position.x + 0.02f >= mMovePoints[mCurrentPoint].position.x &&
                    transform.position.x - 0.02f <= mMovePoints[mCurrentPoint].position.x)
                {
                    mCurrentPoint += 1;
                }

                if (mCurrentPoint >= mMovePoints.Count)
                    mCurrentPoint = 0;
                break;
            case MovePlatformType.RotateByPoint:
                transform.RotateAround(mMovePoints[0].position, Vector3.up, mTimeToMove * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (mMovePlatformType)
        {
            case MovePlatformType.UpDown:
                if (mRigidbody.position.y != mMovePoints[mCurrentPoint].position.y)
                    mRigidbody.position = Vector3.MoveTowards(mRigidbody.position, mMovePoints[mCurrentPoint].position, mTimeToMove);

                if (transform.position.y == mMovePoints[mCurrentPoint].position.y)
                    mCurrentPoint += 1;

                if (mCurrentPoint >= mMovePoints.Count)
                    mCurrentPoint = 0;
                break;
            case MovePlatformType.RotateByXAxis:
                mRigidbody.AddTorque(transform.right*mTimeToMove);
                break;
            case MovePlatformType.UpDownVelocity:
                if (mRigidbody.position.y != mMovePoints[mCurrentPoint].position.y)
                    mRigidbody.velocity = (mMovePoints[mCurrentPoint].position - mRigidbody.position) * mTimeToMove;

                if (mRigidbody.position.y + 0.02f >= mMovePoints[mCurrentPoint].position.y &&
                    mRigidbody.position.y - 0.02f <= mMovePoints[mCurrentPoint].position.y)
                    mCurrentPoint += 1;

                if (mCurrentPoint >= mMovePoints.Count)
                    mCurrentPoint = 0;
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        mSequence.Kill();
    }
}
