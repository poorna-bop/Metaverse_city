using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int chainID = 56;

    [HideInInspector]
    public string lastScene;
    public static GameManager Instance;
    public static bool isPaintingDataCollected = false;
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
    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Start is called before the first frame update
    void Start()
    {
        isPaintingDataCollected = false;
        APIManager.Instance.allNFTsList = new List<NFT>();

        //Get all paintings data
        StartCoroutine(APIManager.Instance.IGetNFTsForChainId(chainID, async () =>
        {
            for (int i = 0; i < APIManager.Instance.nftForChainIdResponse.data.Length; i++)
            {
                NFT nftDetails = await APIManager.Instance.IGetNFTDetails(APIManager.Instance.nftForChainIdResponse.data[i].uri_link);
                APIManager.Instance.allNFTsList.Add(nftDetails);
            }
        }));
    }
    

    public void OnStoreEnter()
    {
        if(SceneManager.GetActiveScene().name == Constants.FashionStoreSceneName)
        return;
        lastScene = Constants.CitySceneName;
        SceneManager.LoadScene(Constants.FashionStoreSceneName);
    }
    public void OnNFTGalleryEnter()
    {
        if(SceneManager.GetActiveScene().name == Constants.NFTGalleryceneName)
        return;
        lastScene = Constants.CitySceneName;
        SceneManager.LoadScene(Constants.NFTGalleryceneName);
    }
    public void OnCarShowroomEnter()
    {
        if(SceneManager.GetActiveScene().name == Constants.CarShowroomSceneName)
        return;
        lastScene = Constants.CitySceneName;
        SceneManager.LoadScene(Constants.CarShowroomSceneName);
    }
    
    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape))
        {
             Time.timeScale = (Time.timeScale==1)?0:1;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if(SceneManager.GetActiveScene().name == Constants.CitySceneName)
            return;
            GoBackToCityScene();
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == Constants.FashionStoreSceneName)
        {
            PlayerManager.Instance.SetPlayerPosition(Constants.fashionStorePlayerInitialPoint,Constants.fashionStorePlayerInitialRotationPoint);
        }
        if(scene.name == Constants.NFTGalleryceneName)
        {
            PlayerManager.Instance.SetPlayerPosition(Constants.nftGalleryPlayerInitialPoint,Constants.nftGalleryPlayerInitialRotationPoint);
        }
        if(scene.name == Constants.CitySceneName)
        {
            if(lastScene == Constants.FashionStoreSceneName)
            {
                PlayerManager.Instance.SetDirectPlayerPosition(Constants.cityScenePointOnFashionStoreGate,Constants.citySceneRotationPointOnFashionStoreGate);
            }
            else if (lastScene == Constants.NFTGalleryceneName)
            {
                PlayerManager.Instance.SetDirectPlayerPosition(Constants.cityScenePointOnNFTGalleryGate,Constants.citySceneRotationPointOnNFTGalleryGate);
            }
            else
            {
                PlayerManager.Instance.SetDirectPlayerPosition(Constants.cityScenePlayerInitialPoint,Constants.cityScenePlayerInitialRotationPoint);
            }
        }
    }
    void GoBackToCityScene()
    {
        if(SceneManager.GetActiveScene().name == Constants.CitySceneName)
        return;

        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(Constants.CitySceneName);
        
    }
}
