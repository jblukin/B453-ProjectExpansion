using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CreatureManager : MonoBehaviour
{

    Sprite currentCreatureImage;
    public Object[] creaturesToLoad;
    public List<Creature> allCreatures;
    GameObject creatureCard;
    TextMeshProUGUI creatureName;
    Image creatureImage;
    TextMeshProUGUI creatureHP;
    TextMeshProUGUI creatureType;
    TextMeshProUGUI creatureFeather;
    TextMeshProUGUI creatureStone;
    TextMeshProUGUI creatureWood;
    TextMeshProUGUI creatureTentacle;
    TextMeshProUGUI creatureAsh;
    public Creature currentCreature;
    public Creature noCreature;
    public Sprite emptySprite;
    GameManager gm;
    int rnd;
    bool readyToSpawn;
    public AudioSource audioSource;

    void Start()
    {
        creaturesToLoad = Resources.LoadAll("Images", typeof(Sprite));
        allCreatures = new List<Creature>();
        GenerateCreatures();
        creatureCard = GameObject.Find("CreatureCard");
        creatureName = creatureCard.transform.Find("CreatureName").GetComponent<TextMeshProUGUI>();
        creatureImage = creatureCard.transform.Find("CreatureImage").GetComponent<Image>();
        creatureHP = creatureCard.transform.Find("CreatureHP").GetComponent<TextMeshProUGUI>();
        creatureType = creatureCard.transform.Find("CreatureType").GetComponent<TextMeshProUGUI>();
        creatureFeather = creatureCard.transform.Find("CreatureFeather").GetComponent<TextMeshProUGUI>();
        creatureStone = creatureCard.transform.Find("CreatureStone").GetComponent<TextMeshProUGUI>();
        creatureWood = creatureCard.transform.Find("CreatureWood").GetComponent<TextMeshProUGUI>();
        creatureTentacle = creatureCard.transform.Find("CreatureTentacle").GetComponent<TextMeshProUGUI>();
        creatureAsh = creatureCard.transform.Find("CreatureAsh").GetComponent<TextMeshProUGUI>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rnd = Random.Range(0, creaturesToLoad.Length);
        audioSource = GameObject.Find("Skeleton_Dance").GetComponent<AudioSource>();


        noCreature.Name = "";
        noCreature.Type = "";
        noCreature.HP = 0;
        noCreature.Feather = 0;
        noCreature.Stone = 0;
        noCreature.Tentacle = 0;
        noCreature.Wood = 0;
        noCreature.Ash = 0;

        CreatureSpawn();

    }
    public void GenerateCreatures()
    {
        //creatures.Add(new Creature("Name", "Typ", HP, Feather, Stone, Wood, Tentacle, Ash));
        allCreatures.Add(new Creature("Boneknapper", "Water", 8, 2, 0, 3, 4, 1));
        allCreatures.Add(new Creature("Keplon", "Water", 5, 4, 1, 2, 3, 0));
        allCreatures.Add(new Creature("Plattypearl", "Water", 5, 3, 1, 0, 4, 2));
        allCreatures.Add(new Creature("Poisick", "Water", 8, 1, 1, 1, 3, 4));
        allCreatures.Add(new Creature("Frepard", "Fire", 5, 0, 2, 1, 3, 4));
        allCreatures.Add(new Creature("Scauldron", "Fire", 8, 1, 4, 0, 2, 3));
        allCreatures.Add(new Creature("Zkill", "Fire", 8, 1, 4, 1, 1, 1));
        allCreatures.Add(new Creature("Phex", "Fire", 8, 0, 2, 1, 4, 3));
        allCreatures.Add(new Creature("Greath", "Wind", 8, 4, 2, 1, 0, 3));
        allCreatures.Add(new Creature("Tribat", "Wind", 5, 3, 0, 2, 1, 4));
        allCreatures.Add(new Creature("Deathstar", "Wind", 8, 1, 4, 1, 1, 1));
        allCreatures.Add(new Creature("Siamcloud", "Wind", 5, 4, 1, 0, 2, 3));
        allCreatures.Add(new Creature("Werefangs", "Nature", 8, 2, 0, 4, 3, 1));
        allCreatures.Add(new Creature("Plaguestag", "Nature", 5, 1, 2, 3, 4, 0));
        allCreatures.Add(new Creature("Nox", "Nature", 5, 0, 1, 3, 4, 2));
        allCreatures.Add(new Creature("Skullington", "Stone", 8, 3, 4, 2, 0, 1));
        allCreatures.Add(new Creature("Cafear", "Stone", 5, 4, 3, 0, 1, 2));
        allCreatures.Add(new Creature("Flashnight", "Stone", 5, 3, 4, 1, 2, 0));


        allCreatures.Sort();
    }

    public void RandomizedNumber()
    {
        rnd = Random.Range(0, creaturesToLoad.Length);
    }

    public void CreatureSpawn()
    {
        RandomizedNumber();
        currentCreatureImage = (Sprite)creaturesToLoad[rnd];
        for (int i = 0; i < allCreatures.Count; i++)
        {
            if (allCreatures[i].Name == currentCreatureImage.name)
            {
                allCreatures[i].imageIndex = rnd;
                currentCreature = allCreatures[i];
                Debug.Log("Found " + currentCreature.Name);
                RefreshCreatureCard();
                readyToSpawn = true;
                break;
            }
        }
    }

    public void RefreshCreatureCard()
    {
        creatureName.text = currentCreature.Name;
        creatureImage.sprite = currentCreatureImage;
        creatureHP.text = currentCreature.HP.ToString();
        creatureType.text = currentCreature.Type.ToString();
        creatureFeather.text = "Feather: " + currentCreature.Feather.ToString();
        creatureStone.text = "Stone: " + currentCreature.Stone.ToString();
        creatureWood.text = "Wood: " + currentCreature.Wood.ToString();
        creatureTentacle.text = "Tentacle: " + currentCreature.Tentacle.ToString();
        creatureAsh.text = "Ash: " + currentCreature.Ash.ToString();
    }

    IEnumerator TimeToSpawn()
    {
        yield return new WaitForSeconds(Random.Range(2,5));
        CreatureSpawn();
    }

    public void CreatureRevive()
    {
        if(currentCreature.Ash <= gm.ashCount && currentCreature.Feather <= gm.featherCount && currentCreature.Stone <= gm.stoneCount && currentCreature.Tentacle <= gm.tentacleCount && currentCreature.Wood <= gm.woodCount && readyToSpawn)
        {
            gm.ashCount -= currentCreature.Ash;
            gm.featherCount -= currentCreature.Feather;
            gm.stoneCount -= currentCreature.Stone;
            gm.tentacleCount -= currentCreature.Tentacle;
            gm.woodCount -= currentCreature.Wood;

            gm.RefreshResources();
            gm.myCreatures.Add(currentCreature);
            audioSource.pitch = 0.8F + (gm.myCreatures.Count / 10f);

            Debug.Log(audioSource.name + " " + audioSource.pitch + " " + (gm.myCreatures.Count / 10f));
            
            currentCreature = noCreature;
            currentCreatureImage = emptySprite;
            RefreshCreatureCard();
            readyToSpawn = false;
            StartCoroutine(TimeToSpawn());
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GainResources(10);
        }
    }

    public void GainResources(int ResourcesCount)
    {
        gm.ashCount += ResourcesCount;
        gm.featherCount += ResourcesCount;
        gm.stoneCount += ResourcesCount;
        gm.tentacleCount += ResourcesCount;
        gm.woodCount += ResourcesCount;
        gm.RefreshResources();
    }
}
