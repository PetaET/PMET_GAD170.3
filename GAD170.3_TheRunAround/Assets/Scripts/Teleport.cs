using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Gate gRef;                               // GATE scriot reference
    public Transform posOne;                        // Reference to the position marker
    public Collider one;                            // Tied to Telep1 collider object in Unity
    public Collider two;                            // Tied to Telep2 collider object in Unity
    public Collider three;                          // Tied to Telep3 collider object in Unity
    public Collider four;                           // Tied to Telep4 collider object in Unity
    public Collider fourLocked;                     // Tied to TelepLocked collider object in Unity
    //public Collider tP;

    void OnTriggerEnter(Collider tP)
    {
        
        tP.transform.position = posOne.position;
        // This currently moves the head and the body does not follow
        // this also does not allow multiple teleports and positions

        //switch (tP)
        //{
        //    case Collider c1 when c1 == one:
        //        Debug.Log("Teleport One entered");
        //        // Teleport the player to marker posOne
        //        GameObject.Find("Player").transform.position = posOne.position;
        //        break;

        //    case Collider c2 when c2 == two:
        //        Debug.Log("Teleport Two entered");
        //        break;

        //    case Collider c3 when c3 == three:
        //        Debug.Log("Teleport Three entered");
        //        // Additional instructions for Teleport3
        //        break;

        //    case Collider c4 when c4 == four:
        //        Debug.Log("Teleport Four entered");
        //        // Additional instructions for Teleport4
        //        break;

        //    case Collider cLocked when cLocked == fourLocked:
        //        Debug.Log("Teleport Four entered whilst still locked");
        //        // Player dies
        //        break;

        //if (cGO == one)
        //{
        //    Debug.Log("Teleport One entered");
        //    // Teleport the player to marker posOne
        //    GameObject.Find("Player").transform.position = posOne.position;
        //}
        //    if (tP == two) 
        //{
        //    Debug.Log("Teleport Two entered");
        //    // Trick. Player falls into lava and dies
        //}
        //if (tP == three) 
        //{
        //    Debug.Log("Teleport Three entered");
        //}
        //if(tP == four) 
        //{
        //    Debug.Log("Teleport Four entered");
        //}
        //if(tP == fourLocked) 
        //{
        //    Debug.Log("Teleport Four entered whilst still locked");
        //    // Player dies
    }
    
    // Start is called before the first frame update
    void Start()
    {
        FindMarkers();
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// Finds and defines the teleport marker positions / where to teleport to for each teleport trigger
    /// </summary>
    public void FindMarkers()
    {
        // Teleport transform markers (marked by empty game objects)
        posOne = GameObject.Find("posOne").transform;

        // add other markers once marker one and teleport one are working
    }
}
