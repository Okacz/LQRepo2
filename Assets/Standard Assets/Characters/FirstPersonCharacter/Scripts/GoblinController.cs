using UnityEngine;
using System.Collections;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class GoblinController : MonoBehaviour
    {
        public SimpleMovement controller;
        public Transform Player;
        public GameObject potion;

        float health = 100;
        bool invulnerable = false;
        float MoveSpeed = 3;
        float MaxDist = 8;
        float MinDist = 3;
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
                transform.LookAt(Player);
                transform.eulerAngles = new Vector3(
                0,
                transform.eulerAngles.y,
                0
                );
                if (!GetComponent<Animation>().IsPlaying("attack1")&&!GetComponent<Animation>().IsPlaying("block"))
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

                        }
                        else

                            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(Player.position.x, 0, Player.position.z)) < MinDist)
                            {
                                GetComponent<Animation>().wrapMode = WrapMode.Once;
                                GetComponent<Animation>().Play("attack1");


                            }
                }
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
            
        }
    }
}
