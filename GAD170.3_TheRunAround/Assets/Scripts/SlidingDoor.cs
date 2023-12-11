using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// This script activates the sliding door in TheRunAround game.  It tells the gate when to open and where to slide open to
/// </summary>
public class SlidingDoor : MonoBehaviour
{
    /* Original/Closed position = x: 30.47034; y: -447.26580; z: 23.59655
     * Opened position = x: 30.47034; y: -447.26580; z: 23.59655
     * The difference between the two z positions = +8.55345
     * The player must collect 3 boxes before the door will open
     * The player must teleport into the cage to collect the third box, before opening sliding door from inside the cage
     * If 3 boxes are collected I want the door to slide open and holds for 5 seconds
     * if 3 boxes are collected the fourth box appears on the game screen to be collected
     * If player is inside the cage and has not collected 3 boxes they die after 5 seconds
     * Maybe play a particle effect for when the player dies inside the cage?
     * If the player fails to exit after 5 seconds of the door being open the door closes (player dies after 5 seconds inside the closed cage)
     * If they do not escape before the door closes they die after 5 seconds
     * If the player dies inside the cage the game resets back to beginning
     * Therefore gate resets back to original locked position and boxes 3 & 4 reset to original states
     */

    #region Cross Script Referferences, Variables, Audio, and Vector 3 ref
    public Boxes boxRef;                                        // Cross References BOXES script
    public AudioSource openSound;                               // Slide open sound
    public AudioSource closeSound;                              // Slide close sound
    Vector3 startPos;                                           // Starting/locked position of sliding door
    Vector3 currentPos;                                         // Updated position whilst keeping start marker
    public Transform endPos;                                    // Sets finishing position for opened door
    public Transform lockedPos;
    State state = State.Locked;                                 // Sets 'state' to enum State Locked.
    float amount = 0;                                           // amount will be set to delta time for Opening speed
    float timer = 0;                                            // timer will be set to delta time for Hold position
    bool opening = false;
   

    // Used to activate 5 second timer if player is locked inside cage without 3boxes collected 
    [HideInInspector] public bool isPlayerInside = false;       // Hidden but public bool to tell if player is inside the cage or not
    [HideInInspector] public bool isPlayerCaged = false;        // Hidden but public bool to tell if player is caged (door Locked) or not

    #endregion

    /// <summary>
    /// This contains the various 'STATES' of movement that the sliding door is in
    /// </summary>
    enum State 
    {
        Locked,                 // Door closed and locked
        Opening,                // Door opening
        Hold,                   // Hold time while door is open
        Closing                 // Door closing
    }

    /// <summary>
    /// This code switches the state of the sliding door using the STATE enum.  Each case represents a different possible value/position, and the corresponding code block is executed when a match is found.
    /// </summary>
    public void SwitchState()
    {
        switch (state)
        {
            case 0:                                                                         // State Enum 0 = Locked
                break;                                                                      // Breaks and returns
            case State.Opening:                                                             // When the door is opening (defines actions of enum State Opening)
                amount += Time.deltaTime / 3.0f;                                            // Sets the speed to 3 seconds
                transform.position = Vector3.Lerp(currentPos, endPos.position, amount);     // Move the position to Open
                if (transform.position == endPos.position)                                  //
                {
                    state = State.Hold;                                                     // change state to Hold
                    timer = 0;                                                              //
                }
                break;                                                                      //
            case State.Hold:                                                                // When the door is holding (defines actions of enum State Hold)
                timer += Time.deltaTime / 3.0f;                                             //
                //openSound.Stop();
                if (timer >= 3.0f)                                                          //
                {
                    state = State.Closing;                                                  // Change state to Closing
                }
                break;
            case State.Closing:                                                             // When door is Closing (defines actions of enum State Closing)
                amount += Time.deltaTime;
                transform.position = Vector3.Lerp(currentPos, lockedPos.position, amount);            // Door slams shut to 'Locked' position
                if (transform.position == lockedPos.position)                                         //
                { 
                    state = State.Locked;                                                   //
                    timer = 0;
                    amount = 0;
                }
                break;                                                                      //
        }
    }

    public void Open()
    {
        // Hidden cheat code
        if (Input.GetKeyDown(KeyCode.Z))                                        // If [Z] pressed State Opening is activated
        {
            opening = true;
            if (opening == true)
            {
                openSound.Play();                                               // Play 'slide open' sound
            }
            state = State.Opening;                                              //  Open the door
            //openSound.Play();                                                 // Play 'slide open' sound
        }
        // Need this for actual game play but for some reason it isn't working but the GETKEYDOWN[Z] is????
        if (Boxes.boxCollected >= 3)                                            // If 3 boxes collected Opening is activated
        { 
            opening = true;
            if (opening == true)
            {
                openSound.Play();                                               // Play 'slide open' sound
            }
            //openSound.Play();                                                 // Play 'slide open' sound
            state = State.Opening;                                              // Open the door and start SwitchState
        }
        if (state == State.Closing)
        {
            opening = false;
            closeSound.Play();                                                 // Play 'slide close' sound
        }
        else
        return;                                                             // Otherwise do nothing and return
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;                           // Finds the starting position of the sliding door.  In this case the 'Locked' position
        currentPos = startPos;
        endPos = GameObject.Find("OpenMarker").transform;
        lockedPos = GameObject.Find("ClosedMarker").transform;
        openSound.Stop();
        closeSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchState();                                          // Calls the SWITCHSTATE() function 
        Open();
        
    }
}