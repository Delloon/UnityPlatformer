using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Настройки для передвежения персонажа")]
    [SerializeField] private float _speed = 5f; // Скорость движения персонажа
    [SerializeField] private float _jumpForce = 10f; // Сила прыжка
    [SerializeField] private Transform _groundCheck; // Точка для проверки, касается ли персонаж земли
    [SerializeField] private LayerMask _groundLayer; // Слой земли для определения касания земли
    [SerializeField] private SpriteRenderer _playerSprite; //Спрайт нашего персонажа

    private Rigidbody2D _rigidBody;
    private bool _isGrounded;

    private Vector3 _input;
    private bool _isMoving;

    private Animations _animations;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animations = GetComponentInChildren<Animations>();
    }

    void Update()
    {
        _animations.IsMoving = _isMoving;
        _animations.IsFlying = IsFlying();

        GroundCheck();

        Move();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void Move()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (!IsFlying())
        {
            Vector2 movement = new Vector2(_input.x * _speed, _rigidBody.velocity.y);
            _rigidBody.velocity = movement;
        }

        _isMoving = _input.x != 0 ? true : false;

        if (_isMoving)
        {
            _playerSprite.flipX = _input.x > 0 ? false : true;
        }
    }


    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
            _animations.Jump();
        }
    }

    private bool IsFlying()
    {
        if (_rigidBody.velocity.y < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}