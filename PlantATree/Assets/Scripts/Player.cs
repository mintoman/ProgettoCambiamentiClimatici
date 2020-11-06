using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioSource collect;

    void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.name.StartsWith("Garbage"))
        {
            // Get longer in next Move call
            //collectedGarbage = true;
            //print("Yes!!!");

            //garbageManager.RemoveFromList(position);

            MusicManager.Instance.PlaySingle(collect.clip);
            coll.gameObject.SetActive(false);
            // Remove the Garbage
            //Destroy(coll.gameObject);
        }
    }
}
