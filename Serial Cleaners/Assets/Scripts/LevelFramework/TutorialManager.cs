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

    void Start()
    {
        popUpIndex = 0;
        bloodCleaned = false;
        didPressE = false;
        UpdateTutorial();

        StartCoroutine(TutorialSequence());
    }

    // Update is called once per frame
    void Update()
    {
        //if (popUpIndex == 0)
        //{
        //    Time.timeScale = 0;
        //    if (Input.GetButtonDown("Submit"))
        //    {
        //        popUpIndex++;
        //        UpdateTutorial();
        //    }
        //}
        //else if (popUpIndex == 1)
        //{
        //    Time.timeScale = 1;

        //    if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Horizontal2") || Input.GetButtonDown("Vertical") || Input.GetButtonDown("Vertical2"))
        //    {
        //        popUpIndex++;
        //        UpdateTutorial();
        //    }

        //}
        //else if (popUpIndex == 2)
        //{
        //    if (Input.GetKeyDown(KeyCode.E) || didPressE)
        //    {
        //        didPressE = !didPressE;
        //        if (player1.transform.Find("InteractablePosition/Bodybag") || player2.transform.Find("InteractablePosition/Bodybag"))
        //        {
        //            popUpIndex++;
        //            UpdateTutorial();
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.X))
        //    {
        //        popUpIndex++;
        //        UpdateTutorial();
        //    }
        //}
        //else if (popUpIndex == 3)
        //{
        //    if (Input.GetKeyDown(KeyCode.E) || didPressE)
        //    {
        //        didPressE = !didPressE;
        //        if (player1.transform.Find("InteractablePosition/Mop") || player2.transform.Find("InteractablePosition/Mop"))
        //        {
        //            popUpIndex++;
        //            UpdateTutorial();
        //        }
        //    }
        //}
        //else if (popUpIndex == 4)
        //{
        //    if (bloodCleaned)
        //    {
        //        popUpIndex++;
        //        UpdateTutorial();
        //    }
        //}
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

        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));

    }


    void UpdateTutorial()
    {
        for (int i = 0; i < popUps.Length; ++i)
        {
            popUps[i].SetActive(i == popUpIndex);
        }
    }

    public void onBloodCleaned()
    {
        bloodCleaned = true;
    }
}
