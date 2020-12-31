using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructionParticle : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(SelfDestruct());
	}

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(this.gameObject.GetComponent<ParticleSystem>().main.duration);
        Destroy(this.gameObject);
    }
}
