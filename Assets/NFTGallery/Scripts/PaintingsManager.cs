using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PaintingsManager : MonoBehaviour
{
    public static PaintingsManager Instance;

    #region private variables

    [SerializeField]
    public Button btnLogin;
    [SerializeField]
    private GameObject btnSendTransaction;
    #endregion

    #region public variables
    public Button btnApprove;
    public Button btnBuyItem;
    public GameObject loading;
    public GameObject successPanel;
    public GameObject failPanel;
    public PhotoObject[] photoObjects;
    public RawImage img;
    public GameObject buyNFTPopup;

    [HideInInspector]
    public Painting currentPainting;
    public GameObject paintingWindow;
    int downIndex = 0;
    int upIndex = 39;
   
    public static bool isPaintingOpend = false;

    #endregion

    #region unity callbacks
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1;
        UIManager.Instance.CloseGoToPanel();
        paintingWindow.SetActive(false);
        AssignPhotos();
    }

    #endregion

    #region private methods
    private void AssignPhotos()
    {
        StartCoroutine(APIManager.Instance.IGetNFTsForChainId(GameSceneManager.Instance.chainID, async () =>
        {
            GetData();
        }));

    }

    async void GetData()
    {
        downIndex = 0;
        upIndex = 38;
    
        for (int i = 0; i < APIManager.Instance.nftForChainIdResponse.data.Length; i++)
        {
            if(SceneManager.GetActiveScene().name != Constants.NFTGalleryceneName)
            break;
            NFT nftDetails = await APIManager.Instance.IGetNFTDetails(APIManager.Instance.nftForChainIdResponse.data[i].uri_link);
            //NFT nftDetails = APIManager.Instance.allNFTsList[i];
            
            if (nftDetails.category == "MTW")
            {
                photoObjects[downIndex].painting.moreinfoLink = APIManager.Instance.nftForChainIdResponse.data[i].uri_link;
                photoObjects[downIndex].painting.token_id = APIManager.Instance.nftForChainIdResponse.data[i].tokenId;
                photoObjects[downIndex].painting.item_id = APIManager.Instance.nftForChainIdResponse.data[i].itemId;
                photoObjects[downIndex].painting.chain_id = GameSceneManager.Instance.chainID;
                if (photoObjects[downIndex].painting != null)
                    photoObjects[downIndex].painting.gameObject.SetActive(true);
                photoObjects[downIndex].painting.paintingName = nftDetails.name;
                photoObjects[downIndex].painting.category = nftDetails.category;
                photoObjects[downIndex].painting.collection = nftDetails.collection;
                photoObjects[downIndex].painting.paintingDescription = nftDetails.description;
                photoObjects[downIndex].imgURL = nftDetails.image;
                photoObjects[downIndex].painting.SetData();
                IGETImage(downIndex);
                downIndex++;
            }
            if (nftDetails.category == "VIPS")
            {
                photoObjects[upIndex].painting.moreinfoLink = APIManager.Instance.nftForChainIdResponse.data[i].uri_link;
                photoObjects[upIndex].painting.token_id = APIManager.Instance.nftForChainIdResponse.data[i].tokenId;
                photoObjects[upIndex].painting.item_id = APIManager.Instance.nftForChainIdResponse.data[i].itemId;
                photoObjects[upIndex].painting.chain_id = GameSceneManager.Instance.chainID;
                if (photoObjects[downIndex].painting != null)
                    photoObjects[upIndex].painting.gameObject.SetActive(true);
                photoObjects[upIndex].painting.paintingName = nftDetails.name;
                photoObjects[upIndex].painting.category = nftDetails.category;
                photoObjects[upIndex].painting.collection = nftDetails.collection;
                photoObjects[upIndex].painting.paintingDescription = nftDetails.description;
                photoObjects[upIndex].imgURL = nftDetails.image;
                photoObjects[upIndex].painting.SetData();
                IGETImage(upIndex);
                upIndex++;
            }
        }
    }


    public async void IGETImage(int index)
    {
         if(SceneManager.GetActiveScene().name != Constants.NFTGalleryceneName)
            return;
            
        if(photoObjects[index].photoRenderer == null)
        return;
        Material _material = new Material(Shader.Find("Standard"));
        Material[] _materials = photoObjects[index].photoRenderer.materials;
        _materials[1].SetFloat("_Glossiness", 0);
        _materials[1].SetFloat("_SpecularHighlights", 0);
        string img_url = photoObjects[index].imgURL;
        if (img_url == "" || img_url == null)
        {
            //Debug.Log("image not available");
            return;
        }
        UnityWebRequest imgRequest = UnityWebRequestTexture.GetTexture(img_url);
        await imgRequest.SendWebRequest();
        if (imgRequest.result == UnityWebRequest.Result.Success)
        {
            DownloadHandlerTexture textureDownloadHandler = (DownloadHandlerTexture)imgRequest.downloadHandler;
            Texture2D _texture = textureDownloadHandler.texture;

            if (_texture == null)
            {
                //Debug.Log("image not available");
                return;
            }
            else
            {

                _material.SetTexture("_MainTex", _texture);
                _materials[1] = _material;

                PaintingsManager.Instance.photoObjects[index].painting.img = _texture;
                PaintingsManager.Instance.photoObjects[index].photoRenderer.materials = _materials;
            }
        }
        else
        {
            Debug.Log("error = " + imgRequest.error);
        }
    }
    #endregion

    #region public methods
    public void OpenBWindow()
    {
        paintingWindow.SetActive(true);
    }
    public void CloseBWindow()
    {
        paintingWindow.SetActive(false);
    }
    public void OpenPaintingWindow(Painting painting)
    {
        currentPainting = painting;
        painting.isOpend = true;
        painting.descriptionPanel.SetActive(true);
        isPaintingOpend = false;
        OpenBWindow();
    }
    public void OpenSendTransactionPanel()
    {
        btnLogin.gameObject.SetActive(false);
        btnSendTransaction.SetActive(true);
    }
    public void CloseSendTransactionPanel()
    {
        btnLogin.gameObject.SetActive(true);
        btnSendTransaction.SetActive(false);
    }
    public void StayOpend()
    {
        if (paintingWindow.activeSelf)
            paintingWindow.SetActive(true);
    }
    public void ClosePaintingWindow(Painting painting)
    {
        currentPainting = null;
        painting.isOpend = false;
        painting.descriptionPanel.SetActive(false);
        if (!painting.isOpend)
            CloseBWindow();
    }
    public void OnMoreInfoButtonClick()
    {
        if (currentPainting == null || currentPainting.moreinfoLink == "")
        {
            Application.OpenURL("https://opensea.io/MetaWhaleWorld");
        }
        else
            Application.OpenURL(currentPainting.moreinfoLink);
    }
    public void OpenBuyNFTPopup()
    {
        //Time.timeScale = 0;
        PlayerManager.Instance.OffPlayerMove();
        CameraManagement.Instance.DisableThirdPersonCamera();
        Cursor.lockState = CursorLockMode.None;
        buyNFTPopup.SetActive(true);
    }
    public void CloseBuyNFTPopup()
    {
        Time.timeScale = 1;
        PlayerManager.Instance.OnPlayerMove();
        CameraManagement.Instance.EnableThirdPersonCamera();
        Cursor.lockState = CursorLockMode.Confined;
        buyNFTPopup.SetActive(false);
    }
    public void OpenSuccessPopup()
    {
        loading.SetActive(false);
        PlayerManager.Instance.OffPlayerMove();
        CameraManagement.Instance.DisableThirdPersonCamera();
        Cursor.lockState = CursorLockMode.None;
        successPanel.SetActive(true);
    }

    public void OpenFailPopup()
    {
        loading.SetActive(false);
        //Time.timeScale = 0;
        PlayerManager.Instance.OffPlayerMove();
        CameraManagement.Instance.DisableThirdPersonCamera();
        Cursor.lockState = CursorLockMode.None;
        failPanel.SetActive(true);
    }
    public void CloseFailePopup()
    {
        Time.timeScale = 1;
        PlayerManager.Instance.OnPlayerMove();
        CameraManagement.Instance.EnableThirdPersonCamera();
        Cursor.lockState = CursorLockMode.Confined;
        failPanel.SetActive(false);
    }
    public void OnOkclick()
    {
        Time.timeScale = 1;
        PlayerManager.Instance.OnPlayerMove();
        CameraManagement.Instance.EnableThirdPersonCamera();
        failPanel.SetActive(false);
    }
    public bool IsCurrentPaintingNull()
    {
        if (currentPainting == null)
            return true;

        else
            return false;
    }
    public bool IsCurrentPainting(Painting painting)
    {
        if (currentPainting == painting)
            return true;

        else
            return false;
    }
    public string GetPaintingName()
    {
        return currentPainting.paintingName;
    }
    public string GetPaintingDescription()
    {
        return currentPainting.paintingDescription;
    }
    #endregion
}
