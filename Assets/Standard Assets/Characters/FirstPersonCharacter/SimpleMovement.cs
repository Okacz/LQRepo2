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
    bool respawned = false;
    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    public float JumpSpeed = 1.0f;
    public float potiony = 0;
    private float curSpeed = 0;
    private float strafingspeed = 0;
    public GameObject camera;
    public GameObject jack = GameObject.Find("Lumberjack2");
    public GameObject projectile;
    public GameObject healthBar;
    public GameObject staminaBar;
    public float maxStamina = 100;
    private float stamina;
    private float maxBarLength = 0;
    public Text healthText;
    public Text potionText;
    private float health;
    public float maxHealth;
    public Text scoreText;
    public GameObject winmenu;
    public GameObject deathmenu;
    public GameObject menu;
    private int score;
    public float invulnerabilityTime;
    bool invulnerable = false;
    bool jumpingUp = false;
    public CameraMovement3 camerascript;
    public bool isMenuUp = false;
    private float jumpStartY;
    public GameObject bossBar;
    public GameObject bossBarBackground;
    public float jumpForce;
    private float maxHeight;
    public GoblinBossController gbc;
    bool staminaCooldown = false;

	public Vector3 Spawnpoint;

    private void Start()
    {

        stamina = maxStamina;
        Cursor.visible = false;
		Spawnpoint = transform.position;
        maxBarLength = healthBar.GetComponent<RectTransform>().rect.width;
        health = maxHealth;
        score = 0;
        UpdateHealth();
        menu.SetActive(false);
        deathmenu.SetActive(false);
        winmenu.SetActive(false);
        potiony = 0;
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(score>0)
            {
                GameObject a = (GameObject)Instantiate(projectile, transform.position + transform.forward * 1 + transform.up * 1, projectile.transform.rotation);
                a.GetComponent<Rigidbody>().AddForce(transform.forward * 20000 + transform.up * 5000);
                score--;
                UpdateScore();
            }
           
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            transform.position=new Vector3(-300, 5.20f, 255);

        }
        if(health!=0)
        {
            transform.eulerAngles = new Vector3(
                            0,
                            transform.eulerAngles.y,
                            0
                            );
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf==false)
            {
                showMenu();
                isMenuUp = true;
                Cursor.visible = true;
            }
            else
            {
                hideMenu();
                isMenuUp = false;
                Cursor.visible = false;
            }
        }

		if (Input.GetKey ("left shift")&&stamina>0) 
		{
				speed = 12.0F;
                stamina = stamina - 2;
                float oldX = staminaBar.transform.position.x;
                float oldW = staminaBar.GetComponent<RectTransform>().rect.width;
                float w = maxBarLength * (stamina / maxStamina);
                float h = staminaBar.GetComponent<RectTransform>().rect.height;
                staminaBar.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
                staminaBar.transform.Translate(new Vector3((-oldW + w) / 2, 0, 0));
                StartCoroutine(StaminaCD());
		}
        else
        {
            speed = 6f;
            if(stamina<100&&staminaCooldown==false)
            {
                stamina = stamina + 0.5f;
                float oldX = staminaBar.transform.position.x;
                float oldW = staminaBar.GetComponent<RectTransform>().rect.width;
                float w = maxBarLength * (stamina / maxStamina);
                float h = staminaBar.GetComponent<RectTransform>().rect.height;
                staminaBar.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
                staminaBar.transform.Translate(new Vector3((-oldW + w) / 2, 0, 0));
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
            de
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
                print("ziemia");
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
    public void heal(float healing)
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
    public IEnumerator StaminaCD()
    {
        staminaCooldown = true;
        yield return new WaitForSeconds(3);
        staminaCooldown = false;

    }
    public IEnumerator DamageEnum(int newHealth)
    {
        
        if (invulnerable == false&&health>0&&respawned==false)
        {
            health -= newHealth;
			GetComponent<AudioSource>().Play ();
            UpdateHealth();
            if(health<=0)
            {
                print("poszło die enum");
                die();
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
		GetComponent<AudioSource>().Play ();
        UpdateHealth();
        if(health<=0&&respawned==false)
            {
                print("poszło die take");
                die();
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
            if (Input.GetMouseButtonDown(0)&&menu.activeSelf==false)
            {
                GetComponent<Animation>().wrapMode = WrapMode.Once;
                GetComponent<Animation>().GetClip("Lumbering");                
                

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
    public void die()
    {
        gbc.musicUp = false;
        bossBar.SetActive(false);
        bossBarBackground.SetActive(false);
        camerascript.stopAllMusic();
        Time.timeScale = 0;
        deathmenu.SetActive(true);
        isMenuUp = true;
        Cursor.visible = true;
        //Application.LoadLevel("Main Menu");
    }
   public IEnumerator isRespawned()
    {
        health = maxHealth;
        UpdateHealth();
        respawned = true;
        yield return new WaitForSeconds(3);
        respawned = false;
    }
    public void respawn()
    {
        health = maxHealth;
        UpdateHealth();
        StartCoroutine(isRespawned());
        
        transform.position = Spawnpoint;

		//Rigidbody.isKinematic = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Water")
        {
            print("poszło die water");
            die();
        }
    }
    public void win()
    {
       
        camerascript.stopAllMusic();
        Time.timeScale = 0;
        winmenu.SetActive(true);
        isMenuUp = true;
        Cursor.visible = true;
        //Application.LoadLevel("Main Menu");
    }
}