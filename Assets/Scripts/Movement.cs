using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private AudioClip thrustClip;
    
    private Rigidbody _myRigidbody;
    private AudioSource _audioSource;
    private Vector3 _thrustVector;
    private Vector3 _rocketRotation;
    private ParticleSystem _boosterParticles;
    private ParticleSystem _leftThrusterParticles;
    
    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _boosterParticles = GetComponent<CollisionHandler>().boosterParticles;
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
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(thrustClip);
            }
            
            if (!_boosterParticles.isPlaying)
            {
                _boosterParticles.Play();
            }

            _myRigidbody.AddRelativeForce(_thrustVector * thrustSpeed * Time.deltaTime);
        }
        else
        {
            _audioSource.Stop();
            _boosterParticles.Stop();
        }
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _myRigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(_rocketRotation * rotationThisFrame * Time.deltaTime);
        _myRigidbody.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
