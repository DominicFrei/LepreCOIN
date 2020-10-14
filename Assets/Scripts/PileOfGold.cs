using TMPro;
using UnityEngine;

public class PileOfGold : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _amount = 0;

    private void Start()
    {
        _text.text = "0";
    }

    public void GoldDelivered(int amount)
    {
        _amount += amount;
        _text.text = _amount + "";
    }

    public int Gather()
    {
        int gold = _amount;
        _amount = 0;
        _text.text = "0";
        return gold;
    }

}
