using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image healthBar;
    public Image staminaBar;
    public Image experienceBar;

    public void UpdateUI(Image displayImage, float currentValue, float maxValue)
    {
        displayImage.fillAmount = Mathf.Clamp01(currentValue / maxValue);
    }
}
