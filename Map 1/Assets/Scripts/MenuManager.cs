using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField nickname;

    public GameObject LoadingPanel;
    public void Enter()
    {
        if(nickname.text != "" && nickname.text.Length > 3)
        {
            PlayerPrefs.SetString("nickname", nickname.text);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Game");
            LoadingPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Comprobar el nickname");
        }
    }
    public void EnterMapa(string nameMapa)
    {
        if (nickname.text != "" && nickname.text.Length > 3)
        {
            PlayerPrefs.SetString("nickname", nickname.text);
            PlayerPrefs.Save();
            SceneManager.LoadScene(nameMapa);
        }
        else
        {
            Debug.Log("Comprobar el nickname");
        }
    }
}
