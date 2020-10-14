using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftLeprechaun : MonoBehaviour
{
    private enum MovementState
    {
        None,
        Up,
        Down
    }

    private Vector3 _startingPosition;
    private MovementState _movementState = MovementState.None;
    private float _maximumMovement = 2.0f;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Update()
    {
        switch (_movementState)
        {
            case MovementState.Down:
                transform.position += Vector3.down * Time.deltaTime;
                if (transform.position.y <= _startingPosition.y - _maximumMovement)
                {
                    _movementState = MovementState.Up;
                }
                break;
            case MovementState.Up:
                transform.position += Vector3.up * Time.deltaTime;
                if (transform.position.y >= _startingPosition.y)
                {
                    //_pileOfGold.GoldDelivered();
                    _movementState = MovementState.None;
                }
                break;
        }

    }

    private void OnMouseUpAsButton()
    {
        if (MovementState.None == _movementState)
        {
            _movementState = MovementState.Down;
        }
    }
}
