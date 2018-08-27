using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ERPMockedData : MonoBehaviour
{


    public static ERPData EUVIC_QR = new ERPData
    {
        itemNo = 1,
        itemName = "Chrome screw",
        quantity = 12,
        oldest = new DateTime(2015, 12, 2),
        shippingTo = "Maria Kowalska",
        shippingDate = DateTime.Now,
        address = "Husarii polskiej 5, 43-100 Tychy, Poland",
        responsible = "Jan Kowalski"
    };

    public static ERPData BARCODE = new ERPData
    {
        itemNo = 2,
        itemName = "Steel cap",
        quantity = 15,
        oldest = new DateTime(2018, 2, 2),
        shippingTo = "Jan Kowalski",
        shippingDate = DateTime.Now,
        address = "Mickiewicza 8, 43-100 Tychy, Poland",
        responsible = "Rafał Obrębski"
    };
}

public class ERPData
{
    public int itemNo;
    public string itemName;
    public int quantity;
    public DateTime oldest;
    public string shippingTo;
    public DateTime shippingDate;
    public string address;
    public string responsible;

}