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

        Debug.Log("Im here");
        Info.SetActive(true);
        screw.SetActive(true);
        yield return new WaitForSeconds(1);
    }
}


