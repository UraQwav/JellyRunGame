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
        RightDiaogonal
    }
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        if (mMovePlatformType == MovePlatformType.LeftRight)
        {
            mSequence = DOTween.Sequence();
            mSequence.Append(transform.DOMove(mMovePoints[0].position, mTimeToMove))
                              .Append(transform.DOMove(mMovePoints[1].position, mTimeToMove))
                              .SetLoops(-1, LoopType.Yoyo);
        }
    }
    void Update()
    {
        if(mMovePlatformType == MovePlatformType.ForwardBack)
        {
            if (transform.position.z != mMovePoints[mCurrentPoint].position.z)
                transform.position = Vector3.MoveTowards(transform.position, mMovePoints[mCurrentPoint].position, mTimeToMove * Time.deltaTime);

            if (transform.position.z == mMovePoints[mCurrentPoint].position.z)
                mCurrentPoint += 1;

            if (mCurrentPoint >= mMovePoints.Count)
                mCurrentPoint = 0;
        }
        if (mMovePlatformType == MovePlatformType.RightDiaogonal)
        {
            if (transform.position.x != mMovePoints[mCurrentPoint].position.x)
                transform.position = Vector3.Lerp(transform.position, mMovePoints[mCurrentPoint].position, mTimeToMove * Time.deltaTime);

            if (transform.position.x + 0.02f >= mMovePoints[mCurrentPoint].position.x &&
                transform.position.x - 0.02f <= mMovePoints[mCurrentPoint].position.x)
            {
                mCurrentPoint += 1;
            }

            if (mCurrentPoint >= mMovePoints.Count)
                mCurrentPoint = 0;
        }
        if(mMovePlatformType == MovePlatformType.RotateByPoint)
        {
            transform.RotateAround(mMovePoints[0].position, Vector3.up, mTimeToMove * Time.deltaTime);
        }
    }
    private void OnDestroy()
    {
        mSequence.Kill();
    }
}
