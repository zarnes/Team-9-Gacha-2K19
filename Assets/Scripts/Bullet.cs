using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 m_velocity;
    public float m_speed = 10.0f;
    float m_minSqrDistanceToTarget = float.MaxValue;
    public Vector3 m_target;

    void Update()
    {
        transform.position += m_velocity * m_speed * Time.deltaTime;

        float sqrDistance = Vector3.SqrMagnitude(m_target - transform.position);
        if (sqrDistance < m_minSqrDistanceToTarget)
        {
            m_minSqrDistanceToTarget = sqrDistance;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


}
