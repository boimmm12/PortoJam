using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public float drainRate = 5f; // berkurang 5 per detik
    public Slider staminaSlider; // UI slider stamina

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
                OnStaminaDepleted();
            }
        }
    }

    void OnStaminaDepleted()
    {
        isRunning = false;
        Debug.Log("Stamina habis! Game Over atau player melambat?");
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
