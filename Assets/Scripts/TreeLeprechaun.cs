using UnityEngine;

public class TreeLeprechaun : MonoBehaviour
{
    [SerializeField] private GameObject _goldCoin;
    [SerializeField] private PileOfGold _pileOfGold;

    private enum MovementState
    {
        None,
        Left,
        Right
    }

    private Vector3 _startingPosition;
    private MovementState _movementState = MovementState.None;
    private float _maximumMovement = 0.85f;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    private void Update()
    {
        switch (_movementState)
        {
            case MovementState.Right:
                transform.position += Vector3.right * Time.deltaTime;
                if (transform.position.x >= _startingPosition.x + _maximumMovement)
                {
                    _goldCoin.SetActive(true);
                    _movementState = MovementState.Left;
                }
                break;
            case MovementState.Left:
                transform.position += Vector3.left * Time.deltaTime;
                if (transform.position.x <= _startingPosition.x)
                {
                    _goldCoin.SetActive(false);
                    _pileOfGold.GoldDelivered();
                    _movementState = MovementState.None;
                }
                break;
        }

    }

    private void OnMouseUpAsButton()
    {
        if (MovementState.None == _movementState)
        {
            _movementState = MovementState.Right;
        }
    }

}
