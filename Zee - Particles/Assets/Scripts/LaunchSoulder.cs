using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSoulder : MonoBehaviour {

    InputManager IM;

    public GameObject shoulderPad, shoulderPlate;
    public GameObject shoulderPadDisable, shoulderPlateDisable;

    private bool isCreated = false;

	// Use this for initialization
	void Start () {
        IM = GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (IM.damage > 4 && !isCreated)
        {
            isCreated = true;
            shoulderPadDisable.SetActive(false);
            shoulderPlateDisable.SetActive(false);
            Instantiate(shoulderPad, transform.position + new Vector3(0, 1f, .2f), transform.rotation);
            Instantiate(shoulderPlate, transform.position + new Vector3(0, 1f, .2f), transform.rotation);
        }
	}
}
