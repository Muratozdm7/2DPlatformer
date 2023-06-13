using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int money;
    public TMP_Text coinText;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        coinText.text = money.ToString();
    }


    public void AddMoney()
    {
        money++;
    }
}
