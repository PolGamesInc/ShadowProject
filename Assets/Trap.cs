using UnityEngine;

public class Trap : MonoBehaviour
{
    public Vector2[] Points;

    private void Update()
    {
        float t = Mathf.PingPong(Time.time * 0.5f, 1f);
        gameObject.transform.position = Vector2.Lerp(Points[0], Points[1], t);
    }
}
