using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class SimpleMovement : MonoBehaviour
{
    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    public float JumpSpeed = 1.0f;
    private float curSpeed = 0;
    public GameObject jack = GameObject.Find("Lumberjack2");
    public Text healthText;
    private int health;
    public Text scoreText;
    private int score;
    public float invulnerabilityTime;
    bool invulnerable = false;
    bool jumpingUp = false;
    private float jumpStartY;
    public float jumpForce;
    private float maxHeight;
    private void Start()
    {
        health = 100;
        score = 0;
    }
    void Update()
    {
        
        CharacterController controller = GetComponent<CharacterController>();

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.left);
        float strafingspeed = Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.Q))
        {
            
            //controller.SimpleMove(right * curSpeed);
            // transform.Translate(new Vector3(1, 0, 0), Space.World);
            controller.Move(-transform.right*strafingspeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            //controller.SimpleMove(right * curSpeed);
            // transform.Translate(new Vector3(1, 0, 0), Space.World);
            controller.Move(transform.right*strafingspeed);
        }
        float cS = speed * Input.GetAxis("Vertical");
        curSpeed = cS;
        ChangeAnimation();
        controller.SimpleMove(forward * curSpeed);
       
        if (Input.GetKey(KeyCode.Space))
        {
            if(controller.isGrounded)
            {
                jumpStartY = transform.position.y;
                maxHeight = jumpStartY + jumpForce;
                jumpingUp = true;
            }
            if (jumpingUp == true)
            {
                if (transform.position.y < maxHeight)
                {
                    print("ded");
                    Jump();
                    print(transform.position.y);
                }
                else
                {
                    jumpingUp = false;
                }

            }
        }
        
    }
    void Jump()
    {
        Rigidbody rb = jack.GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.up * 123124123);
        //transform.Translate(Vector3.up * JumpSpeed * Time.deltaTime, Space.World);
        
        transform.Translate(0, JumpSpeed*Time.deltaTime*0.1f, 0, Space.World);
    }
   public void Damage(int newHealth)
    {
        StartCoroutine(DamageEnum(newHealth));
    }
    public void NoDamage(int newHealth)
    {
        TakeDamage(newHealth);
    }
    public IEnumerator DamageEnum(int newHealth)
    {
        
        if (invulnerable == false&&health>0)
        {
            health -= newHealth;
            UpdateHealth();
            if(health<=0)
            {
                Application.LoadLevel("Main Menu");
            }
            else
            invulnerable = true;
            yield return new WaitForSeconds(0.1f*invulnerabilityTime);
            invulnerable = false;
            
        }

    }
    public void TakeDamage(int newHealth)
    {
        health -= newHealth;
            UpdateHealth();
        if(health<=0)
            {
                Application.LoadLevel("Main Menu");
            }
    }
    void UpdateHealth()
    {
        healthText.text = "HP: " + health;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    void ChangeAnimation()
    {
        if (!jack.GetComponent<Animation>().IsPlaying("Lumbering"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Animation>().wrapMode = WrapMode.Once;
                jack.GetComponent<Animation>().Play("Lumbering");

            }
            else
            {
                if (curSpeed != 0)
                {
                    jack.GetComponent<Animation>().Play("Walk");
                }
                if (curSpeed == 0)
                {
                    jack.GetComponent<Animation>().Play("Idle");
                }
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        
    }
}