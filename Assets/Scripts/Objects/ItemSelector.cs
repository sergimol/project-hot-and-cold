using System;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    private static System.Random rng = new System.Random();

    void Start()
    {
        int selectedItem = rng.Next(gameObject.transform.childCount);
        GameObject selected = gameObject.transform.GetChild(selectedItem).transform.GetChild(1).gameObject;

        selected.GetComponent<ObjectProperties>().searchingThis = true;
    }
}
