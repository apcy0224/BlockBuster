using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBorder : MonoBehaviour {
    [SerializeField] private Transform m_playerTr;
    [SerializeField] private float m_radius = 100;

    private Vector3 m_stageCenter;

    void Start()
    {
        m_stageCenter = GetComponent<Transform>().position;    
    }

    void Update()
    {
        float distance = Vector3.Distance(m_playerTr.position, m_stageCenter);

        if(distance > m_radius)
        {
            Vector3 distance_Vector = m_playerTr.position - m_stageCenter;
            distance_Vector *= m_radius / distance;
            m_playerTr.position = m_stageCenter + distance_Vector;
        }
    }

    public float GetRadius()
    {
        return m_radius;
    }
}
