using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator animator;
    bool active = false;
    public Arm arm;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active && Input.GetKeyDown(KeyCode.K))
        {
            ActivateAnimCry();
            return;
        }
        if (!active && Input.GetKeyDown(KeyCode.L))
        {
            ActivateAnimArms();
            return;
        }
    }


    void ActivateAnimArms()
    {
        StartCoroutine(AnimArms());
    }

    void ActivateAnimCry()
    {
        StartCoroutine(AnimCry());
    }

    IEnumerator AnimArms()
    {
        active = true;
        animator.SetTrigger("ArmAttack");
        yield return new WaitForSeconds(1);
        arm.ActivateArm();
        yield return new WaitForSeconds(2);
        animator.SetTrigger("ArmAttackOut");
        active = false;
    }

    IEnumerator AnimCry()
    {
        active = true;
        animator.SetTrigger("CryAttack");
        yield return new WaitForSeconds(4);
        animator.SetTrigger("CryAttackOut");
        active = false;

    }
}
