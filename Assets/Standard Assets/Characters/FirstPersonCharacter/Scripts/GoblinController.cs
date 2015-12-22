using UnityEngine;
using System.Collections;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class GoblinController : MonoBehaviour
    {
        public SimpleMovement controller;
        public Transform Player;
        public GameObject loot;
        float health = 100;
        int walk = 0;
        bool invulnerable = false;
        float MoveSpeed = 3;
        float MaxDist = 10;
        float MinDist = 3;
        public bool dropsPotion = false;
        bool idiot = false;
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
            

            if(health>0)
            {
                
                if (!GetComponent<Animation>().IsPlaying("attack1")&&!GetComponent<Animation>().IsPlaying("block"))
                {
                    if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.position.x, 0, Player.position.z)) > MaxDist)
                    {
                        /*GetComponent<Animation>().wrapMode = WrapMode.Default;

                        GetComponent<Animation>().Play("idle");*/
                        if (walk == 2)
                        {
                            transform.position += transform.forward * MoveSpeed / 2 * Time.deltaTime;
                        }
                        if (idiot == false)
                        {
                            walk = Random.Range(0, 3);
                            
                            //StartCoroutine(walkAroundLikeAnIdiot(walk));
                        }
                        
                    }
                    /*else
                        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.position.x, 0, Player.position.z)) > MaxDist)
                        {
                            print("123");
                            if(idiot==false)
                            {
                                StartCoroutine(walkAroundLikeAnIdiot());
                            }

                        }*/
                        else
                        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.position.x, 0, Player.position.z)) <= MaxDist && Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.position.x, 0, Player.position.z)) >= MinDist)
                        {
                            transform.LookAt(Player);

							//stary movement
                            /*transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                            transform.position += transform.forward * MoveSpeed * Time.deltaTime;*/
							
							NavMeshAgent agent = GetComponent<NavMeshAgent>();
							agent.destination = Player.position;
                            GetComponent<Animation>().wrapMode = WrapMode.Default;
                            GetComponent<Animation>().Play("run");

                        }
                        else

                            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.position.x, 0, Player.position.z)) < MinDist)
                            {
                                transform.LookAt(Player);
                                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                                GetComponent<Animation>().wrapMode = WrapMode.Once;
                                GetComponent<Animation>().Play("attack1");
                            }
                }
            }
            
            
        }
        IEnumerator walkAroundLikeAnIdiot(int walk)
        {
            idiot = true;
            float randomDirection = Random.Range(0, 360);
            
            transform.rotation = Quaternion.Euler(new Vector3(0, randomDirection, 0));
            if(walk==2)
            {
                GetComponent<Animation>().wrapMode = WrapMode.Loop;
                GetComponent<Animation>().Play("run");
                
                    
                
            }
            else
            {
                GetComponent<Animation>().wrapMode = WrapMode.Default;

                GetComponent<Animation>().Play("idle");
            }
            yield return new WaitForSeconds(4);
            idiot = false;
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

            if (dropsPotion == true)
            {
                GameObject a = (GameObject)Instantiate(loot, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.Euler(-90, -180, 0));
            }          

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
			if((other.tag=="PlayerPhysicsProjectile")&&(health > 0))
            {
                GameObject thingy = other.gameObject;
                Vector3 thingyspeed=thingy.GetComponent<Rigidbody>().velocity;
                //print(thingyspeed.magnitude);
                if(thingyspeed.magnitude>3)
                {
                    print(thingyspeed.magnitude);
                    if (invulnerable == false)
                    {
                        StartCoroutine(getRect(50));
                    }
                }
                
            }
            
        }
    }
}
