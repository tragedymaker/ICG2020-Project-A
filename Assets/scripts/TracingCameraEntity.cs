using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingCameraEntity : MonoBehaviour
{
    //public GameObject targetObject;

    void Update()
    {
        Vector3 deltaPos = targetObject.transform.position - this.transform.position;
        Vector3 position = deltaPos * 0.9f * Time.deltaTime;

        this.transform.position += new Vector3(position.x, position.y, 0);
    }

    public CarEntity targetObject;
    public float MOVING_THRESHOLD = 10f;

    Camera m_Camera;
    float m_OrthographicSize;


    void Start()
    {
        m_Camera = this.GetComponent<Camera>();
        m_OrthographicSize = m_Camera.orthographicSize;
    }

    void LateUpdate()
    {
        Vector2 deltaPos = this.transform.position - targetObject.transform.position;
        m_Camera.orthographicSize = m_OrthographicSize + targetObject.m_Velocity * 0.2f;

        if (deltaPos.magnitude > MOVING_THRESHOLD)
        {
            deltaPos.Normalize();
            Vector2 newPosition = new Vector2(targetObject.transform.position.x, targetObject.transform.position.y) + deltaPos * MOVING_THRESHOLD;
            this.transform.position = new Vector3(newPosition.x, newPosition.y, this.transform.position.z);
        }


    }


}