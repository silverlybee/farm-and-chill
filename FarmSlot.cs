using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FarmSlot : MonoBehaviour
{
    int currentPlant = 0;
    float plantTime = 0;
    int currentStage = 0;
    public float timeBetweenStages = 5f;

    public Farm farm;

    public Sprite[] plant1;
    public Sprite[] plant2;
    public Sprite[] plant3;
    public Sprite emptySlot;
    public Sprite dry;
    public Sprite locked;

    public bool isLocked = true;

    SpriteRenderer sprite;

    public int slotIndex;

    float speed = 1;

    bool isDry = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = emptySlot;
        updatePlant();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDry)
        {
            if (currentPlant != 0 && currentStage < 3)
            {
                plantTime -= Time.deltaTime*speed;
                if (plantTime < 0)
                {
                    plantTime = timeBetweenStages;
                    currentStage++;
                    updatePlant();
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        farm.SetOverFarmSlot(slotIndex);
    }

    private void OnMouseDown()
    {
        if(currentStage == 3)
        {
            farm.AddMoney(currentPlant * 20);
            currentStage = 0;
            currentPlant = 0;
            updatePlant();
            speed = 1;
        }
    }

    private void OnMouseExit()
    {
        farm.SetOverFarmSlot(-1);
    }


    public void Plant(int plantId)
    {
        if (isLocked)
        {
            if (plantId == 6 && farm.money >= 100)
            {
                isLocked = false;
                updatePlant();
                farm.AddMoney(-100);
            }
        }
        else if (plantId < 4 && currentPlant==0)
        {
            if (farm.money >= plantId * 10) {
                isDry = true;
                plantTime = timeBetweenStages;
                currentPlant = plantId;
                updatePlant();
                farm.AddMoney(plantId * -10);

                    }
        }
        else if(plantId == 4)
            {
                Water();
            }   
        else if (plantId == 5 && (farm.money>= 5) && (currentPlant != 0) && (currentStage < 3))
        {
            Fertilize();
        } 
    }

    public void updatePlant() {
        if (isLocked)
        {
            sprite.sprite = locked;
        }
        else if (!isDry)
        {
            switch (currentPlant)
            {
                case 0:
                    sprite.sprite = emptySlot;
                    break;
                case 1:
                    sprite.sprite = plant1[currentStage];
                    break;
                case 2:
                    sprite.sprite = plant2[currentStage];
                    break;
                case 3:
                    sprite.sprite = plant3[currentStage];
                    break;
            }
        } else
        {
            sprite.sprite = dry;
        }
    }

    void Water()
    {
        if (currentPlant>0)
        {
            isDry = false;
            updatePlant();
        }
    }

    void Fertilize()
    {
        farm.AddMoney(-5);
        speed *= 1.2f;
    }
}
