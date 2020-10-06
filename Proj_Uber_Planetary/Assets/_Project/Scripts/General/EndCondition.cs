using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UberPlanetary.Currency;


public class EndCondition : MonoBehaviour
{
    public int goalAmount;

    private CurrencyManager _currencyMngr;
    public Clock clock;

    private void Awake()
    {
        _currencyMngr = GetComponent<CurrencyManager>();
    }
    private void Start()
    {
        _currencyMngr.OnValueChanged.AddListener(CheckWin);
        clock.OnTimeUp.AddListener(Lose);
    }

    void CheckWin(int money)
    {
        if (money < goalAmount) return;

        Win();
    }

    public void Win()
    {
        Debug.Log("YOU DID IT! You paid your debt to those suckers and now you're free!");
    }
    public void Lose()
    {
        Debug.Log("You couldn't pay in time and now you'll sleep with the fishes");
    }
}
