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
    public void GoToMainPlayMenu()
    {
        SceneManager.LoadScene(3);
    }
    public void GoToGame()
    {
        SceneManager.LoadScene(4);
    }
    public void GoToBuilders()
    {
        SceneManager.LoadScene(5);
    }
    public void GoToBuilder2D()
    {
        SceneManager.LoadScene(6);
    }
    public void GoToBuilder3D()
    {
        SceneManager.LoadScene(7);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
