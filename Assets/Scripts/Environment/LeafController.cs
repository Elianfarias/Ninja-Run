using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LeafController : MonoBehaviour
{
    void Update()
    {
        var rb = GetComponent<Rigidbody2D>();
        float downSpeed = rb.linearVelocity.y;

        // Calculamos el desplazamiento horizontal senoidal
        float sway = Mathf.Sin(Time.time * 1.5f) * 1f;

        // Asignamos la nueva velocidad
        rb.linearVelocity = new Vector2(sway, downSpeed);
    }

    public void Destroy() => Destroy(gameObject);
}
