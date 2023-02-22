using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddressPrint : MonoBehaviour
{
    [SerializeField]
    private Text _walletAddress;

    [SerializeField]
    private Text _chainID;
    // Start is called before the first frame update
    void Start()
    {
        _walletAddress.text = "Wallet Address is " + PlayerPrefs.GetString("Account");
        _chainID.text = "Chain ID is " + Web3GL.Network().ToString();
    }

    
}
