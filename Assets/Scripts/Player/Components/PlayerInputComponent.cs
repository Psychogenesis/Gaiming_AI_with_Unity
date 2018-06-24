using UnityEngine;

public class PlayerInputComponent : MonoBehaviour {

    public float RotationAngle;
    public float panSpeed = 20f;
    public float rotationSpeed = 2f;
    public float panBorderThicness = 40f;
    public float zoomDistanceMax = 140f;
    public float zoomDistanceMin = 20f;
    public float zoomSpeed = 20f;
    public Vector2 panLimit;
}
