using System.Collections;
using UnityEngine;

public class LeafTreeGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject leavesTreePrefab;
    [SerializeField]
    float limitX = 2f;
    [SerializeField]
    float limitY = 0.5f;
    [SerializeField]
    float cdInstantiation = 2f;

    float lastInstantiationTime = 0;

    void Update()
    {
        if (Time.time - lastInstantiationTime > 1f)
        {
            lastInstantiationTime = Time.time + Random.Range(0, cdInstantiation);
            GenerateLeavesTree();
        }
    }

    void GenerateLeavesTree()
    {
        var position = new Vector3(Random.Range(-limitX, limitX), limitY, 0);
        var instance = Instantiate(leavesTreePrefab, position, Quaternion.identity);
    }
}
