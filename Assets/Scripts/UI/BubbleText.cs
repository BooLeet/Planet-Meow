using UnityEngine;
using UnityEngine.UI;

public class BubbleText : MonoBehaviour
{
    public Text text;
    public float bubbleScale = 1.5f;
    private float targetScale = 1;
    private float lerpParameter = 10;

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * targetScale, Time.deltaTime * lerpParameter);
        targetScale = Mathf.Lerp(targetScale, 1, Time.deltaTime * lerpParameter);
    }

    public void SetText(string val)
    {
        text.text = val;
        targetScale = bubbleScale;
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.one;
        targetScale = 1;
    }
}
