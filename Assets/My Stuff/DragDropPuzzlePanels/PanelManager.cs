using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance { get; private set; }

    private GameObject currentlyOpenPanel;
    
    private ValuePanel valuePanel;
    
    private void Awake() => Instance = this;

    public void Open(GameObject panel)
    {
        Debug.Log("Opening panel " + panel);
        if (currentlyOpenPanel == panel) return; // already open, nothing to do

        if (currentlyOpenPanel != null)
        {
            Close(currentlyOpenPanel);
        }

        panel.SetActive(true);
        currentlyOpenPanel = panel;
    }

    public void OpenedValuePanel(ValuePanel panel)
    {
        valuePanel = panel;
    }

    public void ClosedValuePanel(ValuePanel panel)
    {
        if (valuePanel == panel)
        {
            valuePanel = null;
        }
    }
    
    public void Close(GameObject panel)
    {
        if (currentlyOpenPanel == panel)
        {
            if (valuePanel != null)
            {
                valuePanel.OnOkClicked();
                valuePanel = null;
            }
            panel.SetActive(false);
            currentlyOpenPanel = null;
            
        }
    }
}