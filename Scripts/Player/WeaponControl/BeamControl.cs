using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamControl : MonoBehaviour {
    [SerializeField] private GameObject m_expEffect = null;
    [SerializeField] private float speed = 1000.0f;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * speed);
        StartCoroutine(RangeOut(3.0f));
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Obstacle")
        {
            Instantiate(m_expEffect, GetComponent<Transform>().position, Quaternion.identity);

            DamageController dmgctr = other.gameObject.GetComponent<DamageController>();
            if(dmgctr != null) { dmgctr.SetDamage(FireControl.GetObject().m_beamDamage); }

            Destroy(this.gameObject);
        }
    }

    IEnumerator RangeOut(float countDown)
    {
        yield return new WaitForSeconds(countDown);
        if (this.gameObject != null) { Destroy(this.gameObject); }
    }
}
