using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharkkkacterInventory : Inventory
{
    public GameObject slotObject;
    public Vector2 inventoryPosition;
    public int gridWidth = 0, gridHeight = 0;
    public float slotWidth, slotOffset;

    public int startSlot = -1;
    public int endSlot = -1;


    public int inventorySelected = 0;

    public bool isDraging = false;

    public Image dragItem;

   
    private Slot[] slotsItems;


    private void Start()
    { 
        //GenerateInventoryUI();

    }

    private void Update()
    {
        DragItem();

        if(startSlot > 0 && endSlot > 0)
        {
            //SwapItems();
            startSlot = -1;
        }
  
    }

    void DragItem()
    {
        if (isDraging)
        {
            dragItem.enabled = true;
            Vector3 p = Input.mousePosition;
                
            dragItem.transform.position = p;

            if (Input.GetButtonUp("Fire1"))
            {
                dragItem.enabled = false;
                isDraging = false;
            }
        }
    }

    public void SwapItems(Item source, Item destination)
    {
        if (endSlot < 0 || startSlot < 0)
            return;

        Item s = source;
        source = destination;
        destination = s;


    }

    public void AddItem(Item i, Item[,]inventory)
    {
        for(int a = 0; a < gridWidth * gridHeight; a++)
        {
            if(inventory[a,inventorySelected] == null)
            {
                inventory[a, inventorySelected] = i;
                UpdateInventoryUI();
                return;
            }
        }
    }

    public void AddItemAt(Item i, Item[,] inventory)
    {
        Item end = inventory[startSlot, inventorySelected];

        inventory[endSlot, inventorySelected] = i;
        inventory[startSlot, inventorySelected] = end;

    }

    public void RemoveItem(int index, Item[] inventory)
    {
        inventory[index] = null;
    }

    public void UpdateInventoryUI()
    {
        for(int i =0; i< gridHeight * gridWidth; i++)
        {
            /*if(items[i,inventorySelected] != null)
            {
                slotsItems[i].icon.color = Color.red;
            }
            else
            {
                slotsItems[i].icon.color = Color.white;
            }*/
        }
    }

    public void DropItem(int index, Item[] inventory)
    {
        inventory[index] = null;

    }

    /*
    private IEnumerator UpdateInventoryPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            for(int i =0; i < gridWidth * gridHeight; i++)
            {
                slotsItems[i].transform.position = slotsItems[i].startPos + new Vector3(inventoryPosition.x,0f,inventoryPosition.y);
            }

        }
    }*/

    private void GenerateInventoryUI()
    {

        int ax = 0;

        for(int y = gridWidth; y > 0; y--)
        {
            for (int x = 0; x < gridHeight; x++)
            {

                GameObject go = Instantiate(slotObject, new Vector3(x*slotWidth + x*slotOffset + inventoryPosition.x, y*slotWidth + y * slotOffset +inventoryPosition.y, 300f), Quaternion.identity);
                go.transform.parent = this.transform;
                go.GetComponent<Slot>().id = transform.childCount;
                //go.GetComponent<Slot>().startPos = go.transform.position;
                
                slotsItems[ax] = go.GetComponent<Slot>();
                ax++;
            }
        }
    }
}
