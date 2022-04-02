using UnityEngine;

public class SpherePropsSpawner : MonoBehaviour
{
    public GameObject[] props;
    public bool saveCollisionInfo;
    public float sphereRadius = 5;
    public Vector2 scaleRange = Vector2.one;
    [Range(0, 1)]
    public float probability = 0.5f;
    public int latitudeDivisions = 10;
    public int longitudeDivisions = 359;

    [ContextMenu("Spawn props")]
    public void Spawn()
    {
        Clear();

        for (int i = 0; i < latitudeDivisions; ++i)
        {
            float latitude = (0.5f - i / (float)(latitudeDivisions - 1)) * Mathf.PI;
            int longitudeIterations = (int)(1 + Mathf.Cos(latitude) * longitudeDivisions);
            for (int j = 0; j < longitudeIterations; ++j)
            {
                if (Random.value > probability)
                {
                    continue;
                }

                GameObject prop = props[Random.Range(0, props.Length)];

                GameObject obj = Instantiate(prop, transform);
                SphericalMovement sphericalMovement = obj.AddComponent<SphericalMovement>();

                sphericalMovement.movementSpeed = 0;
                sphericalMovement.sphereRadius = sphereRadius;

                float lat = latitude;
                float lon = 2 * Mathf.PI * (j / (float)longitudeIterations);

                sphericalMovement.startingCoordinates = new Vector2(lat, lon);
                sphericalMovement.SetLat(lat);
                sphericalMovement.SetLon(lon);

                sphericalMovement.SetTrueBearingDegrees(Random.value * 360);

                SphericalMovementPresenter presenter = obj.AddComponent<SphericalMovementPresenter>();
                presenter.model = sphericalMovement;

                presenter.UpdatePosition();
                presenter.UpdateRotation();

                obj.transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);

                DestroyImmediate(presenter);
                if (!saveCollisionInfo)
                {
                    DestroyImmediate(sphericalMovement);
                }
            }
        }
    }

    [ContextMenu("Clear props")]
    public void Clear()
    {
        Utility.RemoveChildrenImmediate(transform);
    }
}
