using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValuePanel : MonoBehaviour
{
    public static ValuePanel instance { get; private set; }
    
    public GameObject panel;
    
    [SerializeField] private Toggle normalToggle;
    [SerializeField] private Toggle negatedToggle;
    [SerializeField] private TMP_Dropdown variableDropdown;
    
    private CanvasGroup canvasGroup; 
        
    private DraggableItem currentItem;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        
        panel.SetActive(false);
        
    }
    
    public void OpenPanel(DraggableItem item, CanvasGroup canvasG)
    {
        currentItem = item;
        panel.SetActive(true);
        canvasGroup = canvasG;
        PanelManager.Instance.OpenedValuePanel(this);
        canvasGroup.blocksRaycasts = false;

    }

    public void OnOkClicked()
    {
        string selectedModifier = GetSelectedModifier();
        string selectedVariable = variableDropdown.options[variableDropdown.value].text;

        string result = $"{selectedModifier} - {selectedVariable}";

        PanelManager.Instance.ClosedValuePanel(this);
        instance.OnOk(result);
        

    }
    
    public void OnOk(string selectedInfo)
    {
        if (currentItem != null)
        {
            currentItem.ApplyValue(selectedInfo);
        }
        
        panel.SetActive(false);
        canvasGroup.blocksRaycasts = true;
        currentItem = null;
        
    }

    public string GetSelectedModifier()
    {
        if (normalToggle.isOn) return "Normal";
        if (negatedToggle.isOn) return "Negated";

        return "normal";
    }
    
}
