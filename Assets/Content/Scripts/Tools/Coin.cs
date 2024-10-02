using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject Part;

    public void OnCollect()
    {
        SceneController.instance.AddCoin();
        GameObject go = Instantiate(Part, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
