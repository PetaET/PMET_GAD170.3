using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
/// Contains functions and parameters for spheres and boxes
/// </summary>
public class PickUps : MonoBehaviour
{
    public PlayerMovement pMRef;
    public GameObject player;
    public GameObject rOne;
    public GameObject rTwo;
    public GameObject bOne;
    public GameObject bTwo;
    public GameObject rS;
    public GameObject bS;
    public Rigidbody rB;
    //This value is defined in Inspector
    public float floatStrength;
    // Cross reference bool for PLAYERMOVEMENT script
    [HideInInspector] public bool redActive = false;



/// <summary>
/// On trigger enter components and functions attached to RED (Antigravity) and BLUE (Gravity) spheres
/// </summary>
/// <param name="sphere"></param>
    void OnTriggerEnter(Collider sphere)
    {
        if (sphere.CompareTag("Red"))
        {
            // Red sphere activated/triggered
            redActive = true;
            // Access rigidbody and turn off gravity
            rB.GetComponent<Rigidbody>().useGravity = false;
            //CROSS REFERENCE to PLAYERMOVEMENT script gravity
            pMRef.gravity = 0f;

            if (pMRef.gravity == 0f)
            {
                // Apply an upward force instead of simulating gravity
                PlayerMovement.velocity.y += floatStrength * Time.deltaTime;
                // Move the player based on the calculated velocity
                transform.Translate(PlayerMovement.velocity * Time.deltaTime);
            }
            ParticleSystem psOne = rOne.GetComponent<ParticleSystem>();                                        // Accesses the PARTICLE SYSTEM boom COMPONENT
            if (psOne != null)                                                                                 // If it exists do this thing...
            {
                psOne.Play(true);                                                                              // Play the particle system (settings defined in inspector)
                Debug.Log("RED particle system activated");                                                    // Debug RED particle system psOne activated
                // Need to change player gravity 
            }
            ParticleSystem psTwo = rTwo.GetComponent<ParticleSystem>();                                        // Accesses the PARTICLE SYSTEM boom COMPONENT
            if (psTwo != null)                                                                                 // If it exists do this thing...
            {
                psTwo.Play(true);                                                                              // Play the particle system (settings defined in inspector)
                Debug.Log("Second RED particle system activated");                                             // Debug RED particle system psTwo activated
                rS.SetActive(false);                                                                           // Deactivates RED sphere 
            }
        }
        if (sphere.CompareTag("Blue"))
        {
            // Blue sphere turns off Red sphere functions and returns gravity
            redActive = false;
            // Accesses the rigidbody and turns gravity back on
            rB.GetComponent<Rigidbody>().useGravity = true;
            //CROSS REFERENCE to PLAYERMOVEMENT script gravity
            pMRef.gravity = -20f;
            // Return to normal gravity
            ParticleSystem psThree = bOne.GetComponent<ParticleSystem>();                                       // Accesses the PARTICLE SYSTEM boom COMPONENT
            if (psThree != null)                                                                                // If it exists do this thing...
            {
                psThree.Play(true);                                                                             // Play the particle system (settings defined in inspector)
                Debug.Log("BLUE particle system activated");                                                    // Debug BLUE particle system psThree activated
                // Need to return gravity back to normal 

            }
            ParticleSystem psFour = bOne.GetComponent<ParticleSystem>();                                        // Accesses the PARTICLE SYSTEM boom COMPONENT
            if (psFour != null)                                                                                 // If it exists do this thing...
            {
                psFour.Play(true);                                                                              // Play the particle system (settings defined in inspector)
                Debug.Log("Second BLUE particle system activated");                                             // Debug BLUE particle system psFour activated
                bS.SetActive(false);                                                                            // Deactivate BLUE sphere
            }
        }      
    }

    /// <summary>
    /// Stops all particle systems from playing before being interacted with
    /// </summary>
    public void psStop()
    {
        // RedOne particle system
        ParticleSystem psOne = rOne.GetComponent<ParticleSystem>();                                                // Accesses the PARTICLE SYSTEM rOne COMPONENT
        if (psOne != null)                                                                                         // If it exists do this thing...
        {
            psOne.Stop();                                                                                          // Makes sure particle system doesn't play until interacted with
        }
        // RedTwo particle system
        ParticleSystem psTwo = rTwo.GetComponent<ParticleSystem>();                                                // Accesses the PARTICLE SYSTEM rTwo COMPONENT
        if (psTwo != null)                                                                                         // If it exists do this thing...
        {
            psTwo.Stop();                                                                                          // Makes sure particle system doesn't play until interacted with
        }
        // BlueOne particle system
        ParticleSystem psThree = bOne.GetComponent<ParticleSystem>();                                                // Accesses the PARTICLE SYSTEM bOne COMPONENT
        if (psThree != null)                                                                                         // If it exists do this thing...
        {
            psThree.Stop();                                                                                          // Makes sure particle system doesn't play until interacted with
        }
        // BlueTwo particle system
        ParticleSystem psFour = bTwo.GetComponent<ParticleSystem>();                                                // Accesses the PARTICLE SYSTEM bTwo COMPONENT
        if (psFour != null)                                                                                         // If it exists do this thing...
        {
            psFour.Stop();                                                                                          // Makes sure particle system doesn't play until interacted with
        }
    }

    /// <summary>
    /// Respawns spheres (and corresponding components and bools) if player dies but has lives left
    /// </summary>
    public void RespawnSpheres()
    {
        // Reset all PICKUPS script objects, components and local bools
        rB.GetComponent<Rigidbody>().useGravity = true;                                 // Return gravity to rigidbody in case died with red sphere still activated
        redActive = false;                                                              // RedActive reset to false
        rS.SetActive(true);                                                             // RED SPHERE reactivated
        bS.SetActive(true);                                                             // BLUE SPHERE reactivated
    }

    // Start is called before the first frame update
    void Start()
    {
        psStop();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
