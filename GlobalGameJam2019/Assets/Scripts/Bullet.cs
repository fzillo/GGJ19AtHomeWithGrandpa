using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public Rigidbody2D rb2D;

    public AudioManager audioM;

    void Start()
    {
        audioM = FindObjectOfType<AudioManager>();
        audioM.Play("PloppHigh");
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Boundary" || coll.tag == "Bullet")
        {
            return;
        }
        Destroy(gameObject);
        audioM.Play("PloppLow");
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
