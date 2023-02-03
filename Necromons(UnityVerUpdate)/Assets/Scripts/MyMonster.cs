using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyMonster : MonoBehaviour
{

    public Creature thisCreature;
    GameObject managers;
    public int id;

    public int initialHP;

    private void Awake()
    {
        managers = GameObject.Find("GameManager");
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("MyMonsterImage").GetComponent<Image>().sprite = (Sprite) managers.GetComponent<CreatureManager>().creaturesToLoad[thisCreature.imageIndex];
        transform.Find("MyMonsterHP").GetComponent<TextMeshProUGUI>().text = thisCreature.HP.ToString();
        transform.Find("MyMonsterTyp").GetComponent<TextMeshProUGUI>().text = thisCreature.Type;
        transform.Find("MyMonsterName").GetComponent<TextMeshProUGUI>().text = thisCreature.Name;
        GetComponent<Button>().onClick.AddListener(TaskOnClick);
        initialHP = thisCreature.HP;
    }

    void TaskOnClick()
    {
        managers.GetComponent<GameManager>().SelectRaidMonster(id, gameObject);
    }

    public void SendToRaid()
    {
        StartCoroutine(OnRaid());
    }

    public IEnumerator OnRaid()
    {
        Button btn = GetComponent<Button>();
        Image img = GetComponent<Image>();
        btn.interactable = false;
        img.color = new Color32(123, 57, 57, 255);
        yield return new WaitForSeconds(5.0f);
        btn.interactable = true;
        img.color = new Color32(81, 81, 81, 255);
        Debug.Log(thisCreature.HP);
        transform.Find("MyMonsterHP").GetComponent<TextMeshProUGUI>().text = thisCreature.HP.ToString();
        if(thisCreature.HP <= 0)
        {
            //managers.GetComponent<GameManager>().myCreatures.Remove(thisCreature);
            thisCreature.HP = initialHP;
            GameObject.Destroy(gameObject);
        }
    }
}
