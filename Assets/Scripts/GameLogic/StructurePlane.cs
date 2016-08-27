using UnityEngine;
using System.Collections;

public class StructurePlane : MonoBehaviour
{
    const float C_SNAP_SIZE = 1.5f;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 newPosition = transform.position;
            newPosition.z -= 3f;
            transform.position = newPosition; 
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 newPosition = transform.position;
            newPosition.z += 3f;
            transform.position = newPosition; 

        }
    }

}
