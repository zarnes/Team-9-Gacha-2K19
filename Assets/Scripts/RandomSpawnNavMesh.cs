using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomSpawnNavMesh : MonoBehaviour
{
    [SerializeField]
    private int m_number_mob;

    [SerializeField]
    private GameObject m_prefabs;

    private NavMeshAgent m_nav_mesh_agent_;

    // Start is called before the first frame update
    void Start()
    {
        this.m_nav_mesh_agent_ = GetComponent<NavMeshAgent>();
        MakeSpawn(m_number_mob);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void MakeSpawn(int _iNumberOfMobs)
    {
        for (int i = 0; i < _iNumberOfMobs; i++)
        {
            Instantiate(m_prefabs, GetRandomPointInNavMesh(), Quaternion.identity);
        }
    }

    private Vector3 GetRandomPointInNavMesh()
    {
        Vector3 randomPosition = Random.insideUnitSphere * 5;
        randomPosition += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPosition, out hit, 100, 1);

        if (float.IsInfinity(hit.position.x) || float.IsInfinity(hit.position.y) || float.IsInfinity(hit.position.z))
            GetRandomPointInNavMesh();

        return hit.position;
    }

}
