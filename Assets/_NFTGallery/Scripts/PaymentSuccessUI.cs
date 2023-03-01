using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaymentSuccessUI : MonoBehaviour
{
    #region private variables
    [SerializeField]
    private Text photoNameTxt;
    [SerializeField]
    private Text photoAccountTxt;
    [SerializeField]
    private Text photoSellerTxt;
    [SerializeField]
    private Text photoPriceTxt;
    [SerializeField]
    private RawImage photoImg;
    [SerializeField]
    private Text photoSoldTxt;
    #endregion

    #region public variables
    #endregion

    #region unity callbacks
    void OnEnable()
    {
        Painting _painting = PaintingsManager.Instance.currentPainting;
        photoNameTxt.text =  _painting.paintingName;
        photoSellerTxt.text = PaintingsManager.Instance.currentPainting.currentSeller.Substring(0,4)+"..."+PaintingsManager.Instance.currentPainting.currentSeller.Substring(PaintingsManager.Instance.currentPainting.currentSeller.Length - 4);  
        photoPriceTxt.text =  PaintingsManager.Instance.currentPainting.currentPrice+"  "+PaintingsManager.Instance.currentPainting.symbol;
        photoImg.texture = PaintingsManager.Instance.currentPainting.img;
        photoAccountTxt.text = "<color=#989898>Connecte wallet : </color>"+PlayerPrefs.GetString("Account").Substring(0,4)+"..."+PlayerPrefs.GetString("Account").Substring(PlayerPrefs.GetString("Account").Length - 4);
        photoSoldTxt.gameObject.SetActive(true);
    }
    #endregion

    #region private methods

    #endregion

    #region public methods
    public void CloseSuccessPopup()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
        gameObject.SetActive(false);
    }
    #endregion
}
