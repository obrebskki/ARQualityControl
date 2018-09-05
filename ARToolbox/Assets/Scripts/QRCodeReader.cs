using UnityEngine;
using System;
using System.Collections;
using Vuforia;
using System.Threading;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;



public class QRCodeReader : MonoBehaviour
{

    public delegate void CodeAction();
    public static event CodeAction OnCodeFind;

    private bool cameraInitialized;
    private BarcodeReader barCodeReader;
    private bool isDecoding = false;

    public UnityEngine.UI.Text itemNo;
    public UnityEngine.UI.Text itemName;
    public UnityEngine.UI.Text quantity;
    public UnityEngine.UI.Text oldest;
    public UnityEngine.UI.Text shippingTo;
    public UnityEngine.UI.Text shippingDate;
    public UnityEngine.UI.Text address;
    public UnityEngine.UI.Text responsible;


    public GameObject Info;

    [SerializeField]
    private GameObject euvicModel;
    [SerializeField]
    private GameObject euvicTwoModel;

    [SerializeField]
    private GameObject euvicModelFQC;
    [SerializeField]
    private GameObject euvicTwoModelFQC;

    public bool isGazing = false;
    GameObject modelToSetup;

    public static string currentCodeData;

    bool QRCodeBeenFound = false;
    ERPData currentDataFound;

    public static float timeSpentOnBox;
    // public static int boxesChecked;



    void Start()
    {
        timeSpentOnBox = 0;
        barCodeReader = new BarcodeReader();
        StartCoroutine(InitializeCamera());
        Debug.Log("camera instance: " + CameraDevice.Instance);
    }

    private IEnumerator InitializeCamera()
    {
        yield return new WaitForSeconds(1.25f);
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        Image.PIXEL_FORMAT mPixelFormat = Image.PIXEL_FORMAT.GRAYSCALE; // Need Grayscale for Editor

        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
        {
            Debug.Log("Successfully registered pixel format " + mPixelFormat.ToString());
        }
        else
        {
            Debug.Log("didnt registered pixel format " + mPixelFormat.ToString());

        }

        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        Debug.Log(String.Format("AutoFocus : {0}", isAutoFocus));
        cameraInitialized = true;
    }

    private void Update()
    {
        timeSpentOnBox += Time.deltaTime;
        if (cameraInitialized && !isDecoding)
        {
            try
            {
                var cameraFeed = CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.GRAYSCALE);

                if (cameraFeed == null)
                {
                    return;
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(DecodeQr), cameraFeed);

            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }


        if (isGazing)
        {
            Info.SetActive(false);
            euvicModel.SetActive(false);
            euvicTwoModel.SetActive(false);
            QRCodeBeenFound = false;

            currentCodeData = "";
        }

        if (QRCodeBeenFound)
        {
            SetupCurrentQRData();
            Info.SetActive(true);
            euvicModel.SetActive(false);
            euvicTwoModel.SetActive(false);
            if (currentDataFound.item_identifier == "EUVIC")
            {
                QualityControllUIManager.codeRead = CODE_READ.EUVIC;
                switch (QualityControllUIManager.choosenQualityControlVariant)
                {
                    case QualityControlVariant.FULL:
                        euvicModelFQC.SetActive(true);
                        break;
                    case QualityControlVariant.QUICK:
                        euvicModel.SetActive(true);
                        break;
                }
            }

            else if (currentDataFound.item_identifier == "EUVIC2")
            {
                QualityControllUIManager.codeRead = CODE_READ.EUVIC_TWO;

                switch (QualityControllUIManager.choosenQualityControlVariant)
                {
                    case QualityControlVariant.FULL:
                        euvicTwoModelFQC.SetActive(true);
                        break;
                    case QualityControlVariant.QUICK:
                        euvicTwoModel.SetActive(true);
                        break;
                }
            }
        }
    }

    private void DecodeQr(object state)
    {
        isDecoding = true;
        var cameraFeed = (Image)state;
        var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.Gray8);
        if (data != null && data.Text != currentCodeData)
        {

            ERPMockedData mockedData = new ERPMockedData();
            foreach (ERPData erpData in mockedData.mockedList)
            {
                if (data.Text == erpData.item_identifier)
                {
                    currentDataFound = erpData;
                    currentCodeData = erpData.item_identifier;
                    QRCodeBeenFound = true;
                    currentCodeData = "";
                }
            }
        }

        isDecoding = false;

    }

    void SetupCurrentQRData()
    {
        QRCodeBeenFound = false;


        itemNo.text = "item no: " + currentDataFound.itemNo.ToString();
        itemName.text = currentDataFound.itemName;
        quantity.text = "quantity: " + currentDataFound.quantity.ToString();
        oldest.text = "oldest " + currentDataFound.oldest.ToString();
        shippingTo.text = "shipping to: " + currentDataFound.shippingTo;
        shippingDate.text = "shipping date: " + currentDataFound.shippingDate;
        address.text = "address: " + currentDataFound.address;
        responsible.text = "responsible: " + currentDataFound.responsible;
    }
}


