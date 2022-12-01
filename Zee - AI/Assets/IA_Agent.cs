using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Agent : MonoBehaviour {
  public  GameObject Target;
    Rigidbody TargetRB;
    bool IsOnAir;
    float Distance;
    bool isNear;
    bool isFar;
	// Use this for initialization
	void Start () {
        TargetRB = Target.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //CheckDistance();

        var relativePoint = transform.InverseTransformPoint(Target.transform.position);
        if (relativePoint.x < 0.0)
            print("Target <-");
        else if (relativePoint.x > 0.0)
            print("Target ->");
        else {
            print("Target --");


        }

    }


    void CheckDistance() {
        Debug.Log(TargetRB.velocity);
        if (TargetRB.velocity.y >= 0.1f || TargetRB.velocity.y <= -0.01)
        {
            IsOnAir = true;

        }
        else {

            IsOnAir = false;
        }

        Distance = Vector3.Distance(this.transform.position, Target.transform.position);
        if (Distance > 4f)
        {
            isFar = true;
            isNear = false;
        }
        else if (Distance <= 4f)
        {
            isNear = true;
            isFar = false;

        }

    }



}
