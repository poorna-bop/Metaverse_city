using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region  private variables
    [SerializeField]
    private GameObject lowerCanvas;
    [SerializeField]
    private GameObject upperCanvas;
    #endregion

    #region  public variables
    public GameObject selectCharacterPanel;
    public GameObject tutorialPanel;
    public GameObject gotoPanel;
    public GameObject gotoBtn;
    public static UIManager Instance;
    #endregion

    #region  unity Callbacks
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
        
        // DontDestroyOnLoad(lowerCanvas.gameObject);
        // DontDestroyOnLoad(upperCanvas.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowTutorial());
    }

    // Update is called once per frame
    void Update()
    {
    }
    #endregion

    #region private methods
    IEnumerator ShowTutorial()
    {
        tutorialPanel.SetActive(true);
        yield return new WaitForSeconds(5);
        tutorialPanel.SetActive(false);
    }
    #endregion

    #region public methods
    public void OffTutorial()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }
    public void OnSelectCharacter()
    {
        selectCharacterPanel.SetActive(true);
    }
    public void OnCloseSelectionCharacter()
    {
        selectCharacterPanel.SetActive(false);
        PlayerManager.Instance.OnPlayerMove();
        Time.timeScale = 1;
    }
    public void OnGoToFashionStore()
    {
        GameManager.Instance.OnStoreEnter();
    }
    public void OnGoToNFTGallery()
    {
        GameManager.Instance.OnNFTGalleryEnter();
    }
    public void OnGoToCarShowroom()
    {
        GameManager.Instance.OnNFTGalleryEnter();
    }
    public void OpenGoToPanel()
    {
        Time.timeScale = 0;
        gotoPanel.SetActive(true);
        gotoBtn.SetActive(false);
    }
    public void CloseGoToPanel()
    {
        Time.timeScale = 1;
        gotoPanel.SetActive(false);
        gotoBtn.SetActive(true);
    }
    #endregion
}
