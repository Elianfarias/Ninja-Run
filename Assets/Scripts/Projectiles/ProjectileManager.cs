using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] projectileSpawners;

    public static ProjectileManager Instance { get; private set; }
    int lastMaxScore = 0;
    int countSpawnersActivated = 0;
    readonly int scoreThreshold = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ResetProjectileSpawners();
    }

    void Update()
    {
        if (lastMaxScore + scoreThreshold <= ScoreManager.Instance.totalScore)
        {
            lastMaxScore = ScoreManager.Instance.totalScore;
            ActivateProjectileSpawn();
        }
    }

    void ActivateProjectileSpawn()
    {
        if (projectileSpawners.Length >= countSpawnersActivated + 1)
        {
            bool setActive = false;
            Dictionary<int, bool> randIndexesUsed = new();

            while (setActive == false)
            {
                var randomIndex = Random.Range(0, projectileSpawners.Length);
                // Check if the random index has already been used
                if (randIndexesUsed.ContainsKey(randomIndex) && randIndexesUsed[randomIndex])
                    continue;

                // Mark the index as used
                randIndexesUsed[randomIndex] = true;
                var projectileSpawner = projectileSpawners[randomIndex];

                // Check if the projectile spawner is already active
                if (projectileSpawner.activeSelf)
                    continue;

                // Activate the projectile spawner
                projectileSpawner.SetActive(true);
                countSpawnersActivated++;
                setActive = true;
            }
        }
    }

    public void ResetProjectileSpawners()
    {
        lastMaxScore = 0;

        foreach (GameObject spawner in projectileSpawners)
        {
            spawner.SetActive(false);
        }
    }

    public void ActiveFirstSpawn()
    {
        projectileSpawners[0].SetActive(true);
        countSpawnersActivated = 1;
    }
}
