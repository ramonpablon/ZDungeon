using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSoulder : MonoBehaviour {

    InputManager IM;
    InputManager_CPU CPU;
    public bool isBot = false;
    public GameObject shoulderPad, shoulderPlate;
    public GameObject shoulderPadDisable, shoulderPlateDisable;

    private bool isCreated = false;

    // Use this for initialization
    void Start() {
        if (!isBot) { 
        IM = GetComponent<InputManager>();
        } else {
        CPU = GetComponent<InputManager_CPU>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isBot) {
            if (IM.damage > 4 && !isCreated)
            {
                isCreated = true;
                shoulderPadDisable.SetActive(false);
                shoulderPlateDisable.SetActive(false);
                Instantiate(shoulderPad, transform.position + new Vector3(0, 1f, .2f), transform.rotation);
                Instantiate(shoulderPlate, transform.position + new Vector3(0, 1f, .2f), transform.rotation);
            }



        }

        if (isBot) {
            if (CPU.damage > 4 && !isCreated)
            {
                isCreated = true;
                shoulderPadDisable.SetActive(false);
                shoulderPlateDisable.SetActive(false);
                Instantiate(shoulderPad, transform.position + new Vector3(0, 1f, .2f), transform.rotation);
                Instantiate(shoulderPlate, transform.position + new Vector3(0, 1f, .2f), transform.rotation);
            }



        }

    }
}
