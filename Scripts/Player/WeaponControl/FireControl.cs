using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WeaponController
{
    void Fire();
}

public class FireControl : MonoBehaviour
{
    // Singleton
    static private FireControl m_returnObject;
    static public FireControl GetObject()
    {
        if(m_returnObject == null) { m_returnObject = new FireControl(); }
        return m_returnObject;
    }

    // Aiming
    private RaycastHit m_hitInfo;

    // Weapon Controller
    public enum WeaponType { BEAM, MISSILE, DRONE };
    private WeaponType m_currentWeapon;

    [SerializeField] private BeamTurretController[] m_beamTurrets;
    [SerializeField] private MissileTurretController[] m_missileTurrets;

    // Weapon Stat Variables
    public int m_beamDamage = 100;
    private bool m_beamCoolDown = false;
    public float m_beamCoolTime = 1.0f;

    public int m_missileDamage = 50;
    private bool m_missileCoolDown = false;
    public float m_missileCoolTime = 3.0f;

    private FireControl()
    {
        if (m_returnObject == null) { m_returnObject = this; }
    }
    
	void Start ()
    {
        m_currentWeapon = WeaponType.BEAM;
	}
	
	void Update ()
    {
	    if(Input.GetMouseButtonDown(0))
        {
            switch (m_currentWeapon)
            {
                case WeaponType.BEAM:
                    if(m_beamTurrets == null || m_beamCoolDown) { break; }

                    for (int i = 0; i < m_beamTurrets.Length; i++)
                    {
                        m_beamTurrets[i].Fire();
                    }

                    m_beamCoolDown = true;
                    StartCoroutine(CoolDown(WeaponType.BEAM));
                    break;

                case WeaponType.MISSILE:
                    if(m_missileTurrets == null || m_missileCoolDown) { break; }
                    
                    if(Physics.BoxCast(Camera.main.transform.position, Vector3.one * 10, Camera.main.transform.forward, out m_hitInfo, Quaternion.identity, Mathf.Infinity, (1 << LayerMask.NameToLayer("Enemy"))))
                    {
                        if (m_hitInfo.collider.gameObject.tag == "Enemy")
                        {
                            for (int i = 0; i < m_missileTurrets.Length; i++)
                            {
                                m_missileTurrets[i].m_targetTr = m_hitInfo.transform;
                            }
                        }
                    }

                    for (int i = 0; i < m_missileTurrets.Length; i++)
                    {
                        m_missileTurrets[i].Fire();
                        m_missileTurrets[i].m_targetTr = null;
                    }

                    m_missileCoolDown = true;
                    StartCoroutine(CoolDown(WeaponType.MISSILE));
                    break;
            }
        }
        
        for(KeyCode keyCode = KeyCode.Alpha1; keyCode < KeyCode.Alpha3; keyCode++)
        {
            if(Input.GetKeyDown(keyCode))
            {
                switch(keyCode)
                {
                    case KeyCode.Alpha1:
                        m_currentWeapon = WeaponType.BEAM;
                        break;

                    case KeyCode.Alpha2:
                        m_currentWeapon = WeaponType.MISSILE;
                        break;
                }
            }
        }
	}

    IEnumerator CoolDown(WeaponType weaponType)
    {
        switch(weaponType)
        {
            case WeaponType.BEAM:
                yield return new WaitForSeconds(m_beamCoolTime);
                m_beamCoolDown = false;
                break;

            case WeaponType.MISSILE:
                yield return new WaitForSeconds(m_missileCoolTime);
                m_missileCoolDown = false;
                break;
        }
    }
}
