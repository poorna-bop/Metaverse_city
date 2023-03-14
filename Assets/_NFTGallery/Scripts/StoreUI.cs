using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    #region private variables
    [SerializeField]
    private Text photoNameTxt;
    [SerializeField]
    private Text photoDescriptionTxt;
    [SerializeField]
    private Text photoSellerTxt;
    [SerializeField]
    private Text photoPriceTxt;
    [SerializeField]
    private Text photoCategoryTxt;
    [SerializeField]
    private Text photoTokenIdTxt;
    [SerializeField]
    private Text photoSoldTxt;
    [SerializeField]
    private RawImage photoImg;
    // [SerializeField]
    // private Image currencyImg;
    private NFTItemDetails nftItemDetails;
    #endregion

    #region public variables
    #endregion

    #region unity callbacks
    async void OnEnable()
    {
        Painting _painting = PaintingsManager.Instance.currentPainting;
        photoNameTxt.text = _painting.paintingName;
        photoDescriptionTxt.text = _painting.paintingDescription;
        photoImg.texture = PaintingsManager.Instance.currentPainting.img;
        //photoTokenIdTxt.text = "<b>Token Id :  </b>" + _painting.token_id;

        //Debug.Log("current "+PaintingsManager.Instance.currentPainting);
        nftItemDetails = await APIManager.Instance.IGetSaleDetailsFromTokenId(GameManager.Instance.chainID, _painting.token_id);

        //currencyImg.sprite = GameSceneManager.Instance.currencyData[nftItemDetails.currencyName];
        if (nftItemDetails.itemId == "0" || nftItemDetails.sold)
        {
            PaintingsManager.Instance.currentPainting.currentSeller = nftItemDetails.seller;
            PaintingsManager.Instance.currentPainting.currentCurrency = nftItemDetails.currency;
            PaintingsManager.Instance.currentPainting.currentPrice = nftItemDetails.price;
            PaintingsManager.Instance.currentPainting.symbol = nftItemDetails.currencyName;
            
            photoSellerTxt.text = "";
            photoPriceTxt.text = "";
            photoCategoryTxt.text = "";
            
            photoSoldTxt.gameObject.SetActive(true);
            if (PaintingsManager.Instance.btnLogin.gameObject.activeSelf)
                PaintingsManager.Instance.btnLogin.interactable = false;
            if (PaintingsManager.Instance.btnApprove.gameObject.activeSelf)
                PaintingsManager.Instance.btnApprove.interactable = false;
            if (PaintingsManager.Instance.btnBuyItem.gameObject.activeSelf)
                PaintingsManager.Instance.btnBuyItem.interactable = false;

        }
        else
        {
            float price = float.Parse(nftItemDetails.price);
            int _decimal = int.Parse(nftItemDetails.currencyDecimal);
            float divider = Mathf.Pow(10.0f, (float)_decimal);
            if (divider > 0)
                price = (price / divider);

            PaintingsManager.Instance.currentPainting.currentSeller = nftItemDetails.seller;
            PaintingsManager.Instance.currentPainting.currentCurrency = nftItemDetails.currency;
            PaintingsManager.Instance.currentPainting.currentPrice = nftItemDetails.price;
            PaintingsManager.Instance.currentPainting.symbol = nftItemDetails.currencyName;
            
            photoSellerTxt.text =  nftItemDetails.seller.Substring(0,4)+"..."+nftItemDetails.seller.Substring(nftItemDetails.seller.Length - 4);
            photoPriceTxt.text =   price + " " + nftItemDetails.currencyName;
            photoCategoryTxt.text = PaintingsManager.Instance.currentPainting.category+"("+PaintingsManager.Instance.currentPainting.collection+")";


            photoSoldTxt.gameObject.SetActive(false);
            if (PaintingsManager.Instance.btnLogin.gameObject.activeSelf)
                PaintingsManager.Instance.btnLogin.interactable = true;
            if (PaintingsManager.Instance.btnApprove.gameObject.activeSelf)
                PaintingsManager.Instance.btnApprove.interactable = true;
            if (PaintingsManager.Instance.btnBuyItem.gameObject.activeSelf)
                PaintingsManager.Instance.btnBuyItem.interactable = true;
        }

        if ((PlayerPrefs.GetString("Account") == ""))
        {
            PaintingsManager.Instance.CloseSendTransactionPanel();
        }
        else
        {
            PaintingsManager.Instance.OpenSendTransactionPanel();
        }
    }
    #endregion

    #region private methods
    #endregion

    #region public methods
    #endregion
}
