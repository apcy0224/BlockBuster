using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCubeAI : AIScript {
    [SerializeField] private GameObject m_projectile = null;
    [SerializeField] private float m_attackCoolDown = 5.0f;
    [SerializeField] private int damage = 100;

    private PlayerTracking m_playerTracker;
    private EnemyControl m_enemyController;

    void Start()
    {
        m_objectTr = GetComponent<Transform>();
        m_playerTracker = GetComponent<PlayerTracking>();
        m_enemyController = GetComponent<EnemyControl>();
    }

    public override IEnumerator OnActive()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_attackCoolDown);
            if (m_projectile != null)
            {
                if (!m_playerTracker.m_isBlockedWithObstacle && !m_playerTracker.m_isOutOfRange && m_playerTracker.m_isTracking)
                {
                    EnemyBeamControl beam = Instantiate(m_projectile, m_objectTr.position, m_objectTr.rotation).GetComponent<EnemyBeamControl>();
                    beam.SetDamage(damage);
                }
            }
        }
    }
}
