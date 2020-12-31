using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTracking : MonoBehaviour {
    [SerializeField] private float m_trackRangeLimit = 50.0f;
    [SerializeField] private float m_trackingSpeed = 10.0f;
    
    private Transform m_playerTr;
    private Transform m_objectTr;
    private float m_distance;

    public bool m_isTracking { get; private set; }
    public bool m_isBlockedWithObstacle { get; private set; }
    public bool m_isOutOfRange { get; private set; }
    private Vector3 m_objectScale;
    private RaycastHit m_hitInfo;

	// Use this for initialization
	void Start ()
    {
        m_objectTr = GetComponent<Transform>();

        if(m_objectTr != null)
        {
            m_objectScale = m_objectTr.lossyScale;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(m_playerTr != null)
        {
            m_isTracking = true;
            m_objectTr.LookAt(m_playerTr);

            m_distance = Vector3.Distance(m_objectTr.position, m_playerTr.position);
            if(m_distance > m_trackRangeLimit)
            {
                m_isOutOfRange = true;

                if(Physics.BoxCast(m_objectTr.position, m_objectScale / 2, m_objectTr.forward, out m_hitInfo, Quaternion.identity, 10.0f))
                {
                    if(m_hitInfo.collider.gameObject.tag == "Obstacle")
                    {
                        m_isBlockedWithObstacle = true;
                        AvoidObstacle();
                    }

                    else
                    {
                        m_isBlockedWithObstacle = false;
                        m_objectTr.Translate(Vector3.forward * m_trackingSpeed * Time.deltaTime, Space.Self);
                    }
                }

                else
                {
                    m_isBlockedWithObstacle = false;
                    m_objectTr.Translate(Vector3.forward * m_trackingSpeed * Time.deltaTime, Space.Self);
                }
            }
            else
            {
                m_isOutOfRange = false;
            }
        }
        else
        {
            m_isTracking = false;
        }
	}

    void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;   
    }

    public void SetPlayerTransform(Transform tr)
    {
        this.m_playerTr = tr;
    }

    private void AvoidObstacle()
    {
        float distance_x = Mathf.Abs(m_playerTr.position.x - m_objectTr.position.x);
        float distance_z = Mathf.Abs(m_playerTr.position.z - m_objectTr.position.z);
        float direction = m_trackingSpeed;

        if(distance_x > distance_z)
        {
            if(m_playerTr.position.x > m_objectTr.position.x)
            {
                if(m_playerTr.position.z > m_objectTr.position.z) { }
                else { direction *= -1; }
            }
            else
            {
                if (m_playerTr.position.z > m_objectTr.position.z) { direction *= -1; }
                else { }
            }
        }
        else
        {
            if (m_playerTr.position.z > m_objectTr.position.z)
            {
                if (m_playerTr.position.x > m_objectTr.position.x) { direction *= -1; }
                else { }
            }
            else
            {
                if (m_playerTr.position.x > m_objectTr.position.x) { }
                else { direction *= -1; }
            }
        }

        m_objectTr.RotateAround(m_hitInfo.transform.position, Vector3.up, direction * Time.deltaTime);
    }
}
