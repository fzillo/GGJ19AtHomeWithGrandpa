using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConversationController : MonoBehaviour
{
    public static ConversationController instance;
    private PlayerController2 player;
    public GameObject enemy;

    public GameObject buttonContainer;
    public GameObject buttonPrefab;

    private void someFunc(){}
    private List<Skill> availableSkills = new List<Skill> {
        // TODO: animationen:
        /*
        - shouting
        - shield
        - question thing (either speech bubble or facial expression) 
         */
        new Skill("Gegenbeleidigung",-1,1,"", 0f),
        new Skill("Weggehen",-1,0,"",0f),
        new Skill("Distance",0,1,"",0.5f),
        new Skill("Entschuldigen",1,1,"",0.7f),
        new Skill("Question",1,1,"",0.7f),
        new Skill("MeinungGeigen",0,1,"",0.2f),
        new Skill("PeriodLine",-1,0,"",0f),
    };

    // TODO: these need to be sorted
    private List<Attack> availableAttacks = new List<Attack> {
        new Attack("Tränenschwall","",0f),
        new Attack("Redeschwall","",0f),
        new Attack("Greifende Arme","",0f)
    };

    private Skill[] currentSkills = new Skill[3];

    // Update is called once per frame
    void Awake()
    {
        ConversationController.instance = this;
    }

    void callSkill(Skill skill)
    {
        // TODO: call the effect here
        Debug.Log("skill called "+skill.name);
        
        switch(skill.name)
        {
            case "Entschuldigen":
            case "MeinungGeigen":
            case "Question":
                player.effectShield();
                break;
            case "Gegenbeleidigung":
                player.effectShout();
                break;
            default:
                break;

        }
        if(skill.eigenEinFluss < 0) player.lifemeterInstance.DecreaseLife(1);
        if(skill.eigenEinFluss > 0) player.lifemeterInstance.IncreaseLife(1);

        removeSkill(skill);
        removeSkillButton(skill);
    }
    void spawnNewSkillButton(Skill skill)
    {
        var newButton = Instantiate(buttonPrefab);
        SkillButton skillButton = newButton.GetComponent<SkillButton>();
        skillButton.setSkill(skill);
        skillButton.setCallback(() => callSkill(skill));
        newButton.transform.SetParent(buttonContainer.transform);
        newButton.transform.localScale = new Vector3(1,1,1);
        skillButton.setButtonIndex(System.Array.IndexOf(currentSkills, skill));
        skillButton.transform.position = new Vector3(skillButton.transform.position.x, skillButton.transform.position.y, -5);

    }

    void removeSkillButton(Skill skill)
    {

        GameObject deleteme = null;
        foreach(Transform child in buttonContainer.transform)
        {
            if(child.GetComponent<SkillButton>().skill ==skill)
            {
                deleteme = child.gameObject;
            }
        }
        if(deleteme != null) 
        {
            Destroy(deleteme);

            Debug.Log("Button was removed "+skill.name);
        }
    }

    private void removeSkill(Skill skill)
    {
        for(int i = 0; i < currentSkills.Length; i++)
        {
            if (currentSkills[i] == skill) 
            {
                currentSkills[i] = null;
                Debug.Log("skill "+skill.name+" was removed");
            }
        }
    }

    void Update()
    {
        if(PlayerController2.instance != null) player = PlayerController2.instance;

        // set next skill
        for(int i = 0; i < currentSkills.Length; i++)
        {
            if(currentSkills[i] == null)
            {
                currentSkills[i] = getNextSkill(player.getAngryScore());
                currentSkills[i].startedTime = Time.time;
                spawnNewSkillButton(currentSkills[i]);
            }
        }
        // check which skills are depleated
        for(int i = 0; i < currentSkills.Length; i++)
        {   
            var skill = currentSkills[i];

            if(skill.timeUntilDisappearSec * 0.5 < Time.time - skill.startedTime)
            {
                setSkillViewAge(skill);
            }

            if(skill.timeUntilDisappearSec < Time.time - skill.startedTime)
            {
                removeSkill(skill);
                removeSkillButton(skill);
            }
        }
    }
    
    void setSkillViewAge(Skill skill)
    {
        foreach(Transform child in buttonContainer.transform)
        {
            if(child.GetComponent<SkillButton>().skill ==skill)
            {
                var x = ((Time.time - skill.startedTime)-(skill.timeUntilDisappearSec * 0.5f)) / (skill.timeUntilDisappearSec * 0.5f);
                child.GetComponent<SkillButton>().setAge(x);
            }
        }
    }

    // allways returns an array length 4
    // probably will get a better implementation later
    // angryLevel: value from 0 to 1
    public Skill getNextSkill(float playerAngryLevel)
    {
        while(true)
        {
            var skill =  availableSkills[UnityEngine.Random.Range(0,availableSkills.Count)];
            if(skill.neededCalmness < 1f - playerAngryLevel)
            {
                var newSkill = skill.Clone();
                newSkill.timeUntilDisappearSec = 5f;
                return newSkill;
            }
        }
    }

    public Attack getAnAttack(float enemyAngryLevel)
    {
        while(true)
        {
            var attk =  availableAttacks[UnityEngine.Random.Range(0,availableAttacks.Count)];
            if(attk.neededAngryNes < 1f - enemyAngryLevel)
            {
                var newAttk = attk.Clone();
                return newAttk;
            }
        }
    }


    public class Skill
    {
        public Skill(string name, int gegnerEinFluss, int eigenEinFluss, string audioFileName, float neededCalmness)
        {
            this.gegnerEinFluss = gegnerEinFluss;
            this.eigenEinFluss = eigenEinFluss;
            this.audioFileName = audioFileName;
            this.neededCalmness = neededCalmness;
            this.name = name;
        }
        public string name;
        public int gegnerEinFluss;
        public int eigenEinFluss;
        public string audioFileName;
        public float neededCalmness;
        public float timeUntilDisappearSec = 0f;
        public float startedTime = 0;

        // https://stackoverflow.com/questions/6569486/creating-a-copy-of-an-object-in-c-sharp
        public Skill Clone()
        {
            return (Skill) this.MemberwiseClone();
        }
    }

    public class Attack
    {
        public Attack(string name, string audioFileName, float neededAngryNes)
        {
            this.audioFileName = audioFileName;
            this.neededAngryNes = neededAngryNes;
        }
        // TODO: add the spawned pattern here
        public string audioFileName;
        public float neededAngryNes;

        // https://stackoverflow.com/questions/6569486/creating-a-copy-of-an-object-in-c-sharp
        public Attack Clone()
        {
            return (Attack) this.MemberwiseClone();
        }
    }

}
