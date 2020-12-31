using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIScript : MonoBehaviour {
    protected Transform m_playerTr;
    protected Transform m_objectTr;

    public void SetPlayerTransform(Transform tr)
    {
        this.m_playerTr = tr;
    }

    public abstract IEnumerator OnActive();
}
