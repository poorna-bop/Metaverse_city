using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    #region private vairables
    [SerializeField]
    public GameObject loading;
    
    //staging url
    //private string baseURL = "https://metawhale-backend.herokuapp.com";
    
    private string baseURL = "https://nft-backend.metawhaleworld.com";
    #endregion

    #region  public variables
    public static APIManager Instance;
    public delegate void SuccessEvents();
    public delegate void ErrorEvents(string message);

    #region Response Objects

    [HideInInspector]
    public RawNFTList nftForChainIdResponse;
    [HideInInspector]
    public NFTList nftResponse;
    [HideInInspector]
    public NFTList nftByUserResponse;
    [HideInInspector]
    public NFTList nftOnSaleResponse;
    [HideInInspector]
    public NFTList nftListedByUserResponse;
    [HideInInspector]
    public NFT nftMetadataResponse;
    [HideInInspector]
    public Balance balanceOfCoinResponse;

    #endregion

    [HideInInspector]
    public List<NFT> allNFTsList = new List<NFT>(); 

    #endregion

    #region unity callbacks
    void Awake()
    {
        PlayerPrefs.SetString("Account",  "") ;
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
   
    #endregion

    #region private methods
    #endregion

    #region public methods
    public void Loading()
    {
        loading.SetActive(true);
    }
    public void OffLoading()
    {
        loading.SetActive(false);
    }

    // Supported APIs to get NFTs
    public IEnumerator IGetNFTsForChainId(int chainId, SuccessEvents onSuccess)
    {
        chainId = 56;
        WWWForm form = new WWWForm();
        form.AddField("chainId", chainId);

        UnityWebRequest www = UnityWebRequest.Post(baseURL + "/get-all-nfts-data-on-chain", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            nftForChainIdResponse = JsonUtility.FromJson<RawNFTList>(www.downloadHandler.text);
            onSuccess();
        }
    }

    public async Task<NFTItemDetails> IGetSaleDetailsFromTokenId(int chainId, string tokenId)
    {
        WWWForm form = new WWWForm();
        form.AddField("chainId", chainId);
        form.AddField("tokenId", int.Parse(tokenId));

        UnityWebRequest www = UnityWebRequest.Post(baseURL + "/get-token-to-sale-data", form);
        await www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            NFTItemDetails nFTItemDetails1 = new NFTItemDetails();
            nFTItemDetails1.sold = true;
            nFTItemDetails1.price=null;
            nFTItemDetails1.seller=null;
            nFTItemDetails1.currency=null;
            Debug.Log(www.error);
            return nFTItemDetails1;
        }
        else
        {
            NFTItemDetails nftItemDetails = JsonUtility.FromJson<NFTItemDetails>(www.downloadHandler.text);
            return nftItemDetails;
        }
    }

    public async Task<NFT> IGetNFTDetails(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        await www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            return null;
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            NFT nftDetails = JsonUtility.FromJson<NFT>(www.downloadHandler.text);
            return nftDetails;
        }
    }

    #region Unused APIs
    public IEnumerator IGetAllNFTs(SuccessEvents onSuccess)
    {
        UnityWebRequest www = UnityWebRequest.Get(baseURL + "/get-all-nfts/");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            nftResponse = JsonUtility.FromJson<NFTList>("{\"nfts\":" + www.downloadHandler.text + "}");
            
            if (nftResponse.nfts[0].attributes.Length > 0)
            {
              
            }
            onSuccess();
        }
    }
    public IEnumerator IGetUserNFTs(string user, int chainId)
    {
        chainId = 56;
        //user = "0x9CfCD3D329549D9A327114F5ABf73637d13eFD07";
        WWWForm form = new WWWForm();
        form.AddField("user", user);
        form.AddField("chainId", chainId);

        UnityWebRequest www = UnityWebRequest.Post(baseURL + "/get-user-nfts", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            nftByUserResponse = JsonUtility.FromJson<NFTList>("{\"nfts\":" + www.downloadHandler.text + "}");
        }
    }
    public IEnumerator IGetAllNFTsOnSale(int chainId)
    {
        WWWForm form = new WWWForm();
        form.AddField("chainId", chainId);

        UnityWebRequest www = UnityWebRequest.Post(baseURL + "/get-all-nfts-on-sale", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            nftOnSaleResponse = JsonUtility.FromJson<NFTList>("{\"nfts\":" + www.downloadHandler.text + "}");
        }
    }
    public IEnumerator IGetNFTsListedByUser(string user, int chainId)
    {
        chainId = 56;
        //user = "0x9CfCD3D329549D9A327114F5ABf73637d13eFD07";
        WWWForm form = new WWWForm();
        form.AddField("user", user);
        form.AddField("chainId", chainId);

        UnityWebRequest www = UnityWebRequest.Post(baseURL + "/get-nfts-listed-by-user", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            nftListedByUserResponse = JsonUtility.FromJson<NFTList>("{\"nfts\":" + www.downloadHandler.text + "}");
        }
    }
    public IEnumerator IGetUri(int tokenId, int chainId)
    {
        tokenId = 1;
        chainId = 56;
        WWWForm form = new WWWForm();
        form.AddField("token_id", tokenId);
        form.AddField("chainId", chainId);

        UnityWebRequest www = UnityWebRequest.Post(baseURL + "/get-uri", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            nftMetadataResponse = JsonUtility.FromJson<NFT>(www.downloadHandler.text);
        }
    }
    public IEnumerator IGetTokenBalance(string token, string user, int chainId)
    {
        chainId = 56;
        // token = "0x9CfCD3D329549D9A327114F5ABf73637d13eFD07";
        // user = "0x9CfCD3D329549D9A327114F5ABf73637d13eFD07";
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("user", user);
        form.AddField("chainId", chainId);

        UnityWebRequest www = UnityWebRequest.Post(baseURL + "/get-token-balance", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            nftByUserResponse = JsonUtility.FromJson<NFTList>(www.downloadHandler.text);
        }
    }
    public IEnumerator IGetCoinBalance(string user)
    {
        //user = "0x9CfCD3D329549D9A327114F5ABf73637d13eFD07";
        WWWForm form = new WWWForm();
        form.AddField("user", user);

        UnityWebRequest www = UnityWebRequest.Post(baseURL + "/get-coin-balance", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            balanceOfCoinResponse = JsonUtility.FromJson<Balance>(www.downloadHandler.text);
        }
    }


    public async Task<NFTItemDetails> IGetListingDetails(string chainId, string itemId)
    {
        WWWForm form = new WWWForm();
        form.AddField("chainId", int.Parse(chainId));
        form.AddField("itemId", int.Parse(itemId));

        UnityWebRequest www = UnityWebRequest.Post(baseURL + "/get-nfts-listed-details", form);
        await www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            return null;
        }
        else
        {
            NFTItemDetails nftItemDetails = JsonUtility.FromJson<NFTItemDetails>(www.downloadHandler.text);
            return nftItemDetails;
        }
    }
    #endregion

    #endregion
}
