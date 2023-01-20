using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureList : MonoBehaviour
{
    public List<Creature> creatures;
    public Creature creatureScript;

    private void Start()
    {
        creatures = new List<Creature>();
    }
    public void GenerateCreatures()
    {
        //creatures.Add(new Creature("Name", "Typ", HP, Skull, Stone, Wood, Tentacle, Ash));
        creatures.Add(new Creature("Dog", "Water", 8, 2, 0, 3, 2, 1));
        creatures.Add(new Creature("Cat", "Fire", 5, 1, 1, 1, 1, 0));


        creatures.Sort();
        Debug.Log(creatures[0].Name);
    }
}
