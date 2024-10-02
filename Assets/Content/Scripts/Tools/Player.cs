using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Vector3> MovePoses;
    public float MoveSpeed;
    private bool CanMove;
    public GameObject FinishZone;
    [Space]
    public PlayerAnimation Anim;
    Coroutine MoveIE;
    public bool CanFinish;

    public void StopMove()
    {
        StopAllCoroutines();
    }

    public void Move(List<Vector3> poses)
    {
        if (poses.Count > 0)
            transform.position = poses[0];
        MovePoses = poses;

        Anim.PlayAnim("move");
        StartCoroutine(MoveObject());

        SceneController.instance.OnStartMove();
    }

    public void Replay()
    {
        StopMove();

        if (MovePoses.Count > 0)
        {
            print("replay");
            transform.position = MovePoses[0];

            Anim.PlayAnim("move");
            StartCoroutine(MoveObject());
        }
    }

    IEnumerator MoveObject()
    {
        for (int i = 0; i < MovePoses.Count; i++)
        {
            MoveIE = StartCoroutine(Moving(i));
            yield return MoveIE;

            if (i == MovePoses.Count - 1)
            {
                OnEndMove();
            }
        }
    }

    IEnumerator Moving(int currentPosition)
    {
        while (transform.position != MovePoses[currentPosition])
        {
            transform.position = Vector3.MoveTowards(transform.position, MovePoses[currentPosition], MoveSpeed * Time.deltaTime);
 
            Vector3 lookVector = MovePoses[currentPosition] - transform.position;
            lookVector.y = transform.position.y;
            Quaternion rot = Quaternion.LookRotation(lookVector);
            rot.x = 0;
            rot.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5 * Time.deltaTime);

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Coin"))
        {
            other.gameObject.GetComponent<Coin>().OnCollect();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(FinishZone) && CanFinish)
        {
            OnFinish();
            other.gameObject.GetComponent<Finish>().OnFinish();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(FinishZone))
        {
            SceneController.instance.FinishedPlayers--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Player>().OnFall();
            OnFall();
        }
    }

    public void OnFall()
    {
        Anim.PlayAnim("fall");
        StopMove();
        CanFinish = false;
        SceneController.instance.OnLose();
    }

    void OnEndMove()
    {
        Anim.PlayAnim("idle");
        CanFinish = true;
    }

    void OnFinish()
    {
        SceneController.instance.OnLevelFinish(this);

        Anim.PlayAnim("dance");
        CanFinish = false;
    }
}
