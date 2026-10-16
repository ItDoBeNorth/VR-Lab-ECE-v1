using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotsBehavior : MonoBehaviour
{
    [SerializeField] private List<GameObject> slots;
    [SerializeField] private GameObject Label;
    private TextMeshProUGUI label;
    [SerializeField] private Color32 wrongColor;

    [SerializeField] private ObjectiveTask completionTask;
    [SerializeField] GameObject AssociatedRung;
    [SerializeField] private CanvasGroup groupCanvas;

    public bool rungComplete =  false;
    
    void Start()
    {
        wrongColor = new Color32(255, 81, 69,255);
        label = Label.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckSlots()
    {
        int wrong = 0;
        foreach (GameObject slot in slots)
        {
            Slot tempSlot = slot.GetComponent<Slot>();
            if (!tempSlot.IsCorrect())
            {
                tempSlot.SetColor(wrongColor);
                wrong++;
            }
            else
            {
                tempSlot.SetColor(tempSlot.defaultColor);
            }
            
            //tempSlot.SetColor(tempSlot.IsCorrect() ? tempSlot.defaultColor : wrongColor);
        }

        if (slots.Count != 0)
        {
            if (wrong == 0)
            {
                label.text = "Correct!";
                ObjectivesManager.Instance.CompleteTask(completionTask);
                AssociatedRung.GetComponent<RungItem>().puzzleComplete();
                groupCanvas.blocksRaycasts = false;
                rungComplete = true;
            }
            else
            {
                label.text = wrong.ToString() + " Wrong!";
            }
        }
    }

    public void ResetSlotColors()
    {
        foreach (GameObject slot in slots)
        {
            Slot tempSlot = slot.GetComponent<Slot>();
            tempSlot.SetColor(tempSlot.defaultColor);
        }
    }

    public void ResetSlots()
    {
        foreach (GameObject slot in slots)
        {
            Slot tempSlot = slot.GetComponent<Slot>();
            tempSlot.Reset();
        }
        label.text = "Check";
    }
}
