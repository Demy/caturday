using UnityEngine;

[RequireComponent(typeof(CatMovement))]
[RequireComponent(typeof(CatInteraction))]
public class KeyboardController : MonoBehaviour
{
    public GameObject hint;

    private CatMovement _movable;
    private CatInteraction _character;
    private Vector2 _speed;
    private bool _isActive;

    void Start()
    {
        _movable = GetComponent<CatMovement>();
        _character = GetComponent<CatInteraction>();

        _isActive = true;
    }

	void Update ()
	{
	    if (_isActive)
	    {
	        MoveAround();
	        Interact();
	        ShowHint();
	    }
	}

    private void MoveAround()
    {
        bool isMovingLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isMovingRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool isMovingUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isMovingDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        _speed.Set(0, 0);
        if (isMovingLeft && !isMovingRight) _speed.x = -1;
        if (!isMovingLeft && isMovingRight) _speed.x = 1;
        if (isMovingUp && !isMovingDown) _speed.y = 1;
        if (!isMovingUp && isMovingDown) _speed.y = -1;
        _movable.Move(_speed);
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            _character.InteractWithClosest();
        }
    }

    private void ShowHint()
    {
        Interactable closest = _character.FindClosest();
        if (closest != null && closest.IsUsable())
        {
            hint.SetActive(true);
            UIUtils.PlaceUIObjectNear(hint.gameObject, _character.gameObject);
        }
        else
        {
            hint.SetActive(false);
        }
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public void SetActive(bool value)
    {
        _isActive = value;
        if (!value)
        {
            hint.SetActive(false);
            _speed.Set(0, 0);
            _movable.Move(_speed);
        }
    }
}
