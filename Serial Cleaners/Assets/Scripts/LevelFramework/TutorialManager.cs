using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    public GameObject player1;
    public GameObject player2;

    private int popUpIndex;
    bool bloodCleaned;

    bool didPressE;

    private bool playerMove = false;
    private bool playerSubmit = false;
    private bool bodyPickedUp = false;
    private bool bodyDestroyed = false;
    private bool mopPickedUp = false;
    private bool onePuddleCleaned = false;

    [SerializeField] private GameObject mop;

    void Start()
    {
        popUpIndex = 0;
        bloodCleaned = false;
        didPressE = false;
        UpdateTutorial();
    }

    private IEnumerator TutorialSequence()
    {
        Time.timeScale = 0;
        yield return new WaitUntil(() => playerSubmit);

        Time.timeScale = 1;
        popUpIndex++;
        UpdateTutorial();

        yield return new WaitUntil(() => playerMove);

        popUpIndex++;
        UpdateTutorial();

        yield return new WaitForSeconds(5.0f);

        popUpIndex++;
        UpdateTutorial();

        yield return new WaitUntil(() => bodyPickedUp);

        popUpIndex++;
        UpdateTutorial();

        yield return new WaitUntil(() => bodyDestroyed);

        popUpIndex++;
        UpdateTutorial();
        mop.GetComponent<TutorialAction>().enabled = true;

        yield return new WaitUntil(() => mopPickedUp);

        //popUpIndex++;
        //UpdateTutorial();

        yield return new WaitUntil(() => onePuddleCleaned);

        popUpIndex++;
        UpdateTutorial();
        LevelManager.Instance.StartLevel();

        yield return new WaitForSeconds(5.0f);

        popUpIndex++;
        UpdateTutorial();
    }

    public void PlayerSubmit()
    {
        playerSubmit = true;
    }

    public void PlayerMove()
    {
        playerMove = true;
    }

    public void InteractedWithBody()
    {
        bodyPickedUp = true;
    }

    public void BodyDestroyed()
    {
        bodyDestroyed = true;
    }

    public void InteractedWithMop()
    {
        mopPickedUp = true;
    }

    public void PuddleCleaned()
    {
        onePuddleCleaned = true;
    }

    void UpdateTutorial()
    {
        for(int i = 0; i < popUps.Length; ++i)
        {
            popUps[i].SetActive(i == popUpIndex);
        }
    }

    public void onBloodCleaned()
    {
        bloodCleaned = true;
    }
}
