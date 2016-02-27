using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class AtomMovement : MonoBehaviour
{
    private Rigidbody2D _Rigidbody;
    private PlayerMovement _Player;
    private List<AtomMovement> _AtomsList;
    private Vector2 _MoveDirection;
    private const float _MoveDirectionThreshold = 1000.0f;
    private bool _IsShot;
    private float _ShotTimestamp;

    [Header("Idle")]
    public float _AttractSpeed;
    public float _PlayerRepulseSpeed;
    public float _PlayerRepulseDistance;
    public float _AtomRepulseSpeed;
    public float _AtomRepulseDistance;

    [Header("Shot")]
    public float _ShootSpeed;
    public float _ShootDuration;

    private void Awake()
    {
        _IsShot = false;
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(_IsShot)
        {
            ShotMovement();
        }
        else
        {
            IdleMovement();
        }
    }

    private void ShotMovement()
    {
        if(Time.time - _ShotTimestamp > _ShootDuration)
        {
            _IsShot = false;
            _AtomsList.Add(this);
        }
    }

    private void IdleMovement()
    {
        Vector3 MoveDirection = Vector3.zero;

        for (int i = 0; i < _AtomsList.Count; ++i)
        {
            if (_AtomsList[i] != this)
            {
                Vector3 atomDirection = _AtomsList[i].transform.position - transform.position;
                if (atomDirection.magnitude <= _AtomRepulseDistance)
                {
                    atomDirection.Normalize();
                    MoveDirection -= atomDirection * _AtomRepulseSpeed;
                }
            }
        }

        Vector3 playerDirection = _Player.transform.position - transform.position;
        float playerDistance = playerDirection.magnitude;
        playerDirection.Normalize();
        if (playerDistance <= _PlayerRepulseDistance)
        {
            MoveDirection -= playerDirection * _PlayerRepulseSpeed;
        }

        Vector3 playerDirectionAnticipation = _Player.GetMoveDirectionAnticipation();
        Vector3 attractDirection = _Player.transform.position + playerDirectionAnticipation - transform.position;
        attractDirection.Normalize();
        MoveDirection += attractDirection * _AttractSpeed;

        _Rigidbody.AddForce(MoveDirection * Time.fixedDeltaTime);

        if (MoveDirection.magnitude > _MoveDirectionThreshold)
        {
            MoveDirection.Normalize();
            _MoveDirection = MoveDirection;
        }
    }

    public void Init(PlayerMovement player, List<AtomMovement> atomsList)
    {
        _Player = player;
        _AtomsList = atomsList;
    }

    public Vector2 GetMoveDirection()
    {
        return _MoveDirection;
    }

    public void Shoot(Vector2 direction)
    {
        _IsShot = true;
        _AtomsList.Remove(this);
        direction.Normalize();
        _Rigidbody.AddForce(direction * _ShootSpeed, ForceMode2D.Impulse);
        _ShotTimestamp = Time.time;
    }
}
