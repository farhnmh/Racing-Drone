using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform drone;
    [SerializeField] Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(drone.position.x - offset.x,
                                              drone.position.y - offset.y,
                                              drone.position.z - offset.z);
    }
}
