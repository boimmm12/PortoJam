using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public float drainRate = 5f;
    public Slider staminaSlider;
    public Player player;
    private bool isRunning = true;

    void Start()
    {
        currentStamina = maxStamina;
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
    }

    void Update()
    {
        if (isRunning)
        {
            currentStamina -= drainRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            if (staminaSlider != null)
                staminaSlider.value = currentStamina;

            if (currentStamina <= 0)
            {
                StaminaDepleted();
            }
        }
    }

    void StaminaDepleted()
    {
        isRunning = false;
        player.OnStaminaDepleted();
    }

    public void RefillStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0, maxStamina);
    }

    public void SetRunning(bool value)
    {
        isRunning = value;
    }
    public void ReduceStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina - amount, 0, maxStamina);
    }

}
