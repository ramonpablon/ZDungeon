using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateParticles : MonoBehaviour {

    public ParticleSystem slash1, slash2, slash3, slash4;
    public ParticleSystem blockHit, softHit, hardHit;

    public void Slash1()
    {
        slash1.GetComponent<ParticleSystem>().Play(true);
    }

    public void Slash2()
    {
        slash2.GetComponent<ParticleSystem>().Play(true);
    }

    public void Slash3()
    {
        slash3.GetComponent<ParticleSystem>().Play(true);
    }

    public void Slash4()
    {
        slash4.GetComponent<ParticleSystem>().Play(true);
    }

    public void BlockHit()
    {
        blockHit.GetComponent<ParticleSystem>().Play(true);
    }
}
