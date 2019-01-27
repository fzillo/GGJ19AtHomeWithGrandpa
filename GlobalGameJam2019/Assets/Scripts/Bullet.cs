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
            audioM.Play("PloppHigh");
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Boundary" || coll.tag == "Bullet")
        {
            return;
        }
        if (audioM != null)
            audioM.Play("PloppLow");
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
