using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleRandomArrangement : MonoBehaviour {
    [SerializeField] private Transform m_obstacleParent = null;
    [SerializeField] private GameObject[] m_obstacleObjects = null;
    [SerializeField] private int m_arrangementCount = 20;

    private GameObject m_currentInstantiateObject;
    private Vector3 m_xzPlaneVector;
    private float m_radius;

	// Use this for initialization
	void Start ()
    {
        if (GetComponent<StageBorder>() != null)
        {
            m_radius = GetComponent<StageBorder>().GetRadius();
        }
        else { m_radius = 100.0f; }

        if (m_obstacleParent != null && m_obstacleObjects != null)
        {
            for (int i = 0; i < m_arrangementCount; i++)
            {
                BoxRandomCreation();
            }
        }
	}

    private void BoxRandomCreation()
    {
        m_xzPlaneVector = Random.insideUnitSphere;
        m_xzPlaneVector.y = 0;

        m_currentInstantiateObject = Instantiate(m_obstacleObjects[Random.Range(0, m_obstacleObjects.Length)], m_xzPlaneVector * m_radius, Random.rotation, m_obstacleParent);
    }
}
