using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public void OnStartButtonClicked()
    {
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

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
