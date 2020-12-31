using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCubeAI : AIScript {
    [SerializeField] private GameObject m_expEffect = null;
    [SerializeField] private int damage = 100;

    private PlayerTracking m_playerTracker;
    private EnemyControl m_enemyController;

    void Start()
    {
        m_objectTr = GetComponent<Transform>();
        m_playerTracker = GetComponent<PlayerTracking>();
        m_enemyController = GetComponent<EnemyControl>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(m_expEffect, GetComponent<Transform>().position, Quaternion.identity);

            DamageController dmgctr = collision.gameObject.GetComponent<DamageController>();
            if (dmgctr != null) { dmgctr.SetDamage(damage); }

            Destroy(this.gameObject);
        }
    }

    public override IEnumerator OnActive()
    {
        yield return null;
    }
}
