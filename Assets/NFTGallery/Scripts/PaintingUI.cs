using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PaintingUI : MonoBehaviour
{
    #region private variables
    #endregion

    #region public variables
    #endregion

    #region unity callbacks
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PaintingsManager.Instance.OpenBuyNFTPopup();
            //PaintingsManager.Instance.controller.speed = 0;
            //PaintingsManager.Instance.OnMoreInfoButtonClick();
        }
    }
    #endregion

    #region private methods
    #endregion

    #region public methods
    #endregion#endregion

}
