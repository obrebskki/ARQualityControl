using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ERPMockedData : MonoBehaviour
{
    public List<ERPData> mockedList;
    public ERPMockedData()
    {
        mockedList = new List<ERPData>();
        mockedList.Add(EUVIC);
        mockedList.Add(EUVICTWO);
        mockedList.Add(BARCODE);
    }
    public ERPData EUVIC = new ERPData
    {
        item_identifier = "EUVIC",
        itemNo = 1,
        itemName = "Chrome screw",
        quantity = 12,
        oldest = new DateTime(2015, 12, 2),
        shippingTo = "Maria Kowalska",
        shippingDate = DateTime.Now,
        address = "Marii Skłodowskiej 5, 43-100 Tychy, Poland",
        responsible = "Jan Kowalski"
    };

    public ERPData EUVICTWO = new ERPData
    {
        item_identifier = "EUVIC2",
        itemNo = 2,
        itemName = "Steel cap",
        quantity = 15,
        oldest = new DateTime(2018, 2, 2),
        shippingTo = "Jan Kowalski",
        shippingDate = DateTime.Now,
        address = "Mickiewicza 8, 43-100 Tychy, Poland",
        responsible = "Rafał Obrębski"
    };

    public ERPData BARCODE = new ERPData
    {
        item_identifier = "EASDUVIC2", //to change
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
    public string item_identifier;
    public int itemNo;
    public string itemName;
    public int quantity;
    public DateTime oldest;
    public string shippingTo;
    public DateTime shippingDate;
    public string address;
    public string responsible;
    public int goAmount;
    public int noGoAmount;

}