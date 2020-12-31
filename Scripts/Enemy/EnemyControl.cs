using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour, DamageViewer {
    [SerializeField] private GameObject m_explosionEffect;
    [SerializeField] private int hp = 300;
    [SerializeField] private GameObject m_dropItem;
    [SerializeField] private bool is_ItemDrop = false;

    public Transform m_playerTr;

    private PlayerTracking m_playerTracker;
    private AIScript m_aiScript;
    private DamageController m_dmgCtrl;

	// Use this for initialization
	void Start ()
    {
        m_playerTracker = GetComponent<PlayerTracking>();
        m_aiScript = GetComponent<AIScript>();
        m_dmgCtrl = GetComponent<DamageController>();

        if(m_dmgCtrl != null)
        {
            m_dmgCtrl.SetObject(this);
            m_dmgCtrl.hp = this.hp;
        }
        
        if(m_playerTr != null) { SetPlayerTransform(m_playerTr); }

        m_aiScript.StartCoroutine(m_aiScript.OnActive());
	}
	
	public void SetPlayerTransform(Transform tr)
    {
        this.m_playerTr = tr;
        m_playerTracker.SetPlayerTransform(tr);
        m_aiScript.SetPlayerTransform(tr);
    }

    public void DamageUpdate()
    {
        if (m_dmgCtrl.hp <= 0)
        {
            Instantiate(m_explosionEffect, GetComponent<Transform>().position, Quaternion.identity);

            if(is_ItemDrop)
            {
                switch(Random.Range(0, 3))
                {
                    case 0:
                        Instantiate(m_dropItem, GetComponent<Transform>().position, Quaternion.identity);
                        break;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
