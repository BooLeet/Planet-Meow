using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnDamageCameraShaker : MonoBehaviour
{
    public Player player;
    public CameraShaker cameraShaker;
    public float shakeStrength = 0.2f;

    void Start()
    {
        player.damageable.OnDamageTaken += Shake;
    }

    private void OnDestroy()
    {
        player.damageable.OnDamageTaken -= Shake;
    }

    private void Shake()
    {
        cameraShaker.Shake(shakeStrength);
    }

}
