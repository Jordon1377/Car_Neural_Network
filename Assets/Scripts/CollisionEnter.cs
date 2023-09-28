using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEnter : MonoBehaviour
{
    
    public GameObject selfGameObject;
    private GameObject colliderGameObject;

    public static bool wallCol = false;



    public void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.tag == "Wall") {

            //Debug.Log("Wall Collition Detected");
            wallCol = true;

           
           

        }

        //if (collision.gameObject.tag == "AICar") {

            //Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), selfGameObject.GetComponent<Collider>(), true);

        //}

    }
    private void OnTriggerEnter(Collider other) {
       
        if (other.gameObject.tag == "CenterLane"){
            
            colliderGameObject = other.gameObject;
            Debug.Log("Center Collition Detected");
            //Physics.IgnoreCollision(colliderGameObject.GetComponent<Collider>(), selfGameObject.GetComponent<Collider>());
        }



   }


}
