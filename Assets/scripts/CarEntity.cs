using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CarEntity : MonoBehaviour
{
    public Obstacle Line;
    public Enter Enter;
    public Enter Center;
    public BlackHole blackhole;
    public Obstacle BackLine;
    public Enter BackEnter;
    public Enter BackCenter;

    public GameObject wheelFrontRight;
    public GameObject wheelFrontLeft;
    public GameObject wheelBackRight;
    public GameObject wheelBackLeft;
    int coin_to_pass;
    int coins=0;
    public GameObject Block;

    //------car steering---------//
    float m_FrontWheelAngle = 0;
    const float WHEEL_ANGLE_LIMIT = 35f;
    public float turnAngularVelocity = 10f;
    public float Augulardeceleration = 20f;
    public float m_Velocity;
    //public float Velocity{ get { return m_Velocity; } }
  

    const float carLength = 1.02f;

    //-------Accelerate and decelerate------------//

    public float acceleration = 3f;
    public float autodeceleration = 3f;
    public float deceleration = 20f;
    public float maxVelocity = 12f;
    public float minVelocity = -12f;

    //-----Car control boolean-------------//
    bool a = false;
    bool b = false;
    bool c = false;
    bool d = false;
    float m_DeltaMovement;
    bool first_lot = false;
    bool choose = false;

    //----update wheels by m_FrontWheelAngle----//
    void UpdateWheels()
    {
        Vector3 localEulerAngles = new Vector3(0f, 0f, m_FrontWheelAngle);
        wheelFrontLeft.transform.localEulerAngles = localEulerAngles;
        wheelFrontRight.transform.localEulerAngles = localEulerAngles;
    }

    
    void Start()
    {
        Debug.Log("Choose the difficulty!\n"+"Press 'H' for hard , 'M' for medium , 'E' for ez\n");
    }
    void FixedUpdate()
    {
        //----Starting Version chosen----------//
        if (Input.GetKeyDown(KeyCode.H) & !choose)
        {
            coin_to_pass = 12;
            Debug.Log("You've chosen the hard version. Plz eat 12 coins in order to finish the game.");
            choose = true;
        }
            
        if (Input.GetKeyDown(KeyCode.M) & !choose)
        {
            coin_to_pass = 10;
            Debug.Log("You've chosen the medium version. Plz eat 10 coins in order to finish the game.");
            choose = true;
        }
            
        if (Input.GetKeyDown(KeyCode.E) & !choose)
        {
            coin_to_pass = 8;
            Debug.Log("You've chosen the ez version. Plz eat 8 coins in order to finish the game.");
            choose = true;
        }
            
          //-------speed up-------//
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_Velocity = Mathf.Min(maxVelocity, m_Velocity + Time.fixedDeltaTime * acceleration);
            a = false;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
            a = true;
        //-------break-----------//
        if (a)
        {
            m_Velocity = Mathf.Max(0, m_Velocity - Time.fixedDeltaTime * autodeceleration);
            if (m_Velocity == 0)
                a = false;
        }
         //-------slowing down-----//
        if (Input.GetKey(KeyCode.Z))
        {
            if (m_Velocity > 0)
            {
                m_Velocity = Mathf.Max(0, m_Velocity - Time.fixedDeltaTime * deceleration);

                if (m_Velocity == 0)
                    a = false;
            }
            else
            {
                m_Velocity = Mathf.Min(0, m_Velocity + Time.fixedDeltaTime * deceleration);
                if (m_Velocity == 0)
                    b = false;
            }
        }
          //-------turn left-------//
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_FrontWheelAngle = Mathf.Clamp(m_FrontWheelAngle + Time.fixedDeltaTime * turnAngularVelocity, -WHEEL_ANGLE_LIMIT, WHEEL_ANGLE_LIMIT);
            UpdateWheels();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            c = true;
        if (c)
        {
            m_FrontWheelAngle = Mathf.Max(0, m_FrontWheelAngle - Time.fixedDeltaTime * Augulardeceleration);
            UpdateWheels();
            if (m_FrontWheelAngle == 0)
                c = false;
        }
          //-------turn right-------//

        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_FrontWheelAngle = Mathf.Clamp(m_FrontWheelAngle - Time.fixedDeltaTime * turnAngularVelocity, -WHEEL_ANGLE_LIMIT, WHEEL_ANGLE_LIMIT);
            UpdateWheels();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
            d = true;
        if (d)
        {
            m_FrontWheelAngle = Mathf.Min(0, m_FrontWheelAngle + Time.fixedDeltaTime * Augulardeceleration);
            UpdateWheels();
            if (m_FrontWheelAngle == 0)
                d = false;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_Velocity = Mathf.Max(minVelocity, m_Velocity - Time.fixedDeltaTime * acceleration);
            b = false;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
            b = true;
        if (b)
        {
            m_Velocity = Mathf.Min(0, m_Velocity + Time.fixedDeltaTime * autodeceleration);
            if (m_Velocity == 0)
                b = false;
        }
        m_DeltaMovement = m_Velocity * Time.fixedDeltaTime;

        this.transform.Rotate(0f, 0f, 1 / carLength * Mathf.Tan(Mathf.Deg2Rad * m_FrontWheelAngle) * m_DeltaMovement * Mathf.Rad2Deg);
        this.transform.Translate(Vector3.right * m_DeltaMovement);
    }


    //------保持顏色/改顏色/停止/完成任務-----//
    [SerializeField] SpriteRenderer[] m_Renderders = new SpriteRenderer[5];

    void ResetColor()
    {
        ChangeColor(Color.white);
    }

    public void ChangeColor(Color color)
    {
        foreach (SpriteRenderer r in m_Renderders)
        {
            r.color = color;
        }
    }
    void Stop()
    {
       
            m_Velocity = 0;
    }

    void Complete()
    {
           ChangeColor(Color.gray);
        if(!first_lot)
            Debug.Log("Successfully parallel parking.");

        first_lot = true;
        if (coins >= coin_to_pass)
        {
            Destroy(Block.gameObject);
        }

    }

    //-----------------進入碰撞--------------------//

    void OnCollisionEnter2D(Collision2D collision)
    {
        //----parkinglot 邊線判斷----//
        if (Line.SlideLine)
        {
            Stop();
            ChangeColor(Color.red);
        }
        if(BackLine.SlideLine)
        {
            Stop();
            ChangeColor(Color.red);
        }

        //-------黑洞判斷-----------//
        if (blackhole.Thehole)
        {
            Destroy(this.gameObject);
            UnityEditor.EditorApplication.isPlaying = false;
        }
       //-------其餘物品進入碰撞後反應----//     
        Stop();
        ChangeColor(Color.red);
        this.Invoke("ResetColor", 2f);

    }

    //-----------------離開碰撞--------------------//
    void OnCollisionExit2D(Collision2D collision)
    {
        ResetColor();
    }

    //-----------------進入trigger--------------------//
    void OnTriggerEnter2D(Collider2D other)
    {

        CheckPoint checkPoint = other.gameObject.GetComponent<CheckPoint>();

        //if (checkPoint != null)
        //{
        if (!BackEnter.YellowLine & !Enter.YellowLine)
        {
            ChangeColor(Color.green);
            this.Invoke("ResetColor", 2f);
            coins++;

            Debug.Log(coins);
            if (coins >= coin_to_pass & first_lot)
            {
                Destroy(Block.gameObject);
            }
        }
    }
    //-----------------停留在trigger--------------------//
    void OnTriggerStay2D(Collider2D other)
    {
        if (!Line.SlideLine & !Enter.YellowLine & Center.YellowLine)
        {
            Complete();
        }

        if(!BackLine.SlideLine & !BackEnter.YellowLine & BackCenter.YellowLine)
        {
            Complete();
            Debug.Log("Successfully reverse parking.");
            Debug.Log("Congrats!");

            UnityEditor.EditorApplication.isPlaying = false;

        }
    }
    //-----------------離開trigger--------------------//
    void OnTriggerExit2D(Collider2D other)
    {
        if (Line.SlideLine | Enter.YellowLine |!Center.YellowLine)
        {
            ResetColor();
        }
    }

    }




