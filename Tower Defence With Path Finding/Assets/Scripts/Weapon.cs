using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int towerCost = 75;
    [SerializeField] float buildDelay = .5f;

    void Start(){
        StartCoroutine(Build());
    }

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

    IEnumerator Build(){
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
            foreach(Transform grandChild in child){
                grandChild.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform){
            child.gameObject.SetActive(true);

            foreach(Transform grandChild in child){
                grandChild.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(buildDelay);
        }


    }
}
