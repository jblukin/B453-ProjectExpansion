using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapManager : MonoBehaviour
{
    GameManager gm;
    public GameObject myCreaturePrefab;
    GameObject myCurrentCreature;
    GameObject myMonsterList;
    public int index;


    private void Awake()
    {
        gm = GetComponent<GameManager>();
        myMonsterList = GameObject.Find("MyMonsterList");
    }

    private void Update()
    {
            GameObject.Find("CurrentResources").GetComponent<TextMeshProUGUI>().text = "Current Resources: " + "Feathers " + gm.featherCount + " | " + " Tentacle " + gm.tentacleCount + " | " + " Stones " + gm.stoneCount + " | " + " Wood " + gm.woodCount + " | " + " Ash " + gm.ashCount;
    }

    public void SpawnMyMonsters()
    {

        for (int i = index; i < gm.myCreatures.Count; i++)
        {
            myCurrentCreature = Instantiate(myCreaturePrefab);
            myCurrentCreature.transform.SetParent(myMonsterList.transform);
            myCurrentCreature.GetComponent<MyMonster>().thisCreature = gm.myCreatures[i];
            myCurrentCreature.GetComponent<MyMonster>().id = i;
            index = i + 1;
        }
    }
}
