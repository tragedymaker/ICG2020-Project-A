using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    bool Hole= false;
    public bool Thehole { get { return Hole; } }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject.Destroy(this.gameObject);
        Hole = true;
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        Hole = false;
    }
}
