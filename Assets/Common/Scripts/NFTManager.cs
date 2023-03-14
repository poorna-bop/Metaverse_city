using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#region supporting classes
[Serializable]
public class RawNFT
{
    public string tokenId;
    public string itemId;
    public string uri_link;
}
public class NFTData
{
    public string tokenId;
    public string itemId;
    public string uri_link;
    public string name;
    public string description;
    public string contract_address;
    public NFTAttribute[] attributes;
    public string owner_address;
    public int chainId;
    public string saleId;
    public string category;
    public string collection;
    public string royalty;
    public string imageURL;
}

[Serializable]
public class NFT
{
    public string name;
    public string description;
    public string contract_address;
    public string token_id;
    public NFTAttribute[] attributes;
    public string image;
    public string uri_link;
    public string owner_address;
    public int chainId;
    public string saleId;
    public string category;
    public string collection;
    public string royalty;
}
[Serializable]
public class NFTAttribute
{
    public string trait_type;
    public string value;
}
[Serializable]
public class NFTItemDetails
{
    public string itemId;
    public string tokenId;
    public string seller;
    public string price;
    public string currency;
    public bool sold;
    public bool isActive;
    public string currencyDecimal;
    public string currencyName;
}

[Serializable]
public class NFTList
{
    public NFT[] nfts;
}

[Serializable]
public class RawNFTList
{
    public RawNFT[] data;
}

[Serializable]
public class Balance
{
    public string ethereum;
    public string binance;
    public string polygon;
}
#endregion

public class NFTManager : MonoBehaviour
{


   #region private variables
   #endregion

   #region public variables
   public List<NFTData> AllNFTS = new List<NFTData>();
   
//    public List<RawNFT2> FashionStoreNFTs = new List<RawNFT2>();
//    public List<RawNFT2> GalleryNFTs = new List<RawNFT2>();
    
    public static NFTManager Instance;
    void Awake()
    {
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

   #region unity callbacks
   private void Start() {
    
   }
   #endregion

   #region private methods
   
   #endregion

   #region public methods
   #endregion
}
