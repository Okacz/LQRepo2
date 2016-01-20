using UnityEngine;
using System.Collections;

    public class GoblinBossController : MonoBehaviour
    {
        public SimpleMovement controller;
        public Transform Player;
        public float Cooldown = 2;
        public GameObject healthBar;
        public GameObject healthBarBackground;
        public GameObject fireball;
        public GameObject flamethrower;
        private float maxBarLength = 0;
        bool ready = true;
        public float health = 1000;
        bool invulnerable = false;
        float MoveSpeed = 6;
        float MaxDist = 16;
        float MinDist = 8;
        public GameObject camera;
        public CameraMovement3 camerascript;
        public bool musicUp = false;
        public bool doIRotate = true;
        // Use this for initialization
        Vector3 Barposition;
        void Start()
        {
            Barposition = healthBar.transform.position;
            health = 1000;
            maxBarLength = healthBar.GetComponent<RectTransform>().rect.width;
            GameObject gameControllerObject = GameObject.FindWithTag("GameController");
            if (gameControllerObject != null)
            {
                controller = gameControllerObject.GetComponent<SimpleMovement>();

            }
        }

        // Update is called once per frame
        void Update()
        {

            
            if (health > 0)
            {
                if(doIRotate==true)
                {
                    transform.LookAt(Player);

                }
                
                transform.eulerAngles = new Vector3(
                0,
                transform.eulerAngles.y,
                0
                );
                
                if (!GetComponent<Animation>().IsPlaying("attack1") && !GetComponent<Animation>().IsPlaying("block"))
                {
                    if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.position.x, 0, Player.position.z)) > MaxDist)
                    {
                        GetComponent<Animation>().wrapMode = WrapMode.Default;

                        GetComponent<Animation>().Play("idle");

                    }
                    else
                        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.position.x, 0, Player.position.z)) <= MaxDist && Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.position.x, 0, Player.position.z)) >= MinDist)
                        {
                            if(ready==true)
                            {
                                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                                GetComponent<Animation>().wrapMode = WrapMode.Default;
                                GetComponent<Animation>().Play("run");
                            }
                            if (musicUp == false)
                            {
                                camerascript.playBossMusic();
                                musicUp = true;
                            }

                        }
                        else

                            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.position.x, 0, Player.position.z)) < MinDist)
                            {
                                GetComponent<Animation>().wrapMode = WrapMode.Once;
                                if(ready==true)
                                {
                                    StartCoroutine(shootFireball());

                                }
                                if (musicUp == false)
                                {
                                    camerascript.playBossMusic();
                                    musicUp = true;
                                }

                            }
                }
            }


        }
        IEnumerator shootFireball()
        {

            

                ready = false;
                GetComponent<Animation>().wrapMode = WrapMode.Default;
                GetComponent<Animation>().Play("attack2");
                   // yield return new WaitForSeconds(GetComponent<Animation>().pla["attack2"].length);
                for(int i=0; i<8; i++)
                {
                    //GameObject a = (GameObject)Instantiate(fireball, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z) + transform.forward * 2, transform.rotation);
                    //print(transform.GetChild(2).GetChild(3).name);
                    GameObject a = (GameObject)Instantiate(fireball, transform.GetChild(2).GetChild(2).GetChild(0).transform.position, transform.rotation);

                    a.transform.LookAt(Player);
                    
                    yield return new WaitForSeconds(0.2f);
                }
                GetComponent<Animation>().Play("idle");
                for (int i = 0; i < 36; i++ )
                {
                    GameObject a = (GameObject)Instantiate(flamethrower, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (Quaternion.Euler(0, i*20, 0) * transform.right*(5+i*0.2f)), Quaternion.Euler(-90, 0, 0));
                    yield return new WaitForSeconds(0.1f);
                }


                    yield return new WaitForSeconds(Cooldown);
                ready = true;

            


        }
        IEnumerator spawnFire()
        {
            for (int i = 0; i < 360; i++)
            {
                GameObject a = (GameObject)Instantiate(flamethrower, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (Quaternion.Euler(0, i, 0) * transform.right * 5), Quaternion.Euler(-90, 0, 0));
                GameObject b = (GameObject)Instantiate(flamethrower, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (Quaternion.Euler(0, i, 0) * transform.right * 15), Quaternion.Euler(-90, 0, 0));
                GameObject c = (GameObject)Instantiate(flamethrower, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (Quaternion.Euler(0, i, 0) * transform.right * 10), Quaternion.Euler(-90, 0, 0));
                
            }
            yield return new WaitForSeconds(1);
            for (int i = 0; i < 360; i++)
            {
                GameObject a = (GameObject)Instantiate(flamethrower, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (Quaternion.Euler(0, i, 0) * transform.right * 20), Quaternion.Euler(-90, 0, 0));
                GameObject b = (GameObject)Instantiate(flamethrower, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (Quaternion.Euler(0, i, 0) * transform.right * 25), Quaternion.Euler(-90, 0, 0));
                GameObject c = (GameObject)Instantiate(flamethrower, new Vector3(transform.position.x, transform.position.y, transform.position.z) + (Quaternion.Euler(0, i, 0) * transform.right * 30), Quaternion.Euler(-90, 0, 0));

            }
            yield return new WaitForSeconds(1);
        }
        void getRect(float damage)
        {
            
            health = health - damage;
            if (health <= 0)
            {
                die();
            }
            


        }
        void die()
        {
            GetComponent<Animation>().Stop();
            GetComponent<Animation>().Play("death");
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(waitForWin());

        }
        public IEnumerator waitForWin()
        {
            yield return new WaitForSeconds(3);
            controller.win();

        }
        void OnTriggerEnter(Collider other)
        {
            if (health > 0)
            {
                if (controller.GetComponent<Animation>().IsPlaying("Lumbering"))
                {

                    if (other.name == "NPC_Tools_Axe_004")
                    {
                        if(musicUp==false)
                        {
                            camerascript.playBossMusic();
                            musicUp = true;
                        }
                        //camera.GetComponent<AudioSource>().pla
                        healthBar.SetActive(true);
                        healthBarBackground.SetActive(true);
                        if (invulnerable == false)
                        {
                            float d1 = maxBarLength / 2;
                            
                            other.GetComponent<AudioSource>().Play();
                            getRect(40);
                            float oldX = healthBar.transform.position.x;
                            float oldW = healthBar.GetComponent<RectTransform>().rect.width;
                            float w = maxBarLength * (health / 1000);
                            float h = healthBar.GetComponent<RectTransform>().rect.height;
                            healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
                            float d2 = healthBar.GetComponent<RectTransform>().rect.width;
                            healthBar.transform.Translate(new Vector3((-oldW + w) / 2, 0, 0));
                        }
                    }
                }
            }

        }
        
        public void healToMax()
        {
            health = 1000;
            float oldX = healthBar.transform.position.x;
            float oldW = healthBar.GetComponent<RectTransform>().rect.width;
            float w = maxBarLength * (health / 1000);
            float h = healthBar.GetComponent<RectTransform>().rect.height;
            healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
            float d2 = healthBar.GetComponent<RectTransform>().rect.width;
            healthBar.transform.Translate(new Vector3((-oldW + w) / 2, 0, 0));
        }
    }
