using UnityEngine;
using System.Collections;

    public class GoblinMageController : MonoBehaviour
    {
        public SimpleMovement controller;
        public Transform Player;
        public GameObject fireball;
        public float Cooldown = 2;
        float health = 100;
        bool invulnerable = false;
        public float MoveSpeed = 3;
        public float MaxDist = 8;
        public float MinDist = 3;
        bool ready = true;
        
        // Use this for initialization
        void Start()
        {
            GameObject gameControllerObject = GameObject.FindWithTag("MainLumberjack");
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
                transform.LookAt(Player);
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

                            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                            GetComponent<Animation>().wrapMode = WrapMode.Default;
                            GetComponent<Animation>().Play("run");
                            StartCoroutine(shootFireball());

                        }
                        else

                            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.position.x, 0, Player.position.z)) < MinDist)
                            {
                                GetComponent<Animation>().wrapMode = WrapMode.Default;
                                GetComponent<Animation>().Play("idle");
                                StartCoroutine(shootFireball());

                            }
                }
            }


        }
        IEnumerator shootFireball()
        {
            if (ready == true)
            {
                GameObject a= (GameObject)Instantiate(fireball, new Vector3(transform.position.x, transform.position.y+1, transform.position.z)+transform.forward*2, transform.rotation);
                

                ready = false;
                yield return new WaitForSeconds(Cooldown);
                ready = true;
            }


        }
        IEnumerator getRect(float damage)
        {
            GetComponent<Animation>().Stop();
            GetComponent<Animation>().wrapMode = WrapMode.Once;
            GetComponent<Animation>().Play("block");
            health = health - damage;
            if (health <= 0)
            {
                die();
            }
            invulnerable = true;
            yield return new WaitForSeconds(1);
            invulnerable = false;


        }
        void die()
        {
            GetComponent<Animation>().Stop();
            GetComponent<Animation>().Play("death");
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;

        }
        void OnTriggerEnter(Collider other)
        {
            if (health > 0)
            {
                if (controller.GetComponent<Animation>().IsPlaying("Lumbering"))
                {

                    if (other.name == "NPC_Tools_Axe_004")
                    {

                        if (invulnerable == false)
                        {
                            StartCoroutine(getRect(50));
                        }
                    }
                }
            }
            if (other.tag == "PlayerPhysicsProjectile")
            {
                GameObject thingy = other.gameObject;
                Vector3 thingyspeed = thingy.GetComponent<Rigidbody>().velocity;
                print(thingyspeed);
                if (Mathf.Abs(thingyspeed.x) + Mathf.Abs(thingyspeed.y) + Mathf.Abs(thingyspeed.z) > 4)
                {

                    if (invulnerable == false)
                    {
                        StartCoroutine(getRect(50));
                    }
                }

            }

        }
    }
