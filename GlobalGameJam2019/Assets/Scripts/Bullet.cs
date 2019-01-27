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
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.tag)
        {
            case "Boundary":
            case "Bullet":
                return;
            case "Player":
                if (audioM != null)
                    PlaySound();

                PlayerController2 pc = coll.GetComponent<PlayerController2>();
                if (pc != null)
                {
                    Lifemeter lm = pc.lifemeterInstance;
                    if (lm != null && !pc.shieldActive)
                        lm.DecreaseLife(1);
                }

                break;
        }

        Destroy(gameObject);
        Debug.Log(coll);
    }

    private string[] _playerHitSounds = new string[] { "player_hit", "player_hit2", "player_hit3", "player_hit4" };
    private string[] _playerHurtSounds = new string[] { "player_hurt", "player_hurt2", "player_hurt3", "player_hurt4" };
    private void PlaySound()
    {
        audioM.Play(_playerHitSounds[(int)Mathf.Floor(Random.value * (_playerHitSounds.Length - 1))]);
        audioM.PlayPitchRandom(_playerHurtSounds[(int)Mathf.Floor(Random.value * (_playerHurtSounds.Length - 1))], 0.5f);
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
