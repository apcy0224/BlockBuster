using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface DamageViewer
{
    void DamageUpdate();
}

public class DamageController : MonoBehaviour {
    public int hp { get; set; }

    private DamageViewer m_object = null;
    
    public void SetDamage(int damage)
    {
        hp -= damage;
        if (m_object != null) { m_object.DamageUpdate(); }
    }

    public void SetObject(DamageViewer gameObj)
    {
        this.m_object = gameObj;
    }
}
