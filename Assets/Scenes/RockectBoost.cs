﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockectBoost : MonoBehaviour
{
    Rigidbody rigidBody;
    Vector3 m_EulerAngleVelocity;
    AudioSource m_MyAudioSource;

    //Play the music
    bool m_Play;
    //Detect when you use the toggle, ensures music isn’t played multiple times
    bool m_ToggleChange;


    // Start is called before the first frame update
    void Start()
    {
     rigidBody = GetComponent<Rigidbody>();
      //m_EulerAngleVelocity = new Vector3(0, 100, 0);
        
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
        m_Play = true;
    }

    // Update is called once per frame
         void Update()
    {

        // Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        //rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
        

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space key was pressed.");
            rigidBody.AddRelativeForce(Vector3.up);

            if(!m_MyAudioSource.isPlaying)
            m_MyAudioSource.Play();
        }else{
            m_MyAudioSource.Stop();
        }

        if(Input.GetKey(KeyCode.A))
        {   
            //rigidBody.AddRelativeForce(Vector3.left);
            //transform.forward += Vector3.forward * Time.deltaTime;
            transform.Rotate(transform.forward);
            print("A");
            

       }else if(Input.GetKey(KeyCode.D)){
            //rigidBody.AddRelativeForce(Vector3.right);
            //transform.forward += Vector3.forward * Time.deltaTime;
            transform.Rotate(-transform.forward);
            print("D");
            
        }

    }
    
}