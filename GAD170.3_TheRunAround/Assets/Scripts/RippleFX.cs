using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This contains a script that I sourced and then edited to make water mesh appear to be flowing in and out.  
///  It uses a sine wave.  
///  I have added comments in to explain this scripts processes.
/// </summary>
public class RippleFX : MonoBehaviour
{
    // rippleSpeed is how fast the sign wave moves
    public float rippleSpeed = 1.0f;

    // rippleStrength is how high the water moves.  In this instance I have chosen 0.1
    // This makes the water apear to be seeping through the cracks of the stone floor before receeding back again and repeating
    public float rippleStrength = 0.1f; 

    // rippleWavelength
    public float rippleWavelength = 1.0f;

    private Vector3[] originalVertices;
    private Vector3[] displacedVertices;
    private MeshFilter meshFilter;

    void Start()
    {
        // This grabs the mesh filter from the object and alters its state
        meshFilter = GetComponent<MeshFilter>();
        originalVertices = meshFilter.mesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        // This defines the speed of the sine wave
        float time = Time.time * rippleSpeed;

        // This creates a loop/repeat code
        // This will continue to loop as conditions are not met
        for (int i = 0; i < originalVertices.Length; i++)
        {
            // The following defines elements and parameters 
            Vector3 originalVertex = originalVertices[i];
            Vector3 displacedVertex = originalVertex;

            // Applies the ripple effect using a sine wave (Mathf.Sin) on xyz axis by rippleStrength 
            displacedVertex.y += Mathf.Sin(time + originalVertex.x + originalVertex.z) * rippleStrength;

            displacedVertices[i] = displacedVertex;
        }

        // Updates the mesh with the displaced vertices
        meshFilter.mesh.vertices = displacedVertices;
        meshFilter.mesh.RecalculateNormals();
    }
}