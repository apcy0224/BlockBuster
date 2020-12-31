using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurretController : MonoBehaviour, WeaponController {

    [SerializeField] private GameObject m_missileObject;

    public Transform m_targetTr;
    private Transform m_fireTr;
    private MissileControl m_missile;
    
    void Start ()
    {
        m_fireTr = GetComponent<Transform>();
	}

    public void Fire()
    {
        m_missile = Instantiate(
            m_missileObject,
            m_fireTr.position,
            m_fireTr.rotation
            ).GetComponent<MissileControl>();

        if(m_targetTr != null && m_missile != null)
        {
            m_missile.SetTarget(m_targetTr);
        }
    }
}
