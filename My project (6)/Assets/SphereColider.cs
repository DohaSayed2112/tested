using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColider : MonoBehaviour
{
    public Transform other ;
    public double dist;
    
    // Start is called before the first frame update
    void Start()
    {
          this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(other.position, transform.position);
        if (dist <= 1)
        {
            this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        else 
        {
            this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }
}