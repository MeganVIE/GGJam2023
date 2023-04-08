using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    public void OnRestart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnBack()
    {
        SceneManager.LoadScene(0);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnCancel()
    {
        gameObject.SetActive(false);
    }
}