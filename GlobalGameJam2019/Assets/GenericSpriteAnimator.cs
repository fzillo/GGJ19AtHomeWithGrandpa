using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpriteAnimator : MonoBehaviour
{

    public List<Sprite> sprites;
    public float FPS;
    private int index = 0;

    private float lastSwitch = 0;


    // Update is called once per frame
    void Update()
    {
        if(Time.time-lastSwitch > FPS)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[index%sprites.Count];
            index++;
            lastSwitch = Time.time;
        }
    }
}
