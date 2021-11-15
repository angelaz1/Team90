using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3SquirrelController : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Pose()
    {
        anim.SetTrigger("Pose");
    }

    public void CorrectPoses()
    {
        anim.SetTrigger("Correct");
    }
    
    public void IncorrectPoses()
    {
        anim.SetTrigger("Incorrect");
    }

    public void Success()
    {
        anim.SetBool("Win", true);
    }
}
