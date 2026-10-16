using UnityEngine;
using UnityEngine.UI;

public class SlideshowPopup : MonoBehaviour
{
    public GameObject popupRoot;
    public Image displayImage;
    public Sprite[] slides;

    private int currentIndex = 0;

    void Start()
    {
        popupRoot.SetActive(true);
    }

    public void OpenPopup()
    {
        currentIndex = 0;
        UpdateSlide();
        popupRoot.SetActive(true);
        Time.timeScale = 0f; // optional pause
    }

    public void ClosePopup()
    {
        popupRoot.SetActive(false);
        Time.timeScale = 1f;
    }

    public void NextSlide()
    {
        currentIndex = (currentIndex + 1) % slides.Length;
        UpdateSlide();
    }

    public void PreviousSlide()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = slides.Length - 1;

        UpdateSlide();
    }

    private void UpdateSlide()
    {
        displayImage.sprite = slides[currentIndex];
    }
}
