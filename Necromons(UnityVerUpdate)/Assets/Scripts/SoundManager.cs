using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        Debug.Log(gm.myCreatures.Count / 10);
        
    }
}
