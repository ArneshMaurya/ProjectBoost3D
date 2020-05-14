using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockectBoost : MonoBehaviour {
    Rigidbody rigidBody;
    AudioSource m_MyAudioSource;

    //Play the music
    bool m_Play;
    bool m_ToggleChange;
    [SerializeField] float rcsTrust = 100f;
    [SerializeField] float mainTrust = 80f;

    void Start () {
        rigidBody = GetComponent<Rigidbody> ();
        m_MyAudioSource = GetComponent<AudioSource> ();
        m_Play = true;
    }

    // Update is called once per frame
    void Update () {
        Thrust ();
        Rotate ();
        //TouchControll ();
    }

    private void TouchControll () {

        print ("IN side TouchControll");

        if ((Input.touchCount > 0) && (Input.GetTouch (0).phase == TouchPhase.Began)) {
            Ray raycast = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
            RaycastHit raycastHit;
            print ("IN side 1ft if");
            if (Physics.Raycast (raycast, out raycastHit)) {
                Debug.Log ("Something Hit");
                if (raycastHit.collider.name == "Soccer") {
                    Debug.Log ("Soccer Ball clicked");
                }

                //OR with Tag

                if (raycastHit.collider.CompareTag ("UpKey")) {
                    Debug.Log ("Upkey clicked");
                }

                if (raycastHit.collider.CompareTag ("RightKey")) {
                    Debug.Log ("Rightkey clicked");
                }

                if (raycastHit.collider.CompareTag ("LeftKey")) {
                    Debug.Log ("Leftkey clicked");
                }

            }
        }
    }

    void OnCollisionEnter (Collision collision) {

        foreach (ContactPoint contact in collision.contacts) {
            Debug.DrawRay (contact.point, contact.normal, Color.white);
        }

        switch (collision.gameObject.tag) {
            case "Friendly":
                print ("alive");
                break;
            case "Fuel":
                //
                break;
            default:
                print ("Dead !!");
                break;
        }

    }

    private void Rotate () {

        if (Input.GetKey (KeyCode.A)) {
            //rigidBody.AddRelativeForce(Vector3.left);
            //transform.forward += Vector3.forward * Time.deltaTime;
            float rotationSpeed = rcsTrust * Time.deltaTime;
            transform.Rotate (Vector3.forward * rotationSpeed);
            print ("A");

        } else if (Input.GetKey (KeyCode.D)) {
            //rigidBody.AddRelativeForce(Vector3.right);
            //transform.forward += Vector3.forward * Time.deltaTime;
            float rotationSpeed = rcsTrust * Time.deltaTime;
            transform.Rotate (-Vector3.forward * rotationSpeed);
            print ("D");

        }
    }

    private void Thrust () {
        rigidBody.freezeRotation = true; //to stop the usless rotation
        if (Input.GetKey (KeyCode.Space)) {
            Debug.Log ("Space key was pressed.");
            float rockectSpeed = mainTrust * Time.deltaTime;
            rigidBody.AddRelativeForce (Vector3.up * rockectSpeed);

            if (!m_MyAudioSource.isPlaying)
                m_MyAudioSource.Play ();
        } else {
            m_MyAudioSource.Stop ();
        }
        rigidBody.freezeRotation = false; //to stop the usless rotation
    }

}