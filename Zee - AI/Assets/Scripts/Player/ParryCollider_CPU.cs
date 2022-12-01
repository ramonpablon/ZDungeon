using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryCollider_CPU : MonoBehaviour {
    public bool ThisIsCPU;

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

            if (!ThisIsCPU)
            player.GetComponent<InputManager>().Unlock();
            else if (ThisIsCPU)
                player.GetComponent<InputManager_CPU>().Unlock();

        }
    }

    private void ResetAnimation()
    {
        if (!ThisIsCPU)
        {
            player.GetComponent<Animator>().Play("Idle"); // e se tiver no ar?
            if (player.GetComponent<PlayerPhisics>().blocking)
            {
                player.GetComponent<Animator>().SetTrigger("Block");
            }
        }
        else if (ThisIsCPU)
        {
            player.GetComponent<Animator>().Play("Idle"); // e se tiver no ar?
            player.GetComponent<InputManager_CPU>().Defend = false;

            if (player.GetComponent<PlayerPhisics_CPU>().blocking)
            {
                player.GetComponent<Animator>().SetTrigger("Block");
            }
        }
    }

    private void ResetRotation(float angle)
    {
        if (!ThisIsCPU)
        {
            player.GetComponent<PlayerPhisics>().angle = angle;
            player.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        else if (ThisIsCPU)
        {
            player.GetComponent<InputManager_CPU>().Defend = false;

            player.GetComponent<PlayerPhisics_CPU>().angle = angle;
            player.transform.rotation = Quaternion.Euler(0, angle, 0);



        }
    }

}
