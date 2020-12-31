using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour {
    [SerializeField] private float m_rotSpeed = 60.0f;
    [SerializeField] private GameObject FXEmitter;
    [SerializeField] private AudioClip repairSound;

    private Transform m_objectTr;
    private FXEmitter m_fxEmitter;
    private DamageController m_playerDmgCtrl;

    void Start()
    {
        m_objectTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update ()
    {
        m_objectTr.Rotate(Vector3.up, m_rotSpeed * Time.deltaTime, Space.Self);
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            m_playerDmgCtrl = other.gameObject.GetComponent<DamageController>();
            if(m_playerDmgCtrl != null)
            {
                m_fxEmitter = Instantiate(FXEmitter, GetComponent<Transform>().position, Quaternion.identity)
                    .GetComponent<FXEmitter>();
                m_fxEmitter.SetAudioClip(repairSound);
                m_fxEmitter.Play();

                m_playerDmgCtrl.SetDamage(-100);
                Destroy(this.gameObject);
            }
        }
    }
}
