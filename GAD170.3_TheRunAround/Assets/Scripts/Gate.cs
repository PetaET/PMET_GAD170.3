using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all functions and CROSS REFERENCEs for Teleportation gate at end of level
/// </summary>
public class Gate : MonoBehaviour
{
    public Player pRef;                                         // Player script reference
    public GameObject gate;                                     // Gate game object reference                          
    public GameObject tFour;                                    // Unlocked TP object reference
    public GameObject tFourLocked;                              // Locked TP object reference
    public GameObject brokenGate;                               // Broken gate game object reference

    /// <summary>
    /// Deactivates all of the gate and corresponding teleport game objects
    /// </summary>
    public void GateDeactivated()
    {
        brokenGate.SetActive(true);                             // Activate broken gate game object
        gate.SetActive(false);                                  // Deactivate gate
        tFour.SetActive(false);                                 // Deactivate unlocked TP
        tFourLocked.SetActive(false);                           // Deactivate locked TP
    }

    /// <summary>
    /// Activates the gate and locked teleport
    /// </summary>
    public void GateLocked()
    {
        brokenGate.SetActive(false);                            // Deactivate broken gate object
        gate.SetActive(true);                                   // Activate gate
        tFourLocked.SetActive(true);                            // Activate locked TP
    }
    void OnTriggerEnter(Collider gate)
    {
        if (gate.CompareTag("fourunlocked"))
        {
            // Next level... Print final scores 
        }
    }

    /// <summary>
            /// Deactivates the locked teleport and activates the unlocked teleport
            /// </summary>
    public void GateUnlocked()
    {
        tFourLocked.SetActive(false);                       // Deactivate locked TP
        tFour.SetActive(true);                              // Activate unlocked TP
    }

    /// <summary>
    /// Resets gate on respawn - if player dies and has lives left
    /// </summary>
    public void GateRespawn()
    { GateDeactivated(); }

    // Start is called before the first frame update
    void Start()
    {
        GateDeactivated();                                  // Deactivate gate
    }

    // Update is called once per frame
    void Update()
    {

    }
}
