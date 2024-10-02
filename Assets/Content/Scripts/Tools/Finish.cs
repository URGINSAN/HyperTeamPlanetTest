using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public Animator Anim;
    public static Finish instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OnFinish()
    {
        Anim.Play("Finish");
    }
}
