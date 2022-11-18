using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("PANELS")]
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Selectable[] defaultOptions;


    [Header("AUDIO")]
    [SerializeField] private AudioMixer master;
    private AsyncOperation async;

    private void Start()
    {
        PanelToggle(0);
    }

    public void PanelToggle(int position)
    {
        Input.ResetInputAxes();

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);

            if (position == i)
                defaultOptions[i].Select();
        }
    }

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
