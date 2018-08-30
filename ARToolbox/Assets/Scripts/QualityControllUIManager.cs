using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityControllUIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject noGoDescription;


    public void ToggleNoGoDescription()
    {
        Debug.Log("Toggle");
        noGoDescription.SetActive(false);
        Debug.Log("After toggle: " + noGoDescription.activeSelf);
    }



}
