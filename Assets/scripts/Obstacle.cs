using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer m_TargetRenderer;
    bool SlideDetection = false;
    public bool SlideLine { get { return SlideDetection; } }

    void Start()
    {
        m_TargetRenderer = this.GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SlideDetection = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        SlideDetection = false;
    }
}
