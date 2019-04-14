using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMenu : MonoBehaviour
{
    public GameObject craftingMenu;

    public void Start()
    {
        CloseMenu();
    }

    public void Update()
    {
        if (Input.GetButtonDown("OpenInventory"))
        {
            if(craftingMenu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        craftingMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        craftingMenu.SetActive(false);
    }
}
