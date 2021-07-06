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
            transform.Rotate(0, 0, -rotation * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(0, 0, rotation * Time.deltaTime);
        }
    }
}
