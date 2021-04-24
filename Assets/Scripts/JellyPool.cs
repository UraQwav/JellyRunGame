using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyPool : MonoBehaviour
{
    [SerializeField] private GameObject mJellyPaintSprite = null;
    [SerializeField] private int mMaxJellyPaintSpriteCount = 5;

    private List<GameObject> mJellyPaintSpriteList = new List<GameObject>();

    public void GetPooledObject()
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
    }

}
