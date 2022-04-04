using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image fill;
    public Image damage;
    public float damageLerpParameter = 5;
    public float damagePadding = 0.02f;

    public void SetValue(float val)
    {
        fill.fillAmount = val;
    }

    private void Update()
    {
        damage.fillAmount = Mathf.Lerp(damage.fillAmount, fill.fillAmount - damagePadding, Time.deltaTime * damageLerpParameter);
    }
}
