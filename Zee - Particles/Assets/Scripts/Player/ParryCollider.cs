using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryCollider : MonoBehaviour {

    public GameObject player;
    [Space]
    public GameObject otherPlayer;

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("AttackCollider") && coll.gameObject.GetComponent<AttackCollider>().player != player)
        {
            if (player.transform.position.x > otherPlayer.transform.position.x)
            {
                ResetAnimation();
                ResetRotation(90);
                player.transform.position = otherPlayer.transform.position - new Vector3(1.5f, 0, 0);
            }
            else if (player.transform.position.x < otherPlayer.transform.position.x)
            {
                ResetAnimation();
                ResetRotation(-90);
                player.transform.position = otherPlayer.transform.position + new Vector3(1.5f, 0, 0);
            }

            player.GetComponent<InputManager>().Unlock();
        }
    }

    private void ResetAnimation()
    {
        player.GetComponent<Animator>().Play("Idle"); // e se tiver no ar?
        if (player.GetComponent<PlayerPhisics>().blocking)
            player.GetComponent<Animator>().SetTrigger("Block");
    }

    private void ResetRotation(float angle)
    {
        player.GetComponent<PlayerPhisics>().angle = angle;
        player.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

}
