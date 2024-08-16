using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int rewardAmnt = 25;
    [SerializeField] int penaltyAmnt = 25;

    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void Reward(){
        if(bank ==null) return;
        bank.Deposit(rewardAmnt);
    }

    public void Penalty(){
        bank.Withdraw(penaltyAmnt);
    }
}
