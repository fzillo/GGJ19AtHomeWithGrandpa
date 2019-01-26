using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifemeter : MonoBehaviour
{

    SpriteRenderer myRenderer;

    public Sprite life1;
    public Sprite life2;
    public Sprite life3;
    public Sprite life4;
    public Sprite life5;

    [Range(1, 5)]
    public int liveValueAtStart = 5;

    [Range(0, 6)]
    private int currentLiveValue = 5;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        currentLiveValue = liveValueAtStart;
        EvaluateLife();
    }

    void EvaluateLife()
    {
        switch (currentLiveValue)
        {
            case 0:
                //End game!
                Debug.Log("END");
                break;
            case 1:
                myRenderer.sprite = life1;
                break;
            case 2:
                myRenderer.sprite = life2;
                break;
            case 3:
                myRenderer.sprite = life3;
                break;
            case 4:
                myRenderer.sprite = life4;
                break;
            case 5:
                myRenderer.sprite = life5;
                break;
            case 6:
                //End game!
                Debug.Log("END");
                break;
            default:
                break;
        }
    }

    int DecreaseLife(int value)
    {
        currentLiveValue -= value;
        if (currentLiveValue < 0)
        {
            currentLiveValue = 0;
        }
        EvaluateLife();
        return currentLiveValue;
    }

    int IncreaseLife(int value)
    {
        currentLiveValue += value;
        if (currentLiveValue > 6)
        {
            currentLiveValue = 6;
        }
        EvaluateLife();
        return currentLiveValue;
    }
}