using UnityEngine;

public class TreeLeprechaun : MonoBehaviour
{
    [SerializeField] private GameObject _goldCoin;
    [SerializeField] private PileOfGold _sourcePileOfGold;
    [SerializeField] private PileOfGold _targetPileOfGold;
    [SerializeField] private float _maximumMovement = 0.0f;
    [SerializeField] private MovementState _initialMovementDirection = MovementState.NotSet;

    private enum MovementState : int
    {
        NotSet,
        Left = -1,
        None = 0,
        Right = 1
    }
    private MovementState _movementState = MovementState.None;
    private MovementState _reversedMovementState = MovementState.NotSet;
    private Vector3 _startingPosition;
    private int _gold;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Update()
    {
        if (!_goldCoin || !_targetPileOfGold)
        {
            Debug.LogError("At least one GameObject is NULL.");
            return;
        }

        float movementDirection = (float)_movementState;
        Vector3 movementChange = new Vector3(movementDirection, 0, 0) * Time.deltaTime;
        if (_movementState == _initialMovementDirection)
        {
            transform.position += movementChange;
            if (Mathf.Abs(transform.position.x - _startingPosition.x) >= _maximumMovement)
            {
                _goldCoin.SetActive(true);
                if (_sourcePileOfGold) // This isn't checked above because it is valid to be null.
                {
                    _gold += _sourcePileOfGold.Gather();
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
            transform.position += movementChange;
            // If we're moving left we need to check if we're already to the left of the starting position.
            // Same applies to a movement in the other direction.
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
        // We only make a change if we are not moving already.
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
