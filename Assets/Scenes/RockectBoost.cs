using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockectBoost : MonoBehaviour {
    Rigidbody rigidBody;
    AudioSource m_MyAudioSource;

    enum State { Alive, Dying, Trans }
    State state = State.Alive;

    //Play the music
    bool m_Play;
    bool m_ToggleChange;
    [SerializeField] float rcsTrust = 100f;
    [SerializeField] float mainTrust = 80f;

    [SerializeField] AudioClip mainEng;
    [SerializeField] AudioClip lvlFinish;
    [SerializeField] AudioClip DeadS;

    [SerializeField] ParticleSystem mainEngParticle;
    [SerializeField] ParticleSystem lvlFinishParticle;
    [SerializeField] ParticleSystem DeadSParticle;

    void Start () {
        rigidBody = GetComponent<Rigidbody> ();
        m_MyAudioSource = GetComponent<AudioSource> ();
        m_Play = true;
    }

    // Update is called once per frame
    void Update () {
        //todo somewhere stop sound on death
        if (state == State.Alive) {
            Thrust ();
            Rotate ();
        }
        //TouchControll ();
    }

    void OnCollisionEnter (Collision collision) {

        if (state != State.Alive) {
            return;
        }

        switch (collision.gameObject.tag) {
            case "Friendly":
                print ("alive");
                break;
            case "Finish":
                startSuccusSeq();
                break;
            case "FinalF":
                lvlFinishParticle.Play();
                break;
            default:
                print ("Dead !!");
                deathSeq();
                break;
        }

    }

    private void startSuccusSeq(){
        state = State.Trans;
        m_MyAudioSource.PlayOneShot (lvlFinish);
        lvlFinishParticle.Play();
        Invoke ("nextLvl", 1f); //paramaterize time
    }

    private void deathSeq(){
        state = State.Dying;
        m_MyAudioSource.Stop ();
        mainEngParticle.Stop();
        m_MyAudioSource.PlayOneShot (DeadS);
        DeadSParticle.Play();
        Invoke ("LoadFirstLvl", 1f); //paramaterize time
    }

    private void nextLvl () {

        UnityEngine.SceneManagement.SceneManager.LoadScene (1); //todo allow for more level

    }

    private void LoadFirstLvl () {
        UnityEngine.SceneManagement.SceneManager.LoadScene (0); //todo allow for more level
    }

    private void Rotate () {

        if (Input.GetKey (KeyCode.A)) {
            float rotationSpeed = rcsTrust * Time.deltaTime;
            transform.Rotate (Vector3.forward * rotationSpeed);
            print ("A");

        } else if (Input.GetKey (KeyCode.D)) {
            float rotationSpeed = rcsTrust * Time.deltaTime;
            transform.Rotate (-Vector3.forward * rotationSpeed);
            print ("D");

        }
    }

    private void Thrust () {
        
        if(state==State.Dying)
        m_MyAudioSource.Stop();

        rigidBody.freezeRotation = true; //to stop the usless rotation
        if (Input.GetKey (KeyCode.Space)) {
            ApplyTrust ();
        } else {
            m_MyAudioSource.Stop ();
            mainEngParticle.Stop();
        }
        rigidBody.freezeRotation = false; //to stop the usless rotation
    }

    private void ApplyTrust () {

        float rockectSpeed = mainTrust * Time.deltaTime;
        rigidBody.AddRelativeForce (Vector3.up * rockectSpeed);
        
        if (!m_MyAudioSource.isPlaying)
            m_MyAudioSource.PlayOneShot (mainEng);

        mainEngParticle.Play();
    }

}