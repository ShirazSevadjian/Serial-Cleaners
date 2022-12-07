using System.Collections.Generic;
using UnityEngine;


public class GlobalLevelManager : MonoBehaviour
{
    [SerializeField] private int numberOfLevels = 4;

    private static Dictionary<int, LevelInfo> levelProgression;

    public static GlobalLevelManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            levelProgression = new Dictionary<int, LevelInfo>();

            levelProgression.Add(0, new LevelInfo());
            for (int i = 1; i < numberOfLevels + 1; i++)
            {
                levelProgression.Add(i, new LevelInfo(i, false));
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void CompletedLevel(int index, float timeRemaining, int stars)
    {
        if (levelProgression.ContainsKey(index))
        {
            levelProgression[index].completed = true;
            levelProgression[index].bestTime = timeRemaining;
            levelProgression[index].stars = stars;

        }
    }

    public static Dictionary<int, LevelInfo> CheckCompletedLevels()
    {
        return levelProgression;
    }
}
