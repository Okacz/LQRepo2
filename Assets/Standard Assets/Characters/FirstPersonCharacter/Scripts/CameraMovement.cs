using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float speed;
  Transform target;
  
 void Update () {
     if (Input.GetMouseButton(0))
     {
         target = transform.parent;
         transform.RotateAround(new Vector3(target.position.x, target.position.y, target.position.z), Vector3.up, Input.GetAxis("Mouse X") * speed);
         //transform.RotateAround(new Vector3(target.position.x, target.position.y, target.position.z), Vector3.left, Input.GetAxis("Mouse Y") * speed);
         //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
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

 }

}
