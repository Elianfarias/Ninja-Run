using UnityEngine;

namespace Assets.Scripts
{
    public class FoodManager : MonoBehaviour
    {
        public static FoodManager Instance;

        [SerializeField]
        GameObject foodPrefab;
        [SerializeField]
        float limitX = 2.5f;
        [SerializeField]
        float limitY = 2;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                var randPos = GetRandomPosition();
                foodPrefab = Instantiate(foodPrefab, randPos, Quaternion.identity);
            }
            else
                Destroy(gameObject);
        }

        void Update()
        {
            if (!foodPrefab.activeSelf)
            {
                foodPrefab.transform.position = GetRandomPosition();
                ChangeActiveFood(true);
            }
        }

        Vector3 GetRandomPosition()
        {
            Vector3 randomPosition = new(
                Random.Range(-limitX, limitX),
                Random.Range(-limitY, limitY),
                0
            );
            return randomPosition;
        }

        public void ChangeActiveFood(bool isActive) => foodPrefab.SetActive(isActive);

    }
}