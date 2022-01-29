using System;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    private static System.Random rng = new System.Random();
    [SerializeField]
    bool hideScene;

    int selectedItem;

    void Start()
    {
        if (hideScene)
        {
            selectedItem = rng.Next(gameObject.transform.childCount);
            GameManager.instance.actualObject = selectedItem;
        }
        else
            selectedItem = GameManager.instance.actualObject;


        GameObject selected = gameObject.transform.GetChild(selectedItem).transform.GetChild(1).gameObject;
        selected.GetComponent<ObjectProperties>().searchingThis = true;

    }

    public int getObjectId()
    {
        return selectedItem;
    }
}
