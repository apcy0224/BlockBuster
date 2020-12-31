using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamTurretController : MonoBehaviour, WeaponController
{
    [SerializeField] private GameObject m_beamObject;
    [SerializeField] private Transform m_turretGun;

    private Transform m_turretTr;
    
    void Start ()
    {
        m_turretTr = GetComponent<Transform>();
	}

    public void Fire()
    {
        Instantiate(
            m_beamObject,
            m_turretGun.position,
            Quaternion.Euler(
                m_turretGun.rotation.eulerAngles.x,
                m_turretGun.rotation.eulerAngles.y,
                m_turretGun.eulerAngles.z - 90
                ));
    }
}
