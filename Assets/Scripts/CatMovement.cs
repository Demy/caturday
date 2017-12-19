using UnityEngine;

public class CatMovement : MonoBehaviour
{
    private const float ALLOWED_PAUSE = 1f;
    private float step = 0.07f;

    [SerializeField]
    private Transform exitPlace;

    private Animator _animator;
    private Vector3 _speed;
    private const float isometricDif = 0.66f;

    private Rigidbody body;

    private float timeStill = 0;
    private float timeRunning = 0;

    private bool _isPaused = false;
    private bool _isQuiting = false;

    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 speed)
    {
        _speed = speed.normalized * step;
        _speed.z = _speed.y;
        _speed.y /= isometricDif;
    }

    void FixedUpdate()
    {
        if (!_isPaused)
        {
            if (_speed.sqrMagnitude == 0)
            {
                timeStill += Time.deltaTime;
                ClearRunninfTime(Time.deltaTime);
            }
            else
            {
                AddRunninfTime(Time.deltaTime);
                timeStill = 0;
                transform.Translate(_speed);
                transform.Translate(new Vector3(0, transform.position.z * isometricDif - transform.position.y, 0));
            }
        }
        SetWalkAnimation();
        body.velocity = new Vector3();
    }

    private void ClearRunninfTime(float deltaTime)
    {
        if (!_isQuiting && timeStill >= ALLOWED_PAUSE)
        {
            timeRunning = 0;
        }
    }

    private void AddRunninfTime(float deltaTime)
    {
        if (_isQuiting) return;
        if (timeStill < ALLOWED_PAUSE)
        {
            timeRunning += deltaTime;
        }
        else
        {
            timeRunning = deltaTime;
        }
    }

    private void SetWalkAnimation()
    {
        _animator.SetFloat("Speed", _speed.magnitude);
    }

    public void NavigateToExit()
    {
        _isQuiting = true;
        Move(exitPlace.position - transform.position);
    }

    public float GetTimeRunning()
    {
        return timeRunning;
    }

    public void IsPaused(bool value)
    {
        _isPaused = value;
    }
}