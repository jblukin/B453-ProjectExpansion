using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Creature : IComparable<Creature>
{
    public string Name;
    public string Type;
    public int imageIndex;
    public int HP;
    public int Feather;
    public int Stone;
    public int Wood;
    public int Tentacle;
    public int Ash;

    public Creature(string name, string type,  int hp, int feather, int stone, int wood, int tentacle, int ash)
    {
        Name = name;
        Type = type;
        HP = hp;
        Feather = feather;
        Stone = stone;
        Wood = wood;
        Tentacle = tentacle;
        Ash = ash;
        return ;
    }

    public int CompareTo(Creature other)
    {
        if (other == null)
        {
            return 1;
        }

        return HP - other.HP;
    }
}