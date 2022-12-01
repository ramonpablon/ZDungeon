using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShoulderPad : MonoBehaviour {

    private void OnEnable()
    {
        GetComponentInChildren<Renderer>().material.SetFloat("Vector1_93698D3F", 5);
        GetComponent<Rigidbody>().AddForce(new Vector3(0,1,0) * 8, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(-transform.right * 1000, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(transform.up * 1000, ForceMode.Impulse);
    }
}
