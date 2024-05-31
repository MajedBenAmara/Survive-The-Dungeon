using TMPro;
using UnityEngine;

public class EquipmentWindow : MonoBehaviour
{
    #region Variables

    public static EquipmentWindow Instance;

    public GameObject EquipmentSelectionScreen;
    public GameObject EquipmentUIGameObject;
    public Transform EquipmentUIHolder;
    public GameObject SwapWindow;

    [SerializeField]
    private GameObject _skipButton;
    [SerializeField]
    private GameObject _rerollButton;
    [SerializeField]
    private TextMeshProUGUI _rerollButtonText;
    [SerializeField]
    private PlayerEquipmentUI[] _playerEquipmentList; 

    #endregion

    #region Unity Func

    private void Start()
    {
        Instance = this;
    }


    private void Update()
    {
        //Testing();
    }

    #endregion

    #region Built Func

    // Activate equipments window for testing
    private void Testing()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ShowEquipmentSelectionWindow();
            UpdateUI();
        }
    }

    // this func is called when a gem is destroyed
    // this func create equipment UI slot 
    public void UpdateUI()
    {
        // this the holer of this equipments UI is empty it will create them 
        if(EquipmentUIHolder.childCount == 0)
        {
            CreateEquipmentUIElements();
        }
        else
        {
            // if not it will destroy the existance ones and create new ones, one by one
            for (int i = 0; i < EquipmentUIHolder.childCount; i++)
            {
                Destroy(EquipmentUIHolder.GetChild(i).gameObject);
            }

            CreateEquipmentUIElements();
        }
        UpdatePLayerEquipmentUI();
    }

    // create the equipment UI in the selection window 
    private void CreateEquipmentUIElements()
    {
        // first pick random equipment and put them in a list
        EquipmentManager.Instance.UpdateRandomEquipmentList();
        // go through that list
        for (int i = 0; i < EquipmentManager.Instance.NumberOfEquipment; i++)
        {
            // pick an equipment from that list
            EquipmentSO equip = EquipmentManager.Instance.RandomEquipments[i];
            // then get it's different UI components and put them in there corresponding UI
            Instantiate(EquipmentUIGameObject, EquipmentUIHolder).GetComponent<EquipmentSlotUI>().
                Initialize(equip, EquipmentManager.Instance.EquipmentColors[equip.Level-1]);
        }
    }

    private void UpdatePLayerEquipmentUI()
    {
        for (int i = 0; i < _playerEquipmentList.Length; i++)
        {
            EquipmentSO equip = EquipmentManager.Instance.PlayerEquipments[i];
            if(equip.EquipmentIcon != null)
            {
                Color color = EquipmentManager.Instance.EquipmentColors[equip.Level - 1];
                _playerEquipmentList[i].SetPlayerEquipmentUI(equip.EquipmentIcon, equip.SetIcon, color);
            }

        }
    }

    public void ActivateSwapNotificationWindow()
    {
        SwapWindow.SetActive(true);
    }

    public void DesactivateSwapNotificationWindow()
    {
        SwapWindow.SetActive(false);
    }

    public void ShowEquipmentSelectionWindow()
    {
        GameManager.Instance.StopTheGame();
        EquipmentSelectionScreen.SetActive(true);
        _skipButton.SetActive(true);
        SetRerollButtonText();
        _rerollButton.SetActive(true);
        
        PlayerEquipmentUIManager.Instance.HidePLayerEquipmentUI();

    }

    public void HideEquipmentSelectionWindow()
    {
        GameManager.Instance.ResumeTheGame();
        EquipmentSelectionScreen.SetActive(false);
        _skipButton.SetActive(false);
        _rerollButton.SetActive(false);
        PlayerEquipmentUIManager.Instance.ShowPLayerEquipmentUI();
    }

    // Reroll another set of equipments
    public void RerollEquipments()
    {
        if(GameManager.Instance.PlayerStats.RerollPoints > 0)
        {
            UpdateUI();
            GameManager.Instance.PlayerStats.RerollPoints--;
            SetRerollButtonText();
        }
    }

    private void SetRerollButtonText()
    {
        _rerollButtonText.text = "Reroll " + GameManager.Instance.PlayerStats.RerollPoints;
    }
    #endregion
}
