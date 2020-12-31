using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour, DamageViewer {

    [SerializeField] private float m_moveSpeed = 10.0f;
    [SerializeField] private float m_rotSpeed = 80.0f;

    private Transform m_playerTr;
    private Rigidbody m_playerRigidBody;
    private DamageController m_damageController;
    public int m_maxHp { get; private set; }

    private float h = 0.0f;
    private float v = 0.0f;
    private Vector3 m_moveDir;

	// Use this for initialization
	void Start ()
    {
        m_playerTr = GetComponent<Transform>();
        m_playerRigidBody = GetComponent<Rigidbody>();
        m_damageController = GetComponent<DamageController>();

        m_maxHp = 1000;

        if(m_damageController != null)
        {
            m_damageController.SetObject(this);
            m_damageController.hp = m_maxHp;
        }
	}

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        m_moveDir = (Vector3.forward * v);

        m_playerTr.Translate(m_moveDir * Time.deltaTime * m_moveSpeed, Space.Self);
        m_playerTr.Rotate(Vector3.up * Time.deltaTime * m_rotSpeed * h * ((v >= 0) ? 1 : -1));
    }

    void OnCollisionExit(Collision collision)
    {
        m_playerRigidBody.velocity = Vector3.zero;
    }

    public void DamageUpdate()
    {
        if(m_damageController.hp <= 0)
        {
            m_damageController.hp = 0;
        }
        else if(m_damageController.hp >= 1000)
        {
            m_damageController.hp = 1000;
        }
    }
}
