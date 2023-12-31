using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TP : MonoBehaviour
{
    public Transform playerPos;                     // Player transform position reference
    public Transform marker;                        // Reference to the position marker
    public GameObject player;                       // Player object reference
    public AudioSource teleSound;                   // Teleport sound

    void OnTriggerEnter(Collider tP)
    {
        if (tP.tag == "one")
        {
            // Debug to let know teleport one is triggering
            Debug.Log("Teleport One Entered");
            // Teleport transform markers (marked by empty game objects)
            marker = GameObject.Find("posOne").transform;
            if (player != null)                                                                                 // If it exists do this thing...
            {
                Debug.Log("Player Object found");
                teleSound.Play();

                CharacterController cC = GetComponent<CharacterController>();
                cC.enabled = false;
                player.transform.position = marker.position;
                cC.enabled = true;
            }
        }
        if (tP.tag == "two")
        {
            Debug.Log("Teleport Two Entered");
            marker = GameObject.Find("posTwo").transform;
            if (player != null)
            {
                // This one is a trick and needs different sound
                // Add scream sound
                // Turn off roof trigger momentarily
                // add floor trigger (maybe activate on collision with TP two) to kill player - then turn off after collision (maybe add deactivation to respawn)
                // Turn roof trigger back on (maybe add to respawn)

                CharacterController cC = GetComponent<CharacterController>();
                cC.enabled = false;
                player.transform.position = marker.position;
                cC.enabled = true;
            }
        }
        if (tP.tag == "three")
        {
            // Debug to let know teleport one is triggering
            Debug.Log("Teleport Three Entered");
            // Teleport transform markers (marked by empty game objects)
            marker = GameObject.Find("posThree").transform;
            if (player != null)                                                                                 // If it exists do this thing...
            {
                Debug.Log("Player Object found");
                teleSound.Play();
                CharacterController cC = GetComponent<CharacterController>();
                cC.enabled = false;
                player.transform.position = marker.position;
                cC.enabled = true;

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
