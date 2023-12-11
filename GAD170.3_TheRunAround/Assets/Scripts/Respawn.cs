using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Player pRef;
    public TP tpRef;
    public PickUps pURef;
    public Boxes bRef;
    public SlidingDoor sDRef;
    public PlayerMovement pMRef;
    public Gate gRef;

    public delegate void RespawnDelegates();
    public static RespawnDelegates respawnDelegates;

    public void OnEnable()
    {
        respawnDelegates += pRef.PlayerRespawn;
        respawnDelegates += pURef.RespawnSpheres;
        respawnDelegates += bRef.RespawnBoxes;
        respawnDelegates += sDRef.RespawnCageDoor;
        respawnDelegates += gRef.GateRespawn;
        respawnDelegates += pMRef.GravityRespawn;
    }
    public void OnDisable()
    {
        // Remove at end
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If player is dead and has lives left respawn   
    }
}
