using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEntity : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer m_TargetRenderer;

     void Start()
    {
        m_TargetRenderer = this.GetComponent<SpriteRenderer>();
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        m_TargetRenderer.color = Color.red;   
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_TargetRenderer.color = Color.white;
    }
}
