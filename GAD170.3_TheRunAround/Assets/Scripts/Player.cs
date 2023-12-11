using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains PLAYER lives and scores.  CROSS REFERENCES & DELEGATES with other scripts scores, 
/// and for player respawn or to let the other scripts know if player is dead and if player has any lives left
/// </summary>
public class Player : MonoBehaviour
{
    // Player's starting location
    public Transform respawnPosition;
    // Player object
    public GameObject player;

    #region Variables
    // Static bools and ints
    public static bool playerDied = false;                                      // Static bool to say if the player has died
    public static bool respawn = false;                                         // Static bool to say if the player has any respawn lives left
    public static int playerLives = 3;                                          // Static int of total lives allocated/left

    // Player scores/stats
    public int boxScore = 0;                                                    // Sets up int score for when boxes are collected.  Random number will be assigned
    public int livesLeft;                                                       // How many Lives Left to match with static
    public int currentXp = 0;                                                   // Player's XP score
    private int allXp;                                                          // XP added together from each try/life to then average out
    public int finalXp = 0;                                                     // Final XP
    public int currentScore = 0;                                                // Player's score
    private int allScores;                                                      // All scores added together from each try/life to then average out
    public int finalScore = 0;                                                  // Final averaged score
    private int tries;                                                          // Used to average scores

    // Public strings: Faux player name
    public string firstName = "Lū";
    public string lastName = "Ping̊";
    public string fullName;

    // Private bools and ints to check if boxes collected in correct order for bonus points (Explained in PickUpScores() function)
    public int order = 0;
    private bool correctBox = false;
    private bool stillCorrectBox = false;
    private bool one = false;
    private bool two = false;
    private bool three = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        fullName = firstName + " " + lastName;                                  // Full name = to both names
        allXp = 0;
        allScores = 0;
        tries = 1;                                                              // Start with one as start is first attempt
    }

    private void OnTriggerEnter(Collider deadly)
    {
        if (deadly.CompareTag("roof"))
        {
            Debug.Log("Player ran out of oxygen and died");                         // Debug player died
            PlayerDied();                                                           // Call PlayerDied() function
        }
        if (deadly.CompareTag("lava"))
        {
            Debug.Log("Player melted in the lava");                                 // Debug player died
            PlayerDied();                                                           // Call PlayerDied() function
        }
        if (deadly.CompareTag("fourlocked"))
        {
            Debug.Log("GATE LOCKED! You DIED!");                                    // Debug gate locked and player dies
            PlayerDied();                                                           // PLAYER script CROSS REFERENCE PlayerDied() called
        }
    }

    /// <summary>
    /// Calculates box collection scores and bonuses if collected in the correct order
    /// </summary>
    public void BoxScores()
    {
        // Boxes
        boxScore = Random.Range(0,31);                                              // Sets box score to a random range between 1 and 30.
        Debug.Log("Box collected!  Points Earned = " +  boxScore + ".");            // Debugs box score to make sure matches random int each time
        currentScore += boxScore;                                                   // Adds box score to player score
        Debug.Log("Player Score: " + currentScore + ".");                           // Debug player score

        if (Boxes.oneCollected == true && !one)                                     // If box one is collected and one is false
        {
            order += 1;                                                             // Add one to order
            one = true;                                                             // Set one to tru so it will only calculate once
        }
        if (Boxes.twoCollected == true && !two)                                     // If box two is collected and two is false
        {
            order += 2;                                                             // Add two to order
            two = true;                                                             // Set two to tru so it will only calculate once
        }
        if (Boxes.threeCollected == true && !three)                                 // If box three is collected and three is false
        { 
            order += 3;                                                             // Add three to order
            three = true;                                                           // Set three to tru so it will only calculate once
        }

        // The following are to check if the correct order of boxes has been collected (one to four) for bonus points
        if(Boxes.boxCollected == 1 && order == 1)
        {
            currentXp += 5;                                                           // Add 5 XP
            currentScore += 10;                                                      // Ten bonus points
            correctBox = true;                                                      // Step one Correct
            // Debug updated player score
            Debug.Log("Box ONE has been collected FIRST.  +10 BONUS POINTS. Player Score: " + currentScore + ".");
        }
        if(Boxes.boxCollected == 2 && order == 3 && correctBox)                     // If all of these are true/correct the second box has been chosen in the correct order. Do these things...
        {
            currentXp += 10;                                                        // Add 10 XP
            currentScore += 20;                                                     // Twenty bonus points
            stillCorrectBox = true;                                                 // Step two correct
            // Debug updated player score
            Debug.Log("Box TWO has been collected SECOND.  +20 BONUS POINTS. Player Score: " + currentScore + ".");
        }
        if (Boxes.boxCollected == 3 && order == 6 && stillCorrectBox)                // If these are true then the third has been collected in the correct order.  Do these things...
        {
            currentXp += 15;                                                         // Add 15 XP
            currentScore += 30;                                                      // Thirty bonus points
            // Debug player score
            Debug.Log("Box THREE has been collected THIRD.  +30 BONUS POINTS. Player Score: " + currentScore + ".");
        }
        if (Boxes.boxCollected == 4 && stillCorrectBox)                              // If these are true/correct (all boxes collected in correct order) do these things...
        {
            currentXp += 20;                                                         // Add 20 XP
            currentScore += 40;                                                      // Forty bonus points
            // Debug player score
            Debug.Log("Box FOUR has been collected FOURTH.  +40 BONUS POINTS. Player Score: " + currentScore + ".");
        }
        else { return; }
    }

    /// <summary>
    /// If player dies and has to start again scores are stored and then averaged at the end for final scores
    /// </summary>
    public void AverageStats()
    {
        livesLeft = playerLives;                                                        // Sets lives left same as = playerLives

        // If player dies but has lives left do this...
        if (playerDied && livesLeft > 0)
        {
            Debug.Log(fullName + " DIED! You have " + livesLeft + " lives left.");      // Debugs how many lives are left
            allScores += currentScore;                                                  // Adds playerScore to allScores tally before setting playerScore back to zero
            allXp += currentXp;                                                         // Adds xP to allXp tally before setting xP back to zero
            respawn = true;                                                             // Sets respawn to true
            Respawn.respawnDelegates?.Invoke();                                         // If respawnDelegates from RESPAWN script is not null then invoke delegate functions
        }
        // if no lives left do this...
        if (playerDied && livesLeft <= 0) 
        { 
            respawn = false;                                                            // Do not respawn player has no lives left
            Debug.Log(fullName + " DIED! you have " + livesLeft + " lives left.");                 // Debug no lives left
        }
    }

    /// <summary>
    /// Static public function that tells game the player died - to use across all scripts if necessary
    /// </summary>
    public void PlayerDied()
    {
        playerDied = true;                                                            // Player has died
        playerLives--;                                                                // Subtracts one life from tall
        AverageStats();
    }

    /// <summary>
    /// Respawn instructions for this script (resets necessary stats and bools)
    /// </summary>
    public void PlayerRespawn()
    {
        // If respawning do these things
        if (respawn)
        {
            // if player exists
            if (player != null)
            {
                CharacterController cC = GetComponent<CharacterController>();
                cC.enabled = false;
                player.transform.position = respawnPosition.position;
                cC.enabled = true;
            }
            // adds to number of attempts
            tries++;
            // Reset local (PLAYER script) stats and bools 
            currentXp = 0;
            currentScore = 0;
            boxScore = 0;
            order = 0;
            correctBox = false;
            stillCorrectBox = false;
            playerDied = false;
        }
    }
}
