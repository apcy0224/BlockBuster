using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileControl : MonoBehaviour {
    [SerializeField] private GameObject m_expEffect = null;
    [SerializeField] private float speed = 1000.0f;

    private Transform m_transform;
    public Transform m_target = null;

    // Use this for initialization
    void Start ()
    {
        m_transform = GetComponent<Transform>();
        StartCoroutine(RangeOut(5.0f));
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(m_target != null)
        {
            m_transform.LookAt(m_target);
        }
        m_transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Obstacle")
        {
            Instantiate(m_expEffect, GetComponent<Transform>().position, Quaternion.identity);

            DamageController dmgctr = other.gameObject.GetComponent<DamageController>();
            if (dmgctr != null) { dmgctr.SetDamage(FireControl.GetObject().m_missileDamage); }

            Destroy(this.gameObject);
        }
    }

    public void SetTarget(Transform transform)
    {
        m_target = transform;
    }

    IEnumerator RangeOut(float countDown)
    {
        yield return new WaitForSeconds(countDown);
        if (this.gameObject != null)
        {
            Instantiate(m_expEffect, GetComponent<Transform>().position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
