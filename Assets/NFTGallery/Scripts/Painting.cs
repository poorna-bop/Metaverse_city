using TMPro;
using UnityEngine;
public class Painting : MonoBehaviour
{
    #region private variables
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    #endregion

    #region public variables
    [HideInInspector]
    public string paintingName;
    [HideInInspector]
    public string paintingDescription;
    [HideInInspector]
    public string moreinfoLink;
    [HideInInspector]
    public string token_id;
    public int chain_id;
    public string item_id;
    public string currentPrice;
    public string symbol;
    public string currentSeller;
    public string currentCurrency;
    public string category;
    public string collection;
    public GameObject descriptionPanel;
    public Texture img;
    public bool isOpend = false;
    #endregion

    #region unity callbacks
    void Awake()
    {
        descriptionPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerController")
        {
            PaintingsManager.Instance.OpenPaintingWindow(this);
            PaintingsManager.Instance.OpenBWindow();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PlayerController")
        {
            PaintingsManager.Instance.ClosePaintingWindow(this);
            PaintingsManager.Instance.CloseBWindow();
        }
    }
    #endregion

    #region private methods
    #endregion

    #region public methods
    public void SetData()
    {
        nameText.text = paintingName;
        descriptionText.text = paintingDescription;
    }
    #endregion

}
