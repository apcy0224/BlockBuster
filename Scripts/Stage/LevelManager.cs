using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager {
    private static LevelManager m_returnObject = null;
    
    public int m_stageLevel { get; private set; }
    public int m_enemyCount { get; private set; }

    public static LevelManager GetObject()
    {
        if(m_returnObject == null) { m_returnObject = new LevelManager(); }
        return m_returnObject;
    }

    private LevelManager()
    {
        m_returnObject = this;

        m_stageLevel = 1;
        m_enemyCount = 10;
    }

    public void LevelUp()
    {
        m_stageLevel++;
        m_enemyCount += 10;
    }
}
