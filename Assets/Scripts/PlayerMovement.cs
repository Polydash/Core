using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 _MoveDirection;
    private Rigidbody2D _Rigidbody;
    private const float _DeadZone = 0.1f;
    
    public float _MovementForce;

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _MoveDirection = new Vector2(0.0f, 1.0f);
    }

    private void FixedUpdate()
    {
        //Simple movement using AddForce and linear drag
        Vector2 InputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(InputDirection.magnitude >= _DeadZone)
        {
            _MoveDirection = InputDirection;
            _MoveDirection.Normalize();
            _Rigidbody.AddForce(_MovementForce * _MoveDirection * Time.fixedDeltaTime);
        }
    }

    public Vector2 GetMoveDirection()
    {
        return _MoveDirection;
    }
}
