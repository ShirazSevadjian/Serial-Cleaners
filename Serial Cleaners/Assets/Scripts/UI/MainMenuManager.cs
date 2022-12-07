using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    [Header("PANELS")]
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Selectable[] defaultOptions;

    [Header("LEVELS")]
    [SerializeField] private GameObject[] levelLocks;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private TMPro.TMP_Text[] levelText;
    [SerializeField] private Slider[] levelScore;

    [Header("AUDIO")]
    [SerializeField] private AudioMixer master;

    private AsyncOperation async;
    private Dictionary<int, LevelInfo> levelProgression;

    private void Start()
    {
        PanelToggle(0);

        levelProgression = GlobalLevelManager.CheckCompletedLevels();
        CheckLevelProgression();
    }

    private void CheckLevelProgression()
    {
        foreach (int index in levelProgression.Keys)
        {
            if (levelProgression[index].completed)
            {
                levelLocks[index].SetActive(false);
                levelButtons[index].interactable = true;
                levelText[index].text = "Best Time" + levelProgression[index].bestTime.ToString() + "s";
                levelScore[index].SetValueWithoutNotify(levelProgression[index].stars);
            }
            else
            {
                levelLocks[index].SetActive(true);
                levelButtons[index].interactable = false;
                levelText[index].text = "NOT COMPLETED";
                levelScore[index].SetValueWithoutNotify(0);
            }
        }

        levelLocks[0].SetActive(false);
        levelButtons[0].interactable = true;
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

    #region Sound
    public void MasterVolume(float volume)
    {
        master.SetFloat("MasterVolume", volume);
    }

    public void MusicVolume(float volume)
    {
        master.SetFloat("MusicVolume", volume);
    }

    public void EffectsVolume(float volume)
    {
        master.SetFloat("EffectsVolume", volume);
    }
    #endregion

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
