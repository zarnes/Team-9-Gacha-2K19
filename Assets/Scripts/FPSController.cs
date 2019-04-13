using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float m_pitch, m_yaw = 0.0f;
    public Transform m_cameraTransform;
    public float m_cameraSensibility = 1.75f;
    public float m_mouvementSpeed = 7.0f;
    public float m_jumpForce = 100.0f;

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
    }
}
