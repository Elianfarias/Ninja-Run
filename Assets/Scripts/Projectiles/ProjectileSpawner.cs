using UnityEngine;

public enum ProjectileDirection
{
    VERTICAL,
    HORIZONTAL
}

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    float projectileTimeCD = 10f;
    [SerializeField]
    ProjectileDirection projectileDirection;

    GameObject projectile;
    float lastProjectilDestroy;

    void Start()
    {
        lastProjectilDestroy = Time.time + Random.Range(1f, projectileTimeCD);
        var projectileRotation = projectilePrefab.transform.rotation;

        if (projectile == null)
        {
            projectilePrefab.GetComponent<ProjectileMovement>().spawner = this;
            projectilePrefab.GetComponent<ProjectileMovement>().projectileDirection = projectileDirection;

            if(projectileDirection == ProjectileDirection.HORIZONTAL)
                projectileRotation = Quaternion.Euler(0, 0, 0);

            projectile = Instantiate(projectilePrefab, transform.position, projectileRotation);
        }
    }

    void Update()
    {
        if (GameStateManager.Instance.CurrentGameState == GameState.PLAYING &&
            !projectile.activeSelf && (lastProjectilDestroy < Time.time))
            InvokeProjectile();
    }

    public void UpdateTimeProjectileDestroy() => lastProjectilDestroy = Time.time + Random.Range(1f, projectileTimeCD);

    void InvokeProjectile()
    {
        projectile.transform.position = transform.position;
        projectile.SetActive(true);
        projectile.GetComponent<ProjectileMovement>().StartMovement();
    }
}
