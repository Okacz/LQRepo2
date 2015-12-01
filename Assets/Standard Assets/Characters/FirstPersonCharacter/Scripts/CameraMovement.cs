using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float speed;
  Transform target;
  

    void Start()
  {
      
  }
 void Update () {
     target = transform.parent;
     
     if (Input.GetMouseButton(0))
     {
         print(transform.rotation.y);
         target = transform.parent;
         transform.RotateAround(new Vector3(target.position.x, target.position.y, target.position.z), Vector3.up, Input.GetAxis("Mouse X") * speed);
         //transform.RotateAround(new Vector3(0, target.position.y, target.position.z), Vector3.left, Input.GetAxis("Mouse Y") * speed); 
        // transform.RotateAround(new Vector3(target.position.x, target.position.y, target.position.z), new Vector3((float)(Mathf.Abs((float)(transform.rotation.y))-1), 0,  Mathf.Abs((float)(-transform.rotation.y))), Input.GetAxis("Mouse Y") * speed);
                 //transform.RotateAround(new Vector3(target.position.x, target.position.y, target.position.z), new Vector3((float)(transform.rotation.y%1), 0,  Mathf.Abs((float)(-transform.rotation.y))), Input.GetAxis("Mouse Y") * speed);

         //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
         transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                0
                );
     }
     if (Input.GetMouseButton(1))
         {
             target = transform.parent;
             transform.RotateAround(new Vector3(target.position.x, target.position.y, target.position.z), Vector3.up, Input.GetAxis("Mouse X") * speed);
         
             float a = target.transform.eulerAngles.y;
             target.transform.LookAt(transform);
             
             target.transform.eulerAngles = new Vector3(0, target.transform.eulerAngles.y+180, 0);
             float b=target.transform.eulerAngles.y;
             float c=a-b;
             transform.RotateAround(new Vector3(target.position.x, target.position.y, target.position.z), Vector3.up, c);
             print(c);
         }
     //transform.RotateAround(new Vector3(target.position.x, target.position.y, target.position.z), Vector3.right, Input.GetAxis("Mouse Y") * speed);

 }

}
