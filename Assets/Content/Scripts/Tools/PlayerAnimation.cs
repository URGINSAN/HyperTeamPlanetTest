using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator Anim;
    public string State;

    public void PlayAnim(string state)
    {
        if (State.Equals(state))
            return;

        State = state;

        switch (state)
        {
            case "idle":
                Anim.Play("Idle");
                break;
            case "move":
                Anim.Play("Run");
                break;
            case "dance":
                Anim.Play("Dance");
                break;
        }
    }
}
