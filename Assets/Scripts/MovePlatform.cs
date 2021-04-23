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

    private int mCurrentPoint = 0;

    private enum MovePlatformType
    {
        LeftRight,
        ForwardBack,
        LeftDiagonal,
        RightDiaogonal
    }
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
        if (mMovePlatformType == MovePlatformType.LeftRight)
        {
            DOTween.Sequence().Append(transform.DOMove(mMovePoints[0].position, mTimeToMove))
                              .Append(transform.DOMove(mMovePoints[1].position, mTimeToMove))
                              .SetLoops(-1, LoopType.Incremental);
        }
    }
    void Update()
    {
        if(mMovePlatformType == MovePlatformType.ForwardBack)
        {
            if (transform.position.z != mMovePoints[mCurrentPoint].position.z)
            {
                transform.position = Vector3.MoveTowards(transform.position, mMovePoints[mCurrentPoint].position, mTimeToMove * Time.deltaTime);
            }

            if (transform.position.z == mMovePoints[mCurrentPoint].position.z)
            {
                mCurrentPoint += 1;
            }

            if (mCurrentPoint >= mMovePoints.Count)
            {
                mCurrentPoint = 0;
            }
        }
    }
    private void OnDestroy()
    {
        DOTween.Kill(this);
    }
}
