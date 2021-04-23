using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlatform : MonoBehaviour
{
    [SerializeField] private ParticleSystem mParticles = null;

    private float mTimeToShow = 2f;

    void OnTriggerEnter(Collider other)
    {
        mParticles.Play();
        StartCoroutine(ShowEndCard());
    }
    private IEnumerator ShowEndCard() {
        yield return new WaitForSeconds(mTimeToShow);
        GameManager.Instance.ShowEndCard();
    }
}
