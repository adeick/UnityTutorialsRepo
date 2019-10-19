using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float speed = 20f;
    [Tooltip("In ms^-1")][SerializeField] float xRange = 10f;
    [Tooltip("In ms^-1")][SerializeField] float yRange = 9f;
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -25f;
    [SerializeField] float positionYawFactor = 6f;
    [SerializeField] float controlRollFactor = -25f;

    float xThrow, yThrow;
    // Start is called before the first frame update
    bool dead;
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead){
            TranslationMovement();
            RotationalMovement();
        }
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
            roll = controlRollFactor * (xRange - Mathf.Abs(transform.localPosition.x));
        }
        else{
            roll = xThrow * controlRollFactor;
        }
        if(yRange - Mathf.Abs(transform.localPosition.y) < 0.1){
            controlPitch = controlPitchFactor * (yRange - Mathf.Abs(transform.localPosition.y));
        }
        else{
            controlPitch = yThrow * controlPitchFactor;
        }
        pitch = pitchDueToPosition + controlPitch;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void Explosion(){
        dead = true;
        print("Boom");
    }
}
