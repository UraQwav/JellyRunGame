using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPlatform : MonoBehaviour
{
    [SerializeField] private float mSpeed = 30f;
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(Vector3.forward * mSpeed, ForceMode.Impulse);
    }
}
