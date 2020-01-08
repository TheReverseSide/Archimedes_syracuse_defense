using UnityEngine;

public class FloatingGlucose : MonoBehaviour
{
    public float DestroyTime = 3f;
    public Vector3 offset = new Vector3(0, 2, 0);

    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.localPosition += offset;
    }
}
