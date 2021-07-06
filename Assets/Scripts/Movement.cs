using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody _rigidbody;

    [SerializeField] private float thrust = 20;
    [SerializeField] private float rotation = 20;
    
    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        ProcessThrust();
        ProcessRotation();
    }
    
    private void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            _rigidbody.AddRelativeForce(Vector3.up * (thrust * Time.deltaTime));
        }
    }
    
    private void ProcessRotation() {
        if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotation);
        } else if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotation);
        }
    }

    private void ApplyRotation(float rotationAmount) {
        transform.Rotate(0, 0, rotationAmount * Time.deltaTime);
    }
}
