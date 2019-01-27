using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = -1;
    public Transform tf;
    public AudioManager audioM;

    void Start()
    {
        audioM = FindObjectOfType<AudioManager>();
        if (audioM != null)
        {
            audioM.Play("PloppHigh");
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Boundary" || coll.tag == "Bullet")
        {
            return;
        }
        if (audioM != null)
        {
            audioM.Play("PloppLow");
        }
        if (coll.tag == "Player")
        {
            PlayerController2 pc = coll.GetComponent<PlayerController2>();
            if (pc != null)
            {
                Lifemeter lm = pc.lifemeterInstance;

                if (lm != null)
                {
                    lm.DecreaseLife(1);
                }
            }
        }
        Destroy(gameObject);
        Debug.Log(coll);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Boundary")
        {
            Destroy(gameObject);
            Debug.Log(coll);
        }
    }
}
