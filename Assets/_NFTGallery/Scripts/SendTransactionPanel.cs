using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendTransactionPanel : MonoBehaviour
{
    #region private variables
    [SerializeField]
    private Text accountTxt;
    #endregion

    #region public variables
    void OnEnable()
    {
        PaintingsManager.Instance.btnApprove.gameObject.SetActive(true);
        PaintingsManager.Instance.btnBuyItem.gameObject.SetActive(false);
        accountTxt.gameObject.SetActive(true);
        accountTxt.text = "<color=#989898>Connecte wallet : </color>"+PlayerPrefs.GetString("Account").Substring(0,4)+"..."+PlayerPrefs.GetString("Account").Substring(PlayerPrefs.GetString("Account").Length - 4);
    }
    void OnDisable()
    {
        accountTxt.text = "";
        accountTxt.gameObject.SetActive(false);
    }
    #endregion

    #region unity callbacks
    #endregion

    #region private methods
    #endregion

    #region public methods
    #endregion
}
