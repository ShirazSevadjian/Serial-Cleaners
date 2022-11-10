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

    private bool bodyPickedUp = false;
    private bool mopPickedUp = false;
    private bool onePuddleCleaned = false;

    [SerializeField] private GameObject body;
    [SerializeField] private GameObject mop;

    void Start()
    {
        popUpIndex = 0;
        bloodCleaned = false;
        didPressE = false;
        UpdateTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        if (popUpIndex == 0)
        {
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Horizontal2") || Input.GetButtonDown("Vertical") || Input.GetButtonDown("Vertical2"))
            {
                popUpIndex++;
                UpdateTutorial();
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.E) || didPressE)
            {
                didPressE = !didPressE;
                if (player1.transform.Find("InteractablePosition/Bodybag") || player2.transform.Find("InteractablePosition/Bodybag"))
                {
                    popUpIndex++;
                    UpdateTutorial();
                }
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                popUpIndex++;
                UpdateTutorial();
            }
        }
        else if (popUpIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.E) || didPressE)
            {
                didPressE = !didPressE;
                if (player1.transform.Find("InteractablePosition/Mop") || player2.transform.Find("InteractablePosition/Mop"))
                {
                    popUpIndex++;
                    UpdateTutorial();
                }
            }
        }
        else if (popUpIndex == 4)
        {
            if (bloodCleaned)
            {
                popUpIndex++;
                UpdateTutorial();
            }
        }
    }


    private IEnumerator TutorialSequence()
    {
        Time.timeScale = 0;
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));

        Time.timeScale = 1;
        popUpIndex++;
        UpdateTutorial();

        yield return new WaitUntil(() => Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Horizontal2") || Input.GetButtonDown("Vertical") || Input.GetButtonDown("Vertical2"));

        popUpIndex++;
        UpdateTutorial();

        yield return new WaitForSeconds(5.0f);

        popUpIndex++;
        UpdateTutorial();

        yield return new WaitUntil(() => bodyPickedUp);

        popUpIndex++;
        UpdateTutorial();

        yield return new WaitUntil(() => body == null);

        popUpIndex++;
        UpdateTutorial();

        yield return new WaitUntil(() => mopPickedUp);

        mop.GetComponent<TutorialAction>().enabled = true;
        popUpIndex++;
        UpdateTutorial();

        yield return new WaitUntil(() => onePuddleCleaned);
        
        popUpIndex++;
        UpdateTutorial();

        yield return new WaitForSeconds(5.0f);

        popUpIndex++;
        UpdateTutorial();
    }

    public void InteractedWithBody()
    {
        bodyPickedUp = true;
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
