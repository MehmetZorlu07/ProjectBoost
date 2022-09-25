using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            StartThrusting();
        }
        else {
            StopThrusting();
            }
    }

    void StartThrusting() {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);  
        if(!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        if(!mainEngineParticles.isPlaying) {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting(){
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame) {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
