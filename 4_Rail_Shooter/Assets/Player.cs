using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float speed = 20f;

    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 4f;
    [Tooltip("In ms^-1")][SerializeField] float xRange = 5f;
    [Tooltip("In ms^-1")][SerializeField] float ySpeed = 4f;
    [Tooltip("In ms^-1")][SerializeField] float yRange = 5f;
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = -5f;
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
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffset;
        float rawNewYPos = transform.localPosition.y + yOffset;

        rawNewXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);
        rawNewYPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);

        transform.localPosition = new Vector3(rawNewXPos, rawNewYPos, transform.localPosition.z);
    }

    void RotationalMovement(){

        float pitch = transform.localPosition.y * positionPitchFactor;

        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = 0f;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
