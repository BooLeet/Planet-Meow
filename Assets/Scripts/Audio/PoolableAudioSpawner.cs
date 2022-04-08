using UnityEngine;

public class PoolableAudioSpawner : MonoBehaviour
{
    public Poolable audioPrefab;

    public void SpawnAudio()
    {
        if (audioPrefab != null)
        {
            ObjectSpawner.SpawnObject(audioPrefab, transform.position);
        }
    }
}
