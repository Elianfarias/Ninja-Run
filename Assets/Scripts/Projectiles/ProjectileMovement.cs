using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileMovement : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;

    [SerializeField]
    float speed = 200f;

    public ProjectileSpawner spawner;
    public ProjectileDirection projectileDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        StartMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GameStateManager.Instance.SetGameState(GameState.GAME_OVER);

        if (!collision.CompareTag("Food"))
        {
            spawner.UpdateTimeProjectileDestroy();
            gameObject.SetActive(false);
        }
    }

    public void StartMovement()
    {
        var direction = player.transform.position - spawner.transform.position;

        if(projectileDirection == ProjectileDirection.HORIZONTAL)
            rb.linearVelocityX = speed * (direction.x >= 0 ? 1 : -1);
        else
            rb.linearVelocityY = speed * (direction.y >= 0 ? 1 : -1);
    }
}
