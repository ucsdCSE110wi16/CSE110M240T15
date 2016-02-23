using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIFunctions : MonoBehaviour
{

    public GameObject loadingImage;

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void setVolume(string source, float value)
    {

    }

    public void setMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("Music", value);
        PlayerPrefs.Save();
    }

    public void setSoundsVolume(float value)
    {
        PlayerPrefs.SetFloat("Sounds", value);
        PlayerPrefs.Save();
    }
}