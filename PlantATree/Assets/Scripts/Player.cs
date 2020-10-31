using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.name.StartsWith("Garbage"))
        {
            // Get longer in next Move call
            //collectedGarbage = true;
            //print("Yes!!!");

            //garbageManager.RemoveFromList(position);

            coll.gameObject.SetActive(false);
            // Remove the Garbage
            //Destroy(coll.gameObject);
        }
    }
}
