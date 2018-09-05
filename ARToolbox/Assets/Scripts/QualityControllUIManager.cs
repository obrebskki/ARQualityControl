using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualityControllUIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject noGoDescription;
    [SerializeField]
    private Image gazeIndicator;
    [SerializeField]
    private GameObject euvicTouchMarker;
    [SerializeField]
    private GameObject euvicTwoTouchMarker;
    [SerializeField]
    private GameObject menuSceneObjects;
    [SerializeField]
    private GameObject menuCanvasObjects;
    [SerializeField]
    private GameObject itemDescriptionCanvas;
    [SerializeField]
    private GameObject qualityControlGazeButtons;
    public static QualityControlVariant choosenQualityControlVariant;
    [SerializeField]
    private List<GameObject> objectsToCloseOnGoDecision;
    [SerializeField]
    private List<GameObject> objectsToCloseOnRestartDecision;
    public static CODE_READ codeRead;
    [SerializeField]
    private List<GameObject> itemModels;
    [SerializeField]
    private GameObject decisionPanel;
    [SerializeField]
    private GameObject screenshotPanel;

    [SerializeField]
    private GameObject summaryPanel;
    [SerializeField]
    private Text timeSpentText;
    [SerializeField]
    private Text amountGoText;
    [SerializeField]
    private Text amountNoGoText;
    bool gazeStopped = false;
    int changeMarker = 0;

    private int amountGo;
    private int amountNoGo;
    private int boxesChecked;
    [SerializeField]
    private GameObject screenshotButton;

    public void ToggleNoGoDescription()
    {
        noGoDescription.SetActive(!noGoDescription.activeSelf);
    }

    public void QualityCotrolGazeStartQE()
    {
        gazeStopped = false;
        StartCoroutine(GazeStarted(QualityControlVariant.QUICK));
    }

    public void QualityCotrolGazeStartQF()
    {
        gazeStopped = false;
        StartCoroutine(GazeStarted(QualityControlVariant.FULL));
    }

    public void QualityControllEnd()
    {
        StopCoroutine(GazeStarted(QualityControlVariant.NONE));
        gazeIndicator.fillAmount = 0;
        gazeStopped = true;
    }


    private IEnumerator GazeStarted(QualityControlVariant qualityControlVariantBeeingGazedOn)
    {
        while (gazeIndicator.fillAmount < 1)
        {
            gazeIndicator.fillAmount += 0.01f;
            yield return new WaitForSeconds(Time.deltaTime / 2);
            if (gazeStopped)
                break;
        }
        if (!gazeStopped)
        {
            menuSceneObjects.SetActive(false);
            menuCanvasObjects.SetActive(false);
            qualityControlGazeButtons.SetActive(true);
            choosenQualityControlVariant = qualityControlVariantBeeingGazedOn;
        }

    }

    public void ClearQRScreen()
    {
        itemDescriptionCanvas.SetActive(false);
        QRCodeReader.currentCodeData = "";
        foreach (GameObject model in itemModels)
        {
            model.SetActive(false);
        }
    }

    public void UserTouchedMarker()
    {
        changeMarker += 45;
        switch (codeRead)
        {
            case CODE_READ.EUVIC:
                euvicTouchMarker.transform.Rotate(new Vector3(0, changeMarker, 0));
                break;
            case CODE_READ.EUVIC_TWO:
                euvicTwoTouchMarker.transform.Rotate(new Vector3(0, changeMarker, 0));
                break;
        }
        if (changeMarker == 135)
        {
            foreach (GameObject models in itemModels)
            {
                models.SetActive(false);
            }
            decisionPanel.SetActive(true);
            itemDescriptionCanvas.SetActive(false);
        }
    }

    public void GoDecision()
    {
        foreach (GameObject objectToClose in objectsToCloseOnGoDecision)
        {
            objectToClose.SetActive(false);
        }
        amountGo++;
        changeMarker = 0;
        CheckIfLastBox();
    }

    public void RestartDecision()
    {
        foreach (GameObject objectToClose in objectsToCloseOnRestartDecision)
        {
            objectToClose.SetActive(false);
        }
        changeMarker = 0;
        itemDescriptionCanvas.SetActive(true);

    }

    public void NoGoDecision()
    {
        foreach (GameObject objectToClose in objectsToCloseOnGoDecision)
        {
            objectToClose.SetActive(false);
        }
        amountNoGo++;
        screenshotPanel.SetActive(true);
        screenshotButton.SetActive(true);
    }
    public void TakeAScreenshot()
    {
        ScreenCapture.CaptureScreenshot("Screenshot.png");
        foreach (GameObject objectToClose in objectsToCloseOnGoDecision)
        {
            objectToClose.SetActive(false);
        }
        screenshotPanel.SetActive(false);
        changeMarker = 0;
        CheckIfLastBox();
    }


    void CheckIfLastBox()
    {
        boxesChecked++;
        if (boxesChecked == 3)
        {
            foreach (GameObject objectToClose in objectsToCloseOnGoDecision)
            {
                objectToClose.SetActive(false);
            }
            screenshotPanel.SetActive(false);
            summaryPanel.SetActive(true);
            amountNoGoText.text = amountNoGoText.text + amountNoGo.ToString();
            amountGoText.text = amountGoText.text + amountGo.ToString();
            float minutes = Mathf.Floor(QRCodeReader.timeSpentOnBox / 60);
            float seconds = Mathf.RoundToInt(QRCodeReader.timeSpentOnBox % 60);
            if (minutes > 0 && minutes < 2)
                timeSpentText.text = timeSpentText.text + " " + minutes + " minute, " + seconds + " seconds.";
            else if (minutes > 1)
                timeSpentText.text = timeSpentText.text + " " + minutes + " minutes, " + seconds + " seconds.";
            else
                timeSpentText.text = timeSpentText.text + " " + seconds + " seconds.";


        }
    }

}

public enum CODE_READ { EUVIC, EUVIC_TWO }