using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MetamaskLogin : MonoBehaviour
{
    #region private variables
    [SerializeField]
    private Button btnLogin;
    [SerializeField]
    private Button btnSendTransaction;
    #endregion

    #region Unity Callbacks
    void OnEnable()
    {
        if (PlayerPrefs.HasKey("Account"))
        {
            if (PlayerPrefs.GetString("Account") != "")
            {
                IfLoggedIn();
            }
        }
    }
    #endregion

    #region public methods
    public void IfLoggedIn()
    {
        PaintingsManager.Instance.loading.SetActive(false);
        btnLogin.gameObject.SetActive(false);
        btnSendTransaction.gameObject.SetActive(true);
    }
    
    async public void OnSignMessage()
    {
        try {
            string message = "Hello";
            string response = await Web3GL.Sign(message);
            print(response);
        } catch (Exception e) {
            Debug.LogException(e, this);
        }
    }

    async public void OnSendTransaction()
    {
        // account to send to
        string to = "0x428066dd8A212104Bc9240dCe3cdeA3D3A0f7979";
        // amount in wei to send
        string value = "12300000000000000";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // connects to user's browser wallet (metamask) to send a transaction
        try {
            string response = await Web3GL.SendTransaction(to, value, gasLimit, gasPrice);
            Debug.Log(response);
        } catch (Exception e) {
            Debug.LogException(e, this);
        }
    }
    #endregion
}
