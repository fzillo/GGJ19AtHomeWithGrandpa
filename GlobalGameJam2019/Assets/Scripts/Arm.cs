using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public float maxForward = 10;
    public readonly int initialDirection = -1;
    public int forwardBackwardModifier = 1;
    public float speed = 1;
    public float timeLingering = 1;
    public float yMaxHeight = 3;
    public float yMinHeight = 1;

    [SerializeField] private int counter;

    private float posXBefore;
    [SerializeField] private bool active = false;

    Vector3 originalPos;

    void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        posXBefore = this.transform.position.x;
    }

    void Update()
    {
        if (!active && Input.GetKeyDown(KeyCode.O))
        {
            ActivateArm();
            return;
        }
        if (active)
        {
            counter += (1 * forwardBackwardModifier);
            //Debug.Log(counter);
            if (counter < 0)
            {
                active = false;
                this.transform.position = originalPos;
                counter = 0;
                forwardBackwardModifier *= -1;
                //yield break;
                return;
            }

            //Debug.Log("tfPos " + this.transform.position.x + " posBefore " + posXBefore + " max " + maxForward);
            if (Mathf.Abs(posXBefore - this.transform.position.x) > maxForward)
            {
                //yield return new WaitForSeconds(timeLingering);
                forwardBackwardModifier *= -1;
            }
            Vector3 moveVect = new Vector3(speed * initialDirection * forwardBackwardModifier * Time.deltaTime, 0, 0);
            this.transform.Translate(moveVect, Space.Self);
        }
    }


    public void ActivateArm()
    {
        if (active) return;
        //Debug.Log("Activate Arm!");
        active = true;
        //StartCoroutine(MoveArm());
        float randY = Random.Range(yMinHeight, yMaxHeight);
        this.transform.position = new Vector3(originalPos.x, randY, originalPos.z);
    }
}
