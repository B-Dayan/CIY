using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _speed = 5;
    [SerializeField] private float _deathIconTimer = 0.1f;
    private Rigidbody2D _rb;
    private SpriteRenderer _rend;
    private GameObject _deathIcon;
    private PlayerControls _controls;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rend = GetComponent<SpriteRenderer>();
        _deathIcon = transform.GetChild(0).gameObject;

        _controls = MenuManager.controls;
        _controls.Player.Move.performed += OnMove;
        _controls.Player.Move.canceled += OnStopMoving;
    }

    private void FixedUpdate()
    {
        WrapAroundBounds();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        var movementDirection = ctx.ReadValue<Vector2>();
        _rb.AddForce(movementDirection * _speed, ForceMode2D.Impulse);
    }

    private void OnStopMoving(InputAction.CallbackContext ctx)
    {
        _rb.linearVelocity = Vector2.zero;
    }

    public void OnLoadLevel()
    {
        _rb.position = GameInfo.GetResetPos();
    }

    public void OnDeath()
    {
        StartCoroutine( ShowDeathIcon() );
    }

    private IEnumerator ShowDeathIcon()
    {
        _deathIcon.SetActive(true);
        _rb.position = GameInfo.GetResetPos();
        yield return new WaitForSeconds(_deathIconTimer);
        _deathIcon.SetActive(false);
    }

    private void WrapAroundBounds()
    {
        // If the player is out of the camera's view, teleport them to the opposite side of the screen

        var pos = Camera.main.WorldToViewportPoint(transform.position);
        var newPos = pos;

        if (pos.x < 0)
        {
            newPos.x = 1;
        }
        else if (pos.x > 1)
        {
            newPos.x = 0;
        }
        else if (pos.y < 0)
        {
            newPos.y = 1;
        }
        else if (pos.y > 1)
        {
            newPos.y = 0;
        }
        else
        {
            // If the player is in bounds, don't teleport them
            return;
        }
        
        transform.position = Camera.main.ViewportToWorldPoint(newPos);

        if (GameInfo.isLevelObjectiveMet)
        {
            GameInfo.LoadNextLevel();
        }
    }

    private void OnEnable()
    {
        GameInfo.UpdateLevel.AddListener( OnLoadLevel );
        GameInfo.PlayerDies.AddListener( OnDeath );
    }

    private void OnDisable()
    {
        GameInfo.UpdateLevel.RemoveListener( OnLoadLevel );
        GameInfo.PlayerDies.RemoveListener( OnDeath );
    }
}