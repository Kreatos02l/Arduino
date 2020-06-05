using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class CharacterControl : MonoBehaviour {

    public Canvas Warning;
    public Canvas GyroWarning;

    SerialPort stream = new SerialPort("COM7", 9600);

    public float feeth;
    public float vel = 0;
    public float animvel = 0;
    public bool forJump = false;
    public bool forMove = true;


    public GameObject target; 
    float gyro_normalizer_factor = 1.0f / 32768.0f;   


    float curr_angle_y = 0;
    float curr_angle_z = 0;

    int ct = 0;
    float curr_offset_x = 0;
    float curr_offset_y = 0;
    float curr_offset_z = 0;


    public float factor = 7;
    public float distance;

    public bool enableRotation;
    public bool enableTranslation;

    void Start()
    {
        Warning.enabled = false;
        GyroWarning.enabled = false;
        stream.Open();
        stream.ReadTimeout = 20;
    }

    void Update () {
        
        m_animator.SetBool("Grounded", m_isGrounded);

        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, vel);

        switch (m_controlMode)
        {

            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
    }

    private void TankUpdate()
    {
        string dataString = "null received";

        if (stream.IsOpen)
        {

            try
            {

                dataString = stream.ReadLine();
                Debug.Log("RCV_ : " + dataString);
                

            }
            catch (System.IO.IOException ioe)
            {
                Debug.Log("IOException: " + ioe.Message);
            }
        }
        else
            dataString = "NOT OPEN";

        if (!dataString.Equals("NOT OPEN"))
        {
            char splitChar = ';';
            string[] dataRaw = dataString.Split(splitChar);


            float gx = Int32.Parse(dataRaw[0]) * gyro_normalizer_factor;
            float gy = Int32.Parse(dataRaw[1]) * gyro_normalizer_factor;
            float gz = Int32.Parse(dataRaw[2]) * gyro_normalizer_factor;

            if (Mathf.Abs(gy) < 0.1f)
                gy = 0f;


            curr_angle_y += gy;


           
            target.transform.position = new Vector3(transform.position.x, transform.position.y + 3.05f, transform.position.z - 4);
            if (enableRotation)
            target.transform.rotation = Quaternion.Euler(20.0f, 0, curr_angle_y * factor);
            if(curr_angle_y > 1 || curr_angle_y < -1)
                GyroWarning.enabled = true;
            else
                GyroWarning.enabled = false;





            float data = stream.ReadByte();
            Debug.Log("DATA: " + data);
            feeth = data;
            if (!forJump)
            {
                if (feeth > 10f && forMove && feeth < 20f)
                {
                    
                    vel = 3;
                    StartCoroutine(stopForMove());
                    forMove = true;
                    animvel = 1f;
                    Score.score += 1;
                }


                if (!forMove)
                    forMove = true;

                Debug.Log(data);
            }
            else
            {
                if (feeth > 20f)
                {
                    Warning.enabled = false;
                    
                    vel = 3;
                    JumpingAndLanding();
                    animvel = 1f;
                    StartCoroutine(triggerJump());
                    Score.score += 2;
                }
                else 
                {
                    Warning.enabled = true;
                }
            }
        }

        m_animator.SetFloat("MoveSpeed", animvel);

    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "JumpTrigger")
        {
            vel = 0;
            animvel = 0;
     
            forMove = false;
            forJump = true;
        }
    }

    IEnumerator triggerJump()
    {
        Debug.Log(" " + vel);
        yield return new WaitForSeconds(1f);
        vel = 0;
        animvel = 0;
        forMove = false;
        forJump = false;
    }
    IEnumerator stopForMove()
    {
        yield return new WaitForSeconds(1f);
        vel = 0;
        animvel = 0;
        forMove = false;
    }
    
   

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded)
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }

    private enum ControlMode
    {
        Tank,
        Direct
    }

    [SerializeField]
    private float m_moveSpeed = 2;
    [SerializeField]
    private float m_turnSpeed = 200;
    [SerializeField]
    private float m_jumpForce = 0.5f;
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private Rigidbody m_rigidBody;

    [SerializeField]
    private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

}
