using UnityEngine;

public class FlyingCameraController : BasicController
{
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float _runningSpeed;

    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;

    [SerializeField] private float _rotationSpeedX;
    [SerializeField] private float _rotationSpeedY;

    private float _speed;

    private float _pitch;
    private float _yaw;

    private void OnValidate()
    {
        if (_minAngle >= _maxAngle)
            _minAngle = _maxAngle - 1;
    }

    private void Awake() =>
        _speed = _walkingSpeed;

    private void Start() =>
        Cursor.lockState = CursorLockMode.Locked;

    private void Update()
    {
        Move();
        Rotate();
    }

    public override void Move()
    {
        string vertical = "Vertical";
        string horizontal = "Horizontal";

        Vector3 move = new Vector3(Input.GetAxis(horizontal), GetDirection(), Input.GetAxis(vertical));

        if (Input.GetKey(KeyCode.LeftShift))
            _speed = _runningSpeed;
        else
            _speed = _walkingSpeed;

        transform.position += transform.TransformDirection(move) * _speed * Time.deltaTime;
    }

    public override void Rotate()
    {
        _yaw += _rotationSpeedX * Input.GetAxis("Mouse X");
        _pitch -= _rotationSpeedY * Input.GetAxis("Mouse Y");

        _pitch = Mathf.Clamp(_pitch, _minAngle, _maxAngle);

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0);
    }

    private float GetDirection()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftControl))
            return Vector3.down.y;
        else if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space))
            return Vector3.up.y;

        return Vector3.zero.y;
    }
}
