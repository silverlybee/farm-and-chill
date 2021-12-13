using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Farm : MonoBehaviour
{

    public int itemId;
    public int overFarmSlot = -1;

    public GameObject farmSlot;
    public int size;

    List<FarmSlot> slots = new List<FarmSlot>();

    public int money = 20;

    public Text moneyTxt;

    // Start is called before the first frame update
    void Start()
    {
        overFarmSlot = -1;
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<FarmSlot>());
        }
        moneyTxt.text = "$ " + money;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem(int dragItem)
    {
        if(overFarmSlot>=0)
        {
            slots[overFarmSlot].Plant(dragItem);
        }


    }

    public void SetOverFarmSlot( int value)
    {
        if(overFarmSlot == -1 || value ==-1)
        {
            overFarmSlot = value;
        }
    }

    public void GenerateFarm()
    {
        Debug.Log("Generate new farm");

        DeleteOldFarm();

        for (int i = 0; i < size*size; i++)
        {
            var newSlot = Instantiate(farmSlot, new Vector2(transform.position.x + 2 * (i % size), transform.position.y - 2 * (i / size)), Quaternion.identity);
            newSlot.transform.SetParent(transform);
            slots.Add(newSlot.GetComponent<FarmSlot>());
            newSlot.GetComponent<FarmSlot>().farm = GetComponent<Farm>();
            newSlot.GetComponent<FarmSlot>().slotIndex = i;
            if (i < 4)
            {
                newSlot.GetComponent<FarmSlot>().isLocked = false;
            }
            Debug.Log(slots[i].slotIndex);
        }
    }

    public void DeleteOldFarm()
    {
        slots.Clear();
        for (int i = gameObject.transform.childCount; i > 0; i--)
        {
            DestroyImmediate(gameObject.transform.GetChild(i - 1).gameObject);
        }
    }

    public void AddMoney(int value)
    {
        money += value;
        moneyTxt.text = "$ " + money;
    }
}
