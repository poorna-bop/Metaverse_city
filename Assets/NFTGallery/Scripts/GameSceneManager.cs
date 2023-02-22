using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameSceneManager : MonoBehaviour
{

    #region private variables
    
    #endregion

    #region public variables
    public GameObject selectCharacterPanel;
    public Transform vipMarketplace;
    public Transform metawhaleMarketplace;
    public GameObject vipMarketPlaceBTN;
    public GameObject metawhaleMarketplaceBTN;
    public Sprite[] currencyImages;
    public Dictionary<string, Sprite> currencyData = new Dictionary<string, Sprite>();
    public static GameSceneManager Instance;

    public GameObject tutorialPanel;
    public static bool isCollidedAscendingStair;
    public static bool isColliderStair;
    public int chainID = 56;
    


    #endregion

    #region unity callbacks
    void Awake()
    {
        Instance = this;
        tutorialPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        APIManager.Instance.OffLoading();
        Cursor.lockState = CursorLockMode.Confined;
        
        currencyData["DT3"] = currencyImages[0];
        currencyData["VIPS"] = currencyImages[1];
        currencyData["MTW"] = currencyImages[2];
        currencyData["TBUSD"] = currencyImages[3];
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PaintingsManager.Instance.buyNFTPopup.activeSelf)
        {
             Time.timeScale = (Time.timeScale==1)?0:1;
        }
    }
    #endregion

    #region private methods
    #endregion

    #region public methods
    public void OnVIPMarketplaceClick()
    {
        PlayerManager.Instance.SetPlayerPosition(vipMarketplace.localPosition,vipMarketplace.rotation);
        vipMarketPlaceBTN.SetActive(false);
        metawhaleMarketplaceBTN.SetActive(true);
    }
    public void OnMetawhaleMarketplaceClick()
    {
        PlayerManager.Instance.SetPlayerPosition(metawhaleMarketplace.position,metawhaleMarketplace.rotation);
        vipMarketPlaceBTN.SetActive(true);
        metawhaleMarketplaceBTN.SetActive(false);
    }
    #endregion#endregion

}
