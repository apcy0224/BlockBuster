using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeamControl : MonoBehaviour {
    [SerializeField] private GameObject m_expEffect = null;
    [SerializeField] private float speed = 1000.0f;

    private int damage = 0;
    
	void Start ()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        StartCoroutine(RangeOut(3.0f));
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Obstacle")
        {
            Instantiate(m_expEffect, GetComponent<Transform>().position, Quaternion.identity);

            DamageController dmgctr = other.gameObject.GetComponent<DamageController>();
            if(dmgctr != null) { dmgctr.SetDamage(damage); }

            Destroy(this.gameObject);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    IEnumerator RangeOut(float countDown)
    {
        yield return new WaitForSeconds(countDown);
        if (this.gameObject != null) { Destroy(this.gameObject); }
    }
}
