using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerOrientation : MonoBehaviour
{
    private PlayerMovement _PlayerMovement;
    private Transform _SpriteTransform;

    public float _AngleLerp;

    private void Awake()
    {
        _PlayerMovement = GetComponent<PlayerMovement>();
        _SpriteTransform = transform.GetChild(0);
    }

    private void Update()
    {
        Vector2 MoveDirection = _PlayerMovement.GetMoveDirection();
        float targetAngle = Mathf.Acos(Vector2.Dot(MoveDirection, new Vector2(0.0f, 1.0f))) / Mathf.PI * 180.0f;
        if(Vector2.Dot(MoveDirection, new Vector2(1.0f, 0.0f)) > 0.0f)
        {
            targetAngle = -targetAngle;
        }

        _SpriteTransform.rotation = Quaternion.Lerp(_SpriteTransform.rotation, Quaternion.Euler(new Vector3(0.0f, 0.0f, targetAngle)), _AngleLerp);
    }
}
