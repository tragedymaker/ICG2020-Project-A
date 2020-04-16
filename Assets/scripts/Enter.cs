using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enter : MonoBehaviour
{

    bool EnterDetection = false;
    public bool YellowLine { get { return EnterDetection; } }

    void OnTriggerEnter2D(Collider2D other)
    {
        //GameObject.Destroy(this.gameObject);
        EnterDetection = true;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        EnterDetection = false;
    }



}

