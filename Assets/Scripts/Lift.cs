using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private GameObject _goldCoin;
    [SerializeField] private PileOfGold _sourcePileOfGold;
    [SerializeField] private PileOfGold _targetPileOfGold;

    private enum MovementState
    {
        None,
        Up,
        Down
    }

    private Vector3 _startingPosition;
    private MovementState _movementState = MovementState.None;
    private float _maximumMovement = 2.0f;
    private int _gold;

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
                    _goldCoin.SetActive(true);
                    _gold = _sourcePileOfGold.Gather();
                    _movementState = MovementState.Up;
                }
                break;
            case MovementState.Up:
                transform.position += Vector3.up * Time.deltaTime;
                if (transform.position.y >= _startingPosition.y)
                {
                    _goldCoin.SetActive(false);
                    _targetPileOfGold.Deliver(_gold);
                    _gold = 0;
                    _movementState = MovementState.None;
                }
                break;
        }

    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("click");
        if (MovementState.None == _movementState)
        {
            _movementState = MovementState.Down;
        }
    }
}
