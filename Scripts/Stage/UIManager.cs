using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject m_player;

    [SerializeField] private Image m_hpBar;
    [SerializeField] private Text m_gameOverText;

    [SerializeField] private GameObject m_stageClearUI;
    [SerializeField] private GameObject m_StageFailedUI;

    private PlayerControl m_playerControl;
    private DamageController m_playerHp;

	// Use this for initialization
	void Start ()
    {
        m_playerControl = m_player.GetComponent<PlayerControl>();
        m_playerHp = m_player.GetComponent<DamageController>();

        m_stageClearUI.SetActive(false);
        m_StageFailedUI.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        m_hpBar.fillAmount = (float)m_playerHp.hp / (float)m_playerControl.m_maxHp;
        
        if (GameManager.GetObject().m_isGameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            m_gameOverText.text = "You survived while Stage " + LevelManager.GetObject().m_stageLevel.ToString() + "!";
            m_StageFailedUI.SetActive(true);
        }

        if (GameManager.GetObject().m_isStageCleared)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            m_stageClearUI.SetActive(true);
        }
	}

    public void MoveToNextLevel()
    {
        LevelManager.GetObject().LevelUp();
        SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
        switch (Random.Range(0, 4))
        {
            case 1:
                SceneManager.LoadScene("Stage_1", LoadSceneMode.Single);
                break;

            case 2:
                SceneManager.LoadScene("Stage_2", LoadSceneMode.Single);
                break;

            case 3:
                SceneManager.LoadScene("Stage_3", LoadSceneMode.Single);
                break;
        }
    }
}
