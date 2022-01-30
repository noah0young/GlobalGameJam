using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    public Transform backgroundPosition;

    public float parallaxScalingX;
    public float parallaxScalingY;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        GetComponent<Transform>().position += new Vector3(parallaxScalingX * deltaMovement.x, parallaxScalingY * deltaMovement.y, 0);
        lastCameraPosition = cameraTransform.position;
    }
}
