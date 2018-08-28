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
    public bool firstIf = false;
    public bool cameraFeedNull = false;

    public UnityEngine.UI.Text itemNo;
    public UnityEngine.UI.Text itemName;
    public UnityEngine.UI.Text quantity;
    public UnityEngine.UI.Text oldest;
    public UnityEngine.UI.Text shippingTo;
    public UnityEngine.UI.Text shippingDate;
    public UnityEngine.UI.Text address;
    public UnityEngine.UI.Text responsible;


    public GameObject Info;
    public GameObject screw;
    public GameObject cap;

    public bool isGazing = false;

    private string currentCodeData;
    bool foundEuvic = false;
    bool foundOther = false;
    public SceneManager sceneManager;
    void Start()
    {
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
        if (cameraInitialized && !isDecoding)
        {
            firstIf = true;
            try
            {
                var cameraFeed = CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.GRAYSCALE);

                if (cameraFeed == null)
                {
                    cameraFeedNull = true;
                    return;
                }
                else
                {
                    cameraFeedNull = false;

                }
                ThreadPool.QueueUserWorkItem(new WaitCallback(DecodeQr), cameraFeed);

            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        else
        {
            firstIf = false;
        }
        if (foundEuvic)
        {
            Info.SetActive(true);
            screw.SetActive(true);
            cap.SetActive(false);

            itemNo.text = "Item no: " + ERPMockedData.EUVIC_QR.itemNo.ToString();
            itemName.text = ERPMockedData.EUVIC_QR.itemName;
            quantity.text = "Quantity: " + ERPMockedData.EUVIC_QR.quantity.ToString();
            oldest.text = "Oldest:" + ERPMockedData.EUVIC_QR.oldest.ToString();
            shippingTo.text = "Shipping to:" + ERPMockedData.EUVIC_QR.shippingTo;
            shippingDate.text = "Shipping date: " + ERPMockedData.EUVIC_QR.shippingDate;
            address.text = "Shipping address:" + ERPMockedData.EUVIC_QR.address;
            responsible.text = "Responsible: " + ERPMockedData.EUVIC_QR.responsible;
        }
        else if (foundOther)
        {
            Info.SetActive(true);
            screw.SetActive(false);
            cap.SetActive(true);
            itemNo.text = "Item no: " + ERPMockedData.BARCODE.itemNo.ToString();
            itemName.text = "Item name: " + ERPMockedData.BARCODE.itemName;
            quantity.text = "Quantity: " + ERPMockedData.BARCODE.quantity.ToString();
            oldest.text = "Oldest:" + ERPMockedData.BARCODE.oldest.ToString();
            shippingTo.text = "Shipping to:" + ERPMockedData.BARCODE.shippingTo;
            shippingDate.text = "Shipping date: " + ERPMockedData.BARCODE.shippingDate;
            address.text = "Shipping address:" + ERPMockedData.BARCODE.address;
            responsible.text = "Responsible: " + ERPMockedData.BARCODE.responsible;
        }
        if (isGazing)
        {
            Info.SetActive(false);
            screw.SetActive(false);
            cap.SetActive(false);
            foundEuvic = false;
            foundOther = false;
            currentCodeData = "";
        }
    }

    private void DecodeQr(object state)
    {
        isDecoding = true;
        var cameraFeed = (Image)state;
        var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.Gray8);
        if (data != null && data.Text != currentCodeData)
        {
            if (data.Text == "EUVIC")
            {
                foundEuvic = true;
                foundOther = false;
            }
            else if (data.Text == "705632085943")
            {
                foundOther = true;
                foundEuvic = false;
            }
            currentCodeData = data.Text;
            Debug.Log("found data: " + data.Text);
            isDecoding = false;
        
        }
        else
        {
            isDecoding = false;

        }
    }

    public IEnumerator NewCodeFound()
    {
        Debug.Log("I'm here");
        Info.SetActive(true);
        screw.SetActive(true);
        yield return new WaitForSeconds(2);
    }
}


