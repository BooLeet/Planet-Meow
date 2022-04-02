using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlyby : MonoBehaviour
{
    [System.Serializable]
    public struct Section
    {
        public Transform from;
        public Transform to;
    }

    public Transform flyingTransform;
    public SpriteRenderer fadeSprite;
    public List<Section> route;
    public float speed = 1;
    public float fadeDistance = 0.5f;

    private WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();

    private int currentSectionIndex = 0;

    void Start()
    {
        FlySection(currentSectionIndex);
    }

    private IEnumerator FlyPointToPoint(Transform from, Transform to)
    {
        float time = Vector3.Distance(from.position, to.position) / speed;
        float timeCounter = 0;
        while (timeCounter < time)
        {
            flyingTransform.position = Vector3.Lerp(from.position, to.position, timeCounter / time);
            flyingTransform.rotation = Quaternion.Lerp(from.rotation, to.rotation, timeCounter / time);

            float minDistance = Mathf.Min(Vector3.Distance(flyingTransform.position, from.position), Vector3.Distance(flyingTransform.position, to.position));
            if(minDistance > fadeDistance)
            {
                fadeSprite.color = Color.clear;
            }
            else
            {
                fadeSprite.color = Color.Lerp(Color.clear, Color.black, 1 - minDistance / fadeDistance);
            }

            yield return waitFrame;
            timeCounter += Time.deltaTime;
        }

        currentSectionIndex = (currentSectionIndex + 1) % route.Count;
        FlySection(currentSectionIndex);
    }

    private void FlySection(int sectionIndex)
    {
        StartCoroutine(FlyPointToPoint(route[sectionIndex].from, route[sectionIndex].to));
    }
}
