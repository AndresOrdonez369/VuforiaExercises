using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    void Start(){
    
    }
    void Update()
    {
    
    }
    public Transform particle;
    // Start is called before the first frame update
    void Start()
    {
        particle.GetComponent<ParticleSystem>().enableEmission = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        particle.GetComponent<ParticleSystem>().enableEmission = true;
        StartCoroutine(stopParticle());
    }

    IEnumerator stopParticle()
    {
        yield return new WaitForSeconds(.4f);
        particle.GetComponent<ParticleSystem>().enableEmission = false;
    }
}
