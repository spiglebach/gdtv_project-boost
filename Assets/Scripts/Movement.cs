using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip engineThrustClip;
    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem rightEngineParticles;
    [SerializeField] private ParticleSystem leftEngineParticles;

    [SerializeField] private float thrust = 20;
    [SerializeField] private float rotation = 20;
    
    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        ProcessThrust();
        ProcessRotation();
    }
    
    private void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            _rigidbody.AddRelativeForce(Vector3.up * (thrust * Time.deltaTime));
            if (!_audioSource.isPlaying) {
                _audioSource.PlayOneShot(engineThrustClip);
            }
            if (!mainEngineParticles.isPlaying) {
                mainEngineParticles.Play();
            }
        } else {
            _audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }
    
    private void ProcessRotation() {
        if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotation);
            rightEngineParticles.Stop();
            if (!leftEngineParticles.isPlaying) {
                leftEngineParticles.Play();
            }
        } else if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotation);
            leftEngineParticles.Stop();
            if (!rightEngineParticles.isPlaying) {
                rightEngineParticles.Play();
            }
        } else {
          leftEngineParticles.Stop();
          rightEngineParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationAmount) {
        _rigidbody.freezeRotation = true; // take away control from the physics system temporarily
        transform.Rotate(0, 0, rotationAmount * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }

    public void DisableMovement() {
        _audioSource.Stop();
        mainEngineParticles.Stop();
        rightEngineParticles.Stop();
        leftEngineParticles.Stop();
        enabled = false;
    }
}
