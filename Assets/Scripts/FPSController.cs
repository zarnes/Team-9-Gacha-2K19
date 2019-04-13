using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    public float m_pitch, m_yaw = 0.0f;
    public Transform m_cameraTransform;
    public float m_cameraSensibility = 1.75f;
    public float m_mouvementSpeed = 7.0f;
    public float m_jumpForce = 100.0f;

    public Bullet m_bulletPrefab;
    public Transform m_muzzleFlashPosition;

    public Image m_crosshair;

    void Start()
    {
        Screen.lockCursor = true;
    }

    void Update()
    {
        m_yaw += Input.GetAxis("Mouse X") * m_cameraSensibility;
        m_pitch -= Input.GetAxis("Mouse Y") * m_cameraSensibility;
        m_pitch = Mathf.Clamp(m_pitch, -89.9f, 89.9f);
        m_cameraTransform.rotation = Quaternion.Euler(m_pitch, m_yaw, 0.0f);
        transform.rotation = Quaternion.Euler(0.0f, m_yaw, 0.0f);
        Rigidbody rigidbody = transform.GetComponent<Rigidbody>();

        Vector3 velocity = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        velocity.Normalize();
        velocity *= m_mouvementSpeed * Time.deltaTime;
        transform.position += velocity;

        Debug.DrawLine(transform.position, transform.position + velocity, Color.red);
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(Vector3.up * m_jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // Bit shift the index of the layer (8) to get a bit mask
            int layerMask = 1 << 10;

            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            RaycastHit hit;
            Debug.Log(m_crosshair.rectTransform.position);
            float distance = 300.0f;
            Ray ray = Camera.main.ScreenPointToRay(m_crosshair.rectTransform.position);
            Vector3 target;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray, out hit, distance, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
                target = hit.point;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.white);
                target = ray.origin + ray.direction * distance;
            }


            Bullet bullet = Instantiate<Bullet>(m_bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.m_velocity = target - transform.position;
            bullet.m_velocity.Normalize();
            bullet.m_target = target;
        }
    }
}
