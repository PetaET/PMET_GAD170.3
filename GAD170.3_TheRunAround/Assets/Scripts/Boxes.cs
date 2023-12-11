using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    public Player pRef;
    public Gate gRef;
    public static int boxCollected = 0;
    public AudioSource boxSound;

    public GameObject boxOne;
    public GameObject boxTwo;
    public GameObject boxThree;
    public GameObject boxFour;

    // Static Box Collection bools
    public static bool oneCollected = false;
    public static bool twoCollected = false;
    public static bool threeCollected = false;
    public static bool fourCollected = false;


    /// <summary>
    /// Whenever box is collided with do these things...
    /// </summary>
    void OnTriggerEnter(Collider box)
    {
        if (box.tag == "boxOne")
        {
            Debug.Log("Box One triggered");                                                         // Debug box one triggered
            boxCollected++;                                                                         // Add to boxCollected variable
            oneCollected = true;                                                                    // Set oneCollected to true
            pRef.BoxScores();                                                                       // Cross Reference to PLAYER script BoxScores() function
            if (boxSound != null && boxOne != null)                                                 // If both these things exist... do these things...
            {
                boxSound.Play();                                                                    // Play the box crush sound
                Debug.Log("Box sound played");                                                      // Debug that the sound played
                boxOne.SetActive(false);                                                            // Deactivate boxOne
                Debug.Log("Box One deactivated. Total boxes collected: " + boxCollected + ".");     // Debug that the box has been collected and how many in total have been collected
            }
        }
        if (box.tag == "boxTwo")                                                                    // Same as above but for boxTwo (to save repeating myself)
        {
            Debug.Log("Box Two triggered");
            boxCollected++;
            twoCollected = true;
            pRef.BoxScores();                                                                       // Cross Reference to PLAYER script BoxScores() function
            if (boxSound != null && boxTwo != null)
            {
                boxSound.Play();
                Debug.Log("Box sound played");
                boxTwo.SetActive(false);
                Debug.Log("Box Two deactivated. Total boxes collected: " + boxCollected + ".");
            }
        }
        if (box.tag == "boxThree")                                                                  // Same as above but for boxThree (to save repeating myself)
        {
            Debug.Log("Box Three triggered");
            boxCollected++;
            threeCollected = true;
            pRef.BoxScores();                                                                       // Cross Reference to PLAYER script BoxScores() function
            if (boxSound != null && boxThree != null)
            {
                boxSound.Play();
                Debug.Log("Box sound played");
                boxThree.SetActive(false);
                Debug.Log("Box Three deactivated. Total boxes collected: " + boxCollected + ".");
            }
        }
        if (box.tag == "boxFour")                                                                   // Same as above but for boxFour (to save repeating myself)
        {
            Debug.Log("Box Four triggered");
            boxCollected++;
            fourCollected = true;
            pRef.BoxScores();                                                                       // Cross Reference to PLAYER script BoxScores() function
            gRef.GateLocked();                                                                      // Activate locked gate
            if (boxSound != null && boxFour != null)
            {
                boxSound.Play();
                Debug.Log("Box sound played");
                boxFour.SetActive(false);
                Debug.Log("Box Four deactivated. Total boxes collected: " + boxCollected + ".");
            }
        }
    }

    /// <summary>
    /// Respawns boxes (objects) and BOXES script local variables if player dies but has lives left
    /// </summary>
    public void RespawnBoxes()
    {
        // Reset Game objects
        boxOne.SetActive(true);
        boxTwo.SetActive(true);
        boxThree.SetActive(true);
        boxFour.SetActive(false);

        // Reset Variables
        boxCollected = 0;
        oneCollected = false;
        twoCollected = false;
        threeCollected = false;
        fourCollected = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        boxFour.SetActive(false);                                       // Make fourth box invisible until third collected
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCollected == 3)
        {
            boxFour.SetActive(true);                                                                // Make fourth box visible
        }
    }
}
