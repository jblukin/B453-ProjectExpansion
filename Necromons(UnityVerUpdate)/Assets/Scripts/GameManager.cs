using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int featherCount;
    public int stoneCount;
    public int woodCount;
    public int tentacleCount;
    public int ashCount;

    public TextMeshProUGUI featherCountUI;
    public TextMeshProUGUI stoneCountUI;
    public TextMeshProUGUI woodCountUI;
    public TextMeshProUGUI tentacleCountUI;
    public TextMeshProUGUI ashCountUI;
    public GameObject resources;

    public GameObject areas;
    public int level;
    public int raidCount;
    public int levelUpReq;
    public List<Creature> myCreatures;


    public int raidMonsterID;
    public GameObject raidMonster;

    void Awake()
    {

        featherCount = 4;
        stoneCount = 4;
        woodCount = 4;
        tentacleCount = 4;
        ashCount = 4;

        level = 1;

        levelUpReq = 5;

        raidCount = 0;


        if (instance == null)
        {

            instance = this;

        }

        else if (instance != this)
        {

            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);

        resources = GameObject.Find("Resources");
        featherCountUI = resources.transform.Find("FeatherCount").GetComponent<TextMeshProUGUI>();
        stoneCountUI = resources.transform.Find("StoneCount").GetComponent<TextMeshProUGUI>();
        woodCountUI = resources.transform.Find("WoodCount").GetComponent<TextMeshProUGUI>();
        tentacleCountUI = resources.transform.Find("TentacleCount").GetComponent<TextMeshProUGUI>();
        ashCountUI = resources.transform.Find("AshCount").GetComponent<TextMeshProUGUI>();
        myCreatures = new List<Creature>();

        areas = GameObject.Find("Areas");

        RefreshResources();
    }

    public void RefreshResources()
    {
        featherCountUI.text = featherCount.ToString();
        stoneCountUI.text = stoneCount.ToString();
        woodCountUI.text = woodCount.ToString();
        tentacleCountUI.text = tentacleCount.ToString();
        ashCountUI.text = ashCount.ToString();
        return;
    }

    public void SelectRaidMonster(int id, GameObject raidCreature)
    {
        raidMonsterID = id;
        raidMonster = raidCreature;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartRaid(string location)
    {
        if (raidMonster != null)
        {

            raidCount++;

            Debug.Log("RaidCount: " + raidCount);

            if(raidCount == levelUpReq) { //Leveling up requirement met if true

            Debug.Log("Leveled Up");

                raidCount = 0; //reset raid count (amount is equivalent to exp)

                levelUpReq++; //raises levelreq

                level++; //raises level

                if(level == 3) { 

                    areas.transform.Find("Forest").GetComponent<Button>().interactable = true;
                    areas.transform.Find("Forest").GetChild(1).gameObject.SetActive(false);

                }

                if(level == 5) {

                    areas.transform.Find("Ocean").GetComponent<Button>().interactable = true;
                    areas.transform.Find("Ocean").GetChild(1).gameObject.SetActive(false);

                    areas.transform.Find("Vulcano").GetComponent<Button>().interactable = true;
                    areas.transform.Find("Vulcano").GetChild(1).gameObject.SetActive(false);

                }

            }

            if (location == "Sky")
            {
                if (raidMonster.GetComponent<MyMonster>().thisCreature.Type == "Wind")
                {
                    featherCount += 2 * Random.Range(1, 3);

                    
                        ashCount += 2 * Random.Range(1, 3);
                    

                    // if (Random.Range(1, 2) == 1)
                    // {
                    //     stoneCount += 2 * Random.Range(1, 3);
                    // }

                    // if (Random.Range(1, 4) == 4)
                    // {
                    //     woodCount += 2 * Random.Range(1, 3);
                    // }

                }
                else
                {
                    featherCount += Random.Range(1, 3);

                    
                        ashCount += Random.Range(1, 3);
                    

                    // if (Random.Range(1, 2) == 1)
                    // {
                    //     stoneCount += Random.Range(1, 3);
                    // }

                    // if (Random.Range(1, 4) == 4)
                    // {
                    //     woodCount += Random.Range(1, 3);
                    // }
                }
            }

            if (location == "Mountains")
            {
                if (raidMonster.GetComponent<MyMonster>().thisCreature.Type == "Stone")
                {
                    stoneCount += 2 * Random.Range(1, 3);

                    // if (Random.Range(1, 4) != 4)
                    // {
                    //     featherCount += 2 * Random.Range(1, 3);
                    // }

                    if (Random.Range(1, 2) == 1)
                    {
                        woodCount += 2 * Random.Range(1, 3);
                    }

                    
                    tentacleCount += 2 * Random.Range(1, 3);
                    

                }
                else
                {
                    stoneCount += Random.Range(1, 3);

                    // if (Random.Range(1, 4) != 4)
                    // {
                    //     featherCount += Random.Range(1, 3);
                    // }

                    if (Random.Range(1, 2) == 1)
                    {
                        woodCount += Random.Range(1, 3);
                    }

                    
                        tentacleCount += Random.Range(1, 3);
                    
                }
            }


            if (location == "Forest")
            {
                if (raidMonster.GetComponent<MyMonster>().thisCreature.Type == "Nature")
                {
                    woodCount += 2 * Random.Range(1, 3);

                    // if (Random.Range(1, 4) != 4)
                    // {
                    //     tentacleCount += 2 * Random.Range(1, 3);
                    // }

                    
                        featherCount += 2 * Random.Range(1, 3);
                    

                    
                        stoneCount += 2 * Random.Range(1, 3);
                    

                }
                else
                {
                    woodCount += Random.Range(1, 3);

                    // if (Random.Range(1, 4) != 4)
                    // {
                    //     tentacleCount += Random.Range(1, 3);
                    // }

                    
                        featherCount += Random.Range(1, 3);
                    

                    
                        stoneCount += Random.Range(1, 3);
                    
                }
            }

            if (location == "Ocean")
            {
                if (raidMonster.GetComponent<MyMonster>().thisCreature.Type == "Water")
                {
                    //Debug.Log("HelloThisIsBOB");
                    tentacleCount += 2 * Random.Range(1, 3);

                    
                        woodCount += 2 * Random.Range(1, 3);
                    

                    // if (Random.Range(1, 2) == 1)
                    // {
                    //     featherCount += 2 * Random.Range(1, 3);
                    // }

                    // if (Random.Range(1, 4) == 4)
                    // {
                    //     ashCount += 2 * Random.Range(1, 3);
                    // }

                }
                else
                {
                    tentacleCount += Random.Range(1, 3);

                    
                        woodCount += Random.Range(1, 3);
                    

                    // if (Random.Range(1, 2) == 1)
                    // {
                    //     featherCount += Random.Range(1, 3);
                    // }

                    // if (Random.Range(1, 4) == 4)
                    // {
                    //     ashCount += Random.Range(1, 3);
                    // }
                }
            }

            if (location == "Vulcano")
            {
                if (raidMonster.GetComponent<MyMonster>().thisCreature.Type == "Fire")
                {
                    ashCount += 2 * Random.Range(1, 3);

                    
                        stoneCount += 2 * Random.Range(1, 3);
                    

                    
                        tentacleCount += 2 * Random.Range(1, 3);
                    

                    // if (Random.Range(1, 4) == 4)
                    // {
                    //     featherCount += 2 * Random.Range(1, 3);
                    // }
                }
                else
                {
                    ashCount += Random.Range(1, 3);

                    
                        stoneCount += Random.Range(1, 3);
                    

                    
                        tentacleCount += Random.Range(1, 3);
                    

                    // if (Random.Range(1, 4) == 4)
                    // {
                    //     featherCount += Random.Range(1, 3);
                    // }
                }
            }
        }
        raidMonster.GetComponent<MyMonster>().SendToRaid();
        myCreatures[raidMonsterID].HP -= Random.Range(1, 3);
        raidMonster.GetComponent<MyMonster>().thisCreature.HP = myCreatures[raidMonsterID].HP;
        Debug.Log("Sending: " + raidMonster.GetComponent<MyMonster>().thisCreature.Name + " to " + location);
        Debug.Log(message: "Feathers " + featherCount + " Tentacle " + tentacleCount + " Stones " + stoneCount + " Wood " + woodCount + " Ash " + ashCount);
        raidMonster = null;
    }
}
