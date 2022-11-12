using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("AUDIO")]
    [SerializeField] private AudioMixer master;
    private AsyncOperation async;


    public void MasterVolume(float volume)
    {
        master.SetFloat("MasterVolume", volume);
    }

    public void FullScreenToggle(bool toggle)
    {
        Screen.fullScreen = toggle;
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void LoadLevel(int levelIndex)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(levelIndex);
            async.allowSceneActivation = true;
        }
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
