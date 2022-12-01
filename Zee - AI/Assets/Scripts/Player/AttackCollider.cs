using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PushDirection
{
    None = 0,
    Forward = 1,
    Up = 2,
    Down = 3,
    HitStop = 4
}


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class AttackCollider : MonoBehaviour {

    [Header("Avoid collision with:")]
    [Space(3)]
    public GameObject player;

    [Space(3)]
    public float damage;
    [Space]
    [Header("Is a Block Break?")]
    [Space(3)]
    public bool blockBreak = false;

    [Space]
    [Header("Have a Ultimate Impulse Force?")]
    [Space(3)]
    public PushDirection pushDirection = PushDirection.None;
    [Space(3)]
    public float pushForce;
    [Space(3)]
    public float stunTime;

    [Space]
    [Header("timer entre as pausas do Hitstop")]
    public float timeBetweenHits;


    private void OnEnable()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<InputManager>().getHit = true;
        }

        if (other.gameObject.tag.Equals("CPU"))
        {
            other.gameObject.GetComponent<InputManager_CPU>().getHit = true;
        }

    }
}

