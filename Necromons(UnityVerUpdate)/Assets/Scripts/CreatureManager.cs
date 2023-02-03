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

    bool forestAdd = false;
    bool oceanAdd = false;
    bool vulcanoAdd = false;

    void Start()
    {
        creaturesToLoad = Resources.LoadAll("StarterImages", typeof(Sprite));
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
        /* creatures.Add(new Creature("Name", "Type", HP, Feather, Stone, Wood, Tentacle, Ash)); (Default Example) */

        allCreatures.Add(new Creature("Greath", "Wind", 7, 4, 1, 1, 0, 2));
        allCreatures.Add(new Creature("Tribat", "Wind", 5, 3, 0, 2, 1, 4));
        allCreatures.Add(new Creature("Deathstar", "Wind", 8, 6, 0, 1, 0, 2));
        allCreatures.Add(new Creature("Siamcloud", "Wind", 5, 3, 0, 0, 1, 4));


        allCreatures.Add(new Creature("Skullington", "Stone", 8, 2, 5, 1, 1, 0));
        allCreatures.Add(new Creature("Cafear", "Stone", 5, 2, 3, 2, 1, 0));
        allCreatures.Add(new Creature("Flashnight", "Stone", 5, 1, 3, 1, 2, 0));


        allCreatures.Sort();
        
    }

    public void RandomizedNumber()
    {
        rnd = Random.Range(0, allCreatures.Count);
    }

    public void CreatureSpawn()
    {
        Debug.Log("Spawning");
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
        Debug.Log("Refreshing");
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
        RefreshCreatureCard();
        yield return new WaitForSeconds(1.0f);
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
            //RefreshCreatureCard();
            readyToSpawn = false;
            StartCoroutine(TimeToSpawn());
        }
    }

    public void CreatureReKill() {

        currentCreature = noCreature;
        currentCreatureImage = emptySprite;
        //RefreshCreatureCard();
        readyToSpawn = false; 
        StartCoroutine(TimeToSpawn());

    }

    private void Update()
    {

        /* creatures.Add(new Creature("Name", "Type", HP, Feather, Stone, Wood, Tentacle, Ash)); (Default Example) */

        if (Input.GetKey(KeyCode.Space))
        {
            GainResources(10);
        }

        if(gm.level == 3 && forestAdd == false) {

        allCreatures.Add(new Creature("Werefangs", "Nature", 6, 2, 2, 4, 0, 0));
        allCreatures.Add(new Creature("Plaguestag", "Nature", 3, 1, 2, 3, 0, 0));
        allCreatures.Add(new Creature("Nox", "Nature", 3, 0, 2, 3, 1, 0));
        
        creaturesToLoad = null;
        creaturesToLoad = Resources.LoadAll("IntermediateImages", typeof(Sprite));

        forestAdd = true;

        allCreatures.Sort();

        }

        if(gm.level == 5 && oceanAdd == false && vulcanoAdd == false) {

        allCreatures.Add(new Creature("Boneknapper", "Water", 8, 2, 0, 4, 4, 0));
        allCreatures.Add(new Creature("Keplon", "Water", 7, 4, 0, 2, 3, 0));
        allCreatures.Add(new Creature("Plattypearl", "Water", 5, 3, 0, 1, 3, 0));
        allCreatures.Add(new Creature("Poisick", "Water", 3, 1, 0, 1, 3, 0));

        allCreatures.Add(new Creature("Frepard", "Fire", 5, 0, 2, 0, 3, 2));
        allCreatures.Add(new Creature("Scauldron", "Fire", 7, 0, 4, 1, 2, 3));
        allCreatures.Add(new Creature("Zkill", "Fire", 8, 0, 4, 1, 3, 4));
        allCreatures.Add(new Creature("Phex", "Fire", 6, 0, 2, 1, 4, 3));

        creaturesToLoad = null;
        creaturesToLoad = Resources.LoadAll("HardImages", typeof(Sprite));

        oceanAdd = true;

        vulcanoAdd = true;

        allCreatures.Sort();


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
