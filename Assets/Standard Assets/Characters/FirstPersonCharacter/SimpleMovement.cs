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
    private float potiony = 0;
    private float curSpeed = 0;
    private float strafingspeed = 0;
    public GameObject camera;
    public GameObject jack = GameObject.Find("Lumberjack2");
    public GameObject projectile;
    public GameObject healthBar;
    private float maxBarLength = 0;
    public Text healthText;
    public Text potionText;
    private float health;
    public float maxHealth;
    public Text scoreText;
    public GameObject menu;
    private int score;
    public float invulnerabilityTime;
    bool invulnerable = false;
    bool jumpingUp = false;
    bool isMenuUp = false;
    private float jumpStartY;
    public float jumpForce;
    private float maxHeight;
    private void Start()
    {
        maxBarLength = healthBar.GetComponent<RectTransform>().rect.width;
        health = maxHealth;
        score = 0;
        UpdateHealth();
        menu.SetActive(false);
        potiony = 0;
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(score>-1233)
            {
                GameObject a = (GameObject)Instantiate(projectile, transform.position + transform.forward * 1 + transform.up * 1, projectile.transform.rotation);
                a.GetComponent<Rigidbody>().AddForce(transform.forward * 20000 + transform.up * 5000);
                score--;
                UpdateScore();
            }
           
        }
        transform.eulerAngles = new Vector3(
                            0,
                            transform.eulerAngles.y,
                            0
                            );
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf==false)
            {
                showMenu();
                isMenuUp = true;
            }
            else
            {
                hideMenu();
                isMenuUp = false;
            }
        }
        CharacterController controller = GetComponent<CharacterController>();
        /*
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.left);
        strafingspeed = 0;
        if(controller.isGrounded)
        {

            if (Input.GetKey(KeyCode.Q))
            {
                strafingspeed = Time.deltaTime * speed;
                controller.Move(-transform.right * strafingspeed);
                
                
            }
            if (Input.GetKey(KeyCode.E))
            {
                strafingspeed = Time.deltaTime * speed;
                controller.Move(transform.right * strafingspeed);
                
            }
        }
        float cS = speed * Input.GetAxis("Vertical");
        curSpeed = cS;
        ChangeAnimation();
        controller.SimpleMove(forward * curSpeed);
        */

        //NOWA WERSJA RUCHU
        curSpeed = 0;
        float rS = speed * Input.GetAxis("Horizontal");
        float cS = speed * Input.GetAxis("Vertical");

        Vector3 forward = transform.TransformDirection(new Vector3(0, 0, Mathf.Abs(cS)));

        if (cS == 0)
        {
            forward = transform.TransformDirection(new Vector3(0, 0, Mathf.Abs(rS)));
        }
        
            curSpeed = rS + cS;
        if(cS==-rS&&cS!=0)
        {
            curSpeed = 1;
        }
        
        controller.SimpleMove(forward);

        if (curSpeed != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(rS, 0, cS))*camera.transform.rotation;
            transform.eulerAngles = new Vector3(
                            0,
                            transform.eulerAngles.y,
                            0
                            );
        }
        ChangeAnimation();
        // KONIEC NOWEJ WERSJI RUCHU

        if (Input.GetKey(KeyCode.Space))
        {
            if(controller.isGrounded)
            {
                jumpStartY = transform.position.y;
                maxHeight = jumpStartY + jumpForce;
                jumpingUp = true;
            }
            
        }
        if (jumpingUp == true)
        {
            if (transform.position.y < maxHeight)
            {
                Jump();

            }
            else
            {
                jumpingUp = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (potiony>0)
            {
                consumePotion();
            }

        }
        
    }
    void showMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }
    void hideMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }
    void Jump()
    {
        Rigidbody rb = jack.GetComponent<Rigidbody>();
        
        transform.Translate(0, JumpSpeed*Time.deltaTime*0.1f, 0, Space.World);
    }
   public void Damage(int newHealth)
    {
        StartCoroutine(DamageEnum(newHealth));
    }
    public void heal(int healing)
   {
        if(health+healing<=maxHealth)
        {
            health += healing;
        }
        else
        {
            health = maxHealth;
        }
       UpdateHealth();
   }
    public void NoDamage(int newHealth)
    {
        TakeDamage(newHealth);
    }
    public void getPotion(int number)
    {
        potiony += number;
        updatePotiony();
    }
    public void updatePotiony()
    {
        potionText.text = "Potiony: " + potiony;
    }
    public void consumePotion()
    {
        potiony -= 1;
        updatePotiony();
        heal(40);
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
        //healthText.text = "HP: " + health;

        float oldX = healthBar.transform.position.x;
        float oldW = healthBar.GetComponent<RectTransform>().rect.width;
        float w = maxBarLength * (health/maxHealth);
        float h=healthBar.GetComponent<RectTransform>().rect.height;
        healthBar.GetComponent<RectTransform>().sizeDelta=new Vector2(w, h);
        healthBar.transform.Translate(new Vector3((-oldW+ w) / 2, 0, 0));
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
            if (Input.GetMouseButtonDown(0)&&isMenuUp==false)
            {
                GetComponent<Animation>().wrapMode = WrapMode.Once;
                jack.GetComponent<Animation>().Play("Lumbering");

            }
            else
            {
                if (curSpeed != 0||strafingspeed!=0)
                {
                    jack.GetComponent<Animation>().Play("Walk");
                }
                if (curSpeed == 0&&strafingspeed==0)
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