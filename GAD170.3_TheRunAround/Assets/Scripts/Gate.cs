using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject gate;
    public GameObject tFour;
    public GameObject tFourLocked;

    /// <summary>
    /// Deactivates all of the gate and corresponding teleport game objects
    /// </summary>
    public void GateDeactivated()
    {
        gate.SetActive(false);
        tFour.SetActive(false);
        tFourLocked.SetActive(false);
    }

    /// <summary>
    /// Activates the gate and locked teleport
    /// </summary>
    public void GateLocked()
    {
        gate.SetActive(true);
        tFourLocked.SetActive(true);
    }

    /// <summary>
    /// Deactivates the locked teleport and activates the unlocked teleport
    /// </summary>
    public void GateUnlocked()
    {
        tFourLocked.SetActive(false);
        tFour.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        GateDeactivated();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
