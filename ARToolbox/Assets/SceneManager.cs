using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public Text itemNo;
    public Text itemName;
    public Text quantity;
    public Text oldest;
    public Text shippingTo;
    public Text shippingDate;
    public Text address;
    public Text responsible;

    public GameObject Info;
    public GameObject screw;

    private void OnEnable()
    {
        // QRCodeReader.OnCodeFind += NewCodeFound;
    }


    public IEnumerator NewCodeFound()
    {
        //if (data == "EUVIC")
        //{
        Debug.Log("Im here");
        Info.SetActive(true);
        screw.SetActive(true);
        yield return new WaitForSeconds(1);
        //Debug.Log("Data was eucic");
        //itemNo.text = "Item no: " + ERPMockedData.EUVIC_QR.itemNo.ToString();
        //itemName.text = "Item name: " + ERPMockedData.EUVIC_QR.itemName;
        //quantity.text = "Quantity: " + ERPMockedData.EUVIC_QR.quantity.ToString();
        //oldest.text = "Oldest:" + ERPMockedData.EUVIC_QR.oldest.ToString();
        //shippingTo.text = "Shipping to:" + ERPMockedData.EUVIC_QR.shippingTo;
        //shippingDate.text = "Shipping date: " + ERPMockedData.EUVIC_QR.shippingDate.ToString();
        //address.text = "Shipping address:" + ERPMockedData.EUVIC_QR.address;
        //responsible.text = "Responsible: " + ERPMockedData.EUVIC_QR.responsible;


        //}
    }
}


