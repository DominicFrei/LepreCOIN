using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PileOfGold : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _amount = 0;

    private void Start()
    {
        _text.text = "0";
    }

    public void GoldDelivered()
    {
        _amount++;
        _text.text = _amount + "";
    }
}
