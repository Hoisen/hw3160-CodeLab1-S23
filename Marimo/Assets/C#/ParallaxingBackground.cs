using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxingBackground : MonoBehaviour
{
    public Transform[] background; //Array for all the back and foreground to be parallaxed
    private float[] parallaxScales; //The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f; //How smooth the parallax is going to be. SET THIS TO ABOVE 0

    private Transform cam; //reference to the main cameras transform
    private Vector3 previousCamPos; //The pos of the camera in the previous frame

    //Is called before start
    void Awake()
    {
        //throw new NotImplementedException();
        //set up the camera reference
        cam = Camera.main.transform;

    }

    // Start is called before the first frame update
    void Start()
    {
        //The pevious frame had the current frame's camera position
        previousCamPos = cam.position;
        parallaxScales = new float[background.Length];

        //Assigning coresponding parallaxScales
        for (int i = 0; i < background.Length; i++)
        {
            parallaxScales[i] = background[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < background.Length; i++)
        {
            //The parallax is th opposite of the camera movement bc the previous frame multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            
            //Set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = background[i].position.x + parallax;
            
            //Create a target position which is the background's current position with its target x position
            Vector3 backgroundTargetPos =
                new Vector3(backgroundTargetPosX, background[i].position.y, background[i].position.z);
            
            //fade bewteen current pos and the target position using lerp
            background[i].position =
                Vector3.Lerp(background[i].position, backgroundTargetPos, smoothing + Time.deltaTime);
        }
        
        //set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;

    }
}
