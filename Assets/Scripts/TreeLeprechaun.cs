using UnityEngine;

public class TreeLeprechaun : MonoBehaviour
{
    [SerializeField] private GameObject _goldCoin;
    [SerializeField] private PileOfGold _sourcePileOfGold;
    [SerializeField] private PileOfGold _targetPileOfGold;
    [SerializeField] private float _maximumMovement = 0.0f;
    [SerializeField] private MovementState _initialMovementDirection = MovementState.NotSet;

    private MovementState _reversedMovementState = MovementState.NotSet;
    private int _gold;

    private enum MovementState : int
    {
        NotSet,
        Left = -1, 
        None = 0,
        Right = 1
    }

    private Vector3 _startingPosition;
    private MovementState _movementState = MovementState.None;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Update()
    {
        if (_movementState == _initialMovementDirection)
        {
            transform.position += new Vector3((float)_movementState, 0, 0) * Time.deltaTime;
            if (Mathf.Abs(transform.position.x - _startingPosition.x) >= _maximumMovement)

            {
                _goldCoin.SetActive(true);
                if (null != _sourcePileOfGold)
                {
                    _gold = _sourcePileOfGold.Gather();
                }
                else
                {
                    _gold++;
                }

                _movementState = _reversedMovementState;
            }
        }
        else if (_movementState == _reversedMovementState)
        {
            transform.position += new Vector3((float)_movementState, 0, 0) * Time.deltaTime;
            if (MovementState.Left == _reversedMovementState && transform.position.x <= _startingPosition.x ||
                MovementState.Right == _reversedMovementState && transform.position.x >= _startingPosition.x)
            {
                _goldCoin.SetActive(false);
                _targetPileOfGold.Deliver(_gold);
                _gold = 0;
                _movementState = MovementState.None;
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("click");
        if (MovementState.None == _movementState)
        {
            _movementState = _initialMovementDirection;
            switch (_movementState)
            {
                case MovementState.Right:
                    _reversedMovementState = MovementState.Left;
                    break;
                case MovementState.Left:
                    _reversedMovementState = MovementState.Right;
                    break;
            }
        }
    }

}
