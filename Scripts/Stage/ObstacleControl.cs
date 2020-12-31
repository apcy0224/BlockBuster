using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControl : MonoBehaviour, DamageViewer {
    [SerializeField] private GameObject m_explosionEffect;
    [SerializeField] private int m_hp = 100;

    private DamageController m_dmgCtrl = null;

	// Use this for initialization
	void Start ()
    {
        m_dmgCtrl = GetComponent<DamageController>();
        if(m_dmgCtrl != null)
        {
            m_dmgCtrl.SetObject(this);
            m_dmgCtrl.hp = this.m_hp;
        }
	}
	
	public void DamageUpdate()
    {
        if(m_dmgCtrl.hp <= 0)
        {
            Instantiate(m_explosionEffect, GetComponent<Transform>().position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
