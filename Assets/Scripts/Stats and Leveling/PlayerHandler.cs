using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public PlayerStats playerData = new PlayerStats();

    [SerializeField] Transform spawnPoint;
    [SerializeField] float timerValue;
    [SerializeField] bool canHeal = true;

    public UIManager uiManager;

    public void DamagePlayer(float damageValue)
    {
        timerValue = 0;
        canHeal = false;
        playerData.health.currentValue -= damageValue;
        uiManager.UpdateUI(uiManager.healthBar, playerData.health.currentValue, playerData.health.maxValue);
    }
    void HealOverTime()
    {
        if (canHeal)
        {
            if (playerData.health.currentValue < playerData.health.maxValue && playerData.health.currentValue > 0)
            {
                //current health to increase by a value over time 
                playerData.health.currentValue += playerData.health.value * Time.deltaTime;
                uiManager.UpdateUI(uiManager.healthBar, playerData.health.currentValue, playerData.health.maxValue);
            }
        }
    }
    void Timer()
    {
        if (!canHeal)
        {
            timerValue += Time.deltaTime;
            if (timerValue >= 1.5f)
            {
                //allow healing
                canHeal = true;
                //reset timer
                timerValue = 0;
            }
        }
    }
    private void Start()
    {
       
        uiManager.UpdateUI(uiManager.healthBar, playerData.health.currentValue, playerData.health.maxValue);
        uiManager.UpdateUI(uiManager.staminaBar, playerData.stamina.currentValue, playerData.stamina.maxValue);
        uiManager.UpdateUI(uiManager.experienceBar, playerData.experience.currentValue, playerData.experience.maxValue);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Heal")
        {
            playerData.health.value *= 2;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Heal")
        {
            playerData.health.value /= 2;
        }
        if (other.tag == "SpawnPoint")
        {
            spawnPoint = other.transform;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Damage"))
        {
            DamagePlayer(10);
        }
    }
}
