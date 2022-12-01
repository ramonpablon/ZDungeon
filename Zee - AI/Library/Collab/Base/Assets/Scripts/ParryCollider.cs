using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryCollider : MonoBehaviour {

    public GameObject player;
    [Space]
    public GameObject otherPlayer;


    private void OnEnable()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("AttackCollider"))
        {
            if (player.transform.position.x > otherPlayer.transform.position.x)
            {
                player.transform.rotation = Quaternion.Euler(0, 90, 0);
                player.transform.position = otherPlayer.transform.position - new Vector3(1.5f, 0, 0);
            }
            else if (player.transform.position.x < otherPlayer.transform.position.x)
            {
                player.transform.rotation = Quaternion.Euler(0, -90, 0);
                player.transform.position = otherPlayer.transform.position + new Vector3(1.5f, 0, 0);
            }
        }
    }
}
