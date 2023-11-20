using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isSplash;
    void Start()
    {
        if(isSplash)
        {
            StartCoroutine(GoToMenu());
        }
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }
    public void GoToAnyScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
