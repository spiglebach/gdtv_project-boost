using UnityEngine;

public class Oscillator : MonoBehaviour {
    const float Tau = Mathf.PI * 2;
    private Vector3 startingPosition;
    [SerializeField] private Vector3 movementVector;
    [SerializeField][Range(0,1)] private float movementFactor;
    [SerializeField] private float period = 2;

    void Start() {
        startingPosition = transform.position;
    }

    void Update() {
        float cycles = Time.time / period;
        float rawSin = Mathf.Sin(cycles * Tau);
        movementFactor = (rawSin + 1f) / 2f;
        var offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
