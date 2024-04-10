using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _myRigidbody;
    private Vector3 _thrustVector;
    private Vector3 _rocketRotation;
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
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
            _myRigidbody.AddRelativeForce(_thrustVector * thrustSpeed * Time.deltaTime);
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
