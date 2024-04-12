using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private AudioClip thrustClip;
    [SerializeField] public ParticleSystem boosterParticles;
    [SerializeField] public ParticleSystem leftThrusterParticles;
    [SerializeField] public ParticleSystem rightThrusterParticles;
    
    private Rigidbody _myRigidbody;
    private AudioSource _audioSource;
    private Vector3 _thrustVector;
    private Vector3 _rocketRotation;

    
    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        thrustSpeed = 1000.0f;
        rotationSpeed = 100.0f;
        _thrustVector = Vector3.up;
        _rocketRotation = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    
    private void StartThrusting()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(thrustClip);
        }
            
        if (!boosterParticles.isPlaying)
        {
            boosterParticles.Play();
        }

        _myRigidbody.AddRelativeForce(_thrustVector * thrustSpeed * Time.deltaTime);
    }
    
    private void StopThrusting()
    {
        _audioSource.Stop();
        boosterParticles.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            
        }
        else if (Input.GetKey(KeyCode.A))
        {
            StartRotatingLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRotatingRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartRotatingLeft()
    {
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }

        ApplyRotation(rotationSpeed);
    }
    
    private void StartRotatingRight()
    {
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }

        ApplyRotation(-rotationSpeed);
    }
    
    private void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }
    
    private void ApplyRotation(float rotationThisFrame)
    {
        _myRigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(_rocketRotation * rotationThisFrame * Time.deltaTime);
        _myRigidbody.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}


