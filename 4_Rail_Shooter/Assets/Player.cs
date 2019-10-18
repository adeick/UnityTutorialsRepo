using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float speed = 30f;
    [Tooltip("In ms^-1")][SerializeField] float xRange = 2.5f;
    [Tooltip("In ms^-1")][SerializeField] float yRange = 1.5f;
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -5f;
    [SerializeField] float positionYawFactor = -5f;
    [SerializeField] float controlRollFactor = -10f;

    float xThrow, yThrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TranslationMovement();
        RotationalMovement();

    }
    void TranslationMovement(){
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffset;
        float rawNewYPos = transform.localPosition.y + yOffset;

        rawNewXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
        rawNewYPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);

        transform.localPosition = new Vector3(rawNewXPos, rawNewYPos, transform.localPosition.z);
    }

    void RotationalMovement(){
        float pitch, yaw, roll;
        float controlPitch;

        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor ;
        yaw = transform.localPosition.x * positionYawFactor;

        if(xRange - Mathf.Abs(transform.localPosition.x) < 0.1){
            roll = 0;
        }
        else{
            roll = xThrow * controlRollFactor;
        }
        if(yRange - Mathf.Abs(transform.localPosition.y) < 0.1){
            controlPitch = 0;
        }
        else{
            controlPitch = yThrow * controlPitchFactor;
        }
        pitch = pitchDueToPosition + controlPitch;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
