using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager m_returnObject;

    [SerializeField] private GameObject m_player;
    private PlayerControl m_playerControl;
    private DamageController m_playerHp;

    public bool m_isStageCleared { get; private set; }
    public bool m_isGameOver { get; private set; }

    private EnemySpawnControl m_enemySpawnControl = null;

    public static GameManager GetObject()
    {
        if(m_returnObject == null) { m_returnObject = new GameManager(); }
        return m_returnObject;
    }

    void Start()
    {
        m_returnObject = this;

        m_playerControl = m_player.GetComponent<PlayerControl>();
        m_playerHp = m_player.GetComponent<DamageController>();

        m_enemySpawnControl = GetComponent<EnemySpawnControl>();
        m_isStageCleared = false;
        m_isGameOver = false;
    }

    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Enemy") == null && m_enemySpawnControl.m_enemyCount <= 0 && !m_isGameOver)
        {
            m_isStageCleared = true;
        }

        if(!m_isStageCleared && m_playerHp.hp <= 0)
        {
            m_isGameOver = true;
        }
    }
}
