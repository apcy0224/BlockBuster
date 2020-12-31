using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnControl : MonoBehaviour {
    [SerializeField] private Transform m_playerTr;

    [SerializeField] private GameObject m_normalCubeGameObject = null;
    [SerializeField] private GameObject m_smallCubeGameObject = null;

    public int m_enemyCount { get; private set; }
    public bool m_isAllSpawned { get; private set; }
    private float m_spawnCoolDown = 3.0f;
    private float m_radius;
    private Vector3 m_xzPlaneVector;

	// Use this for initialization
	void Start ()
    {
        m_enemyCount = LevelManager.GetObject().m_enemyCount;

        if (GetComponent<StageBorder>() != null) { m_radius = GetComponent<StageBorder>().GetRadius(); }
        else { m_radius = 100.0f; }

        m_isAllSpawned = false;
        StartCoroutine(StartSpawn());
	}
	
    private IEnumerator StartSpawn()
    {
        for ( ; m_enemyCount >= 0; m_enemyCount--)
        {
            GameObject createdObject = m_normalCubeGameObject;
            m_xzPlaneVector = Random.insideUnitSphere;
            m_xzPlaneVector.y = 0;

            switch(Random.Range(0, 2))
            {
                case 0:
                    createdObject = m_normalCubeGameObject;
                    break;

                case 1:
                    createdObject = m_smallCubeGameObject;
                    break;
            }

            createdObject =
                Instantiate(createdObject, m_xzPlaneVector * m_radius, Quaternion.identity);
            createdObject.GetComponent<EnemyControl>().m_playerTr = m_playerTr;
            yield return new WaitForSeconds(m_spawnCoolDown);
        }

        m_isAllSpawned = true;
    }
}
