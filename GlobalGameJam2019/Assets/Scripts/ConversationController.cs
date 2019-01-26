using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationController : MonoBehaviour
{
    public static ConversationController instance;
    private List<Skill> availableSkills = new List<Skill> {
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
    // Update is called once per frame
    void Awake()
    {
        ConversationController.instance = this;
    }

    // allways returns an array length 4
    // probably will get a better implementation later
    // angryLevel: value from 0 to 1
    public Skill getNextSkill(float playerAngryLevel)
    {
        while(true)
        {
            var skill =  availableSkills[Random.Range(0,availableSkills.Count)];
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
            var attk =  availableAttacks[Random.Range(0,availableAttacks.Count)];
            if(attk.neededCalmness < 1f - enemyAngryLevel)
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
        }
        public int gegnerEinFluss;
        public int eigenEinFluss;
        public string audioFileName;
        public float neededCalmness;
        public float timeUntilDisappearSec = 0f;

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
