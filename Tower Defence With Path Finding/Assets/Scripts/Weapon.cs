using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int towerCost = 75;

    public bool CreateTower(Weapon weapon, Vector3 position){
        Bank bank = FindObjectOfType<Bank>();

        if(bank == null) return false;

        if(bank.CurrBalance>=towerCost){
            Instantiate(weapon.gameObject, position, Quaternion.identity);
            bank.Withdraw(towerCost);
            return true;
        }
        
        return false;

    }
}
