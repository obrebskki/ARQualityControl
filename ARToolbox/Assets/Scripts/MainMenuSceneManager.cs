using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSceneManager : MonoBehaviour
{
    [SerializeField]
    private Image gazeIndicator;
    bool gazeStopped = false;


    void UserStartedGazing()
    {
        gazeStopped = false;
        StartCoroutine(GazeStarted());
    }

    void UserStoppedGazing()
    {
        StopCoroutine(GazeStarted());
        gazeIndicator.fillAmount = 0;
        gazeStopped = true;
    }

    private IEnumerator GazeStarted()
    {
        while (gazeIndicator.fillAmount < 1)
        {
            gazeIndicator.fillAmount += 0.01f;
            yield return new WaitForSeconds(Time.deltaTime / 2);
            if (gazeStopped)
                break;
        }
        if (Meta.Examples.GazeExampleScript.qualityControlVariantBeeingGazedOn == QualityControlVariant.QUICK && !gazeStopped)
        {
            Application.LoadLevel(1); //Change!!
        }
    }
}
