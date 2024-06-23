using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{ // flag to keep track whether we are dragging or not
    public  bool isDragging = false;
   
    public float maxDistance = 20f;
    
    float startMouseX;
    float startMouseY;
   [SerializeField]
    public static string location = "Sphere";

    // Camera component
    public  Camera cam;
    // Start is called before the first frame update
    void Start()
    {
       
       
        cam = this.gameObject.GetComponent<Camera>();
    }

	// Update is called once per frame
    // Variables to control the delay
bool isWaitingForTransfer = false;
float transferDelay = 1.0f; // Adjust this value as needed
public float currentDelayTime = 0.0f;

void FixedUpdate()
{
    int layerMask = 1 << 8;
    layerMask = ~layerMask;
     
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask) )
    {
       

        
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            Debug.Log("Did Hit");

            // Get the center of the hit object
            // Assuming ScriptContainingSphere is the script attached to hit.gameObject
            // Assuming Cube is the script attached to hit.gameObject
            Cube cubeScript = hit.collider.gameObject.GetComponent<Cube>();
            if (!cubeScript){
                
                return ;
            }
            

    // Access the sphere GameObject from the script
            GameObject sphere = cubeScript.sphere;

    // Now you can access the center of the sphere's bounds
            Vector3 center = sphere.GetComponent<Renderer>().bounds.center;


            Debug.Log("Center of the hit object: " + center + "   " + transform.position);
            if (Vector3.Distance(center, transform.position) >2){
                if (!isWaitingForTransfer)
                {
                    
                    // Start the delay before transferring
                    isWaitingForTransfer = true;
                    currentDelayTime = 0.0f;
                }
                else
                {
                    
                   
                    // If the delay time has passed, transfer to the center of the hit object
                    currentDelayTime += Time.deltaTime;
                    if (currentDelayTime >= transferDelay)
                    {
                       
                        cubeScript.changeLocation();
                        transform.position = center;
                        
                        
                        center.z +=10f;
                        center.y -=4f;
                        
                       
                       
                        isWaitingForTransfer = false; // Reset the flag
                    }
                }
            }
        

    }
    else
    {
        currentDelayTime = 0.0f;
       
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.white);
        Debug.Log("Did not Hit");
    }
}
void SetRotate(GameObject toRotate, GameObject camera)
    {
        //You can call this function for any game object and any camera, just change the parameters when you call this function
        transform.rotation = Quaternion.Lerp(toRotate.transform.rotation, camera.transform.rotation, 1000f * Time.deltaTime);
    }
public static void changeLocation(string locationName){
    location = locationName;
}

	void Update () {
		    
        // if we press the left button and we haven't started dragging
        if(Input.GetMouseButtonDown(0) && !isDragging )
        {                
            // set the flag to true
            isDragging = true;

            // save the mouse starting position
            startMouseX = Input.mousePosition.x;
            startMouseY = Input.mousePosition.y;
        }
        // if we are not pressing the left btn, and we were dragging
        else if(Input.GetMouseButtonUp(0) && isDragging)
        {                
            // set the flag to false
            isDragging = false;
        }
    }
   
    void LateUpdate()
    {
        // Check if we are dragging
         if(isDragging)
        {
            //Calculate current mouse position
            float endMouseX = Input.mousePosition.x;
            float endMouseY = Input.mousePosition.y;

            //Difference (in screen coordinates)
            float diffX = endMouseX - startMouseX;
            float diffY = endMouseY - startMouseY;

            //New center of the screen
            float newCenterX = Screen.width / 2 + diffX;
            float newCenterY = Screen.height / 2 + diffY;

            //Get the world coordinate , this is where we want to look at
            Vector3 LookHerePoint = cam.ScreenToWorldPoint(new Vector3(newCenterX, newCenterY, cam.nearClipPlane));

            //Make our camera look at the "LookHerePoint"
            transform.LookAt(LookHerePoint);

            //starting position for the next call
            startMouseX = endMouseX;
            startMouseY = endMouseY;
        }
    }
}