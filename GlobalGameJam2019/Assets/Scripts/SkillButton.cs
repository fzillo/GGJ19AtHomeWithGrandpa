using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    internal ConversationController.Skill skill;

    public List<Sprite> sprites;

    // Start is called before the first frame update
    public void setSkill(ConversationController.Skill skill)
    {
        Button button = GetComponent<Button>();
        this.skill = skill;
        button.transform.GetChild(0).GetComponent<Text>().text = skill.name;
        setAge(0);
    }
    public void setCallback(UnityAction func)
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(func);
    }

    public void setAge(float v)
    {
      //  Debug.Log(v);
        GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,1f-v);
    }

    public void setButtonIndex(int i)
    {
        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = sprites[i%sprites.Count];
    }
}