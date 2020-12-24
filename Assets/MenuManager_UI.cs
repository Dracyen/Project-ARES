using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager_UI : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoTo2DTrackBuilder()
    {
        SceneManager.LoadScene(1);
    }
    public void GoTo3DTrackBuilder()
    {
        SceneManager.LoadScene(2);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
