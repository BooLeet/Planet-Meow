using UnityEngine;

public class PlayerCameraPositioner : MonoBehaviour
{
    public Transform cameraTransform;
    [Space]
    public Player player;
    public Vector3 playerLocalOffset;
    public float playerHeightOffset = 1;
    public float moveTime = 1f;
    private Vector3 interpolationStartPosition;
    private Quaternion interpolationStartRotation;

    private SmoothInterpolator lastInterpolator;

    public void FocusOnPlayer()
    {
        interpolationStartPosition = cameraTransform.position;
        interpolationStartRotation = cameraTransform.rotation;

        StopLastInterpolator();
        lastInterpolator = SmoothInterpolator.StartInterpolation(gameObject, moveTime, MoveToPlayer);
    }

    public void ReturnToOrigin()
    {
        interpolationStartPosition = cameraTransform.localPosition;
        interpolationStartRotation = cameraTransform.localRotation;

        StopLastInterpolator();
        lastInterpolator = SmoothInterpolator.StartInterpolation(gameObject, moveTime, MoveToOrigin);
    }

    private void StopLastInterpolator()
    {
        if (lastInterpolator != null)
        {
            lastInterpolator.Interupt();
        }
    }

    private void MoveToPlayer(float lerpParameter)
    {
        TransfromLerpParameter(ref lerpParameter);

        Vector3 targetPosition = player.transform.position +
            player.transform.forward * playerLocalOffset.z +
            player.transform.right * playerLocalOffset.x +
            player.transform.up * playerLocalOffset.y;

        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position + player.transform.up * playerHeightOffset - cameraTransform.position, player.transform.up);
        cameraTransform.position = Vector3.Lerp(interpolationStartPosition, targetPosition, lerpParameter);
        cameraTransform.rotation = Quaternion.Lerp(interpolationStartRotation, targetRotation, lerpParameter);
    }

    private void MoveToOrigin(float lerpParameter)
    {
        TransfromLerpParameter(ref lerpParameter);

        cameraTransform.localPosition = Vector3.Lerp(interpolationStartPosition, Vector3.zero, lerpParameter);
        cameraTransform.localRotation = Quaternion.Lerp(interpolationStartRotation, Quaternion.identity, lerpParameter);
    }

    private void TransfromLerpParameter(ref float parameter)
    {
        parameter = Mathf.Sin(parameter * Mathf.PI / 2);
    }

}
