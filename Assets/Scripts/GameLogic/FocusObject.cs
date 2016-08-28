using UnityEngine;
using System.Collections;

public class FocusObject : MonoBehaviour
{
    //public float startOffset = 3.0f;
    public Transform target;
    Vector3 m_targetPosition;
    public float speed = 2.0f;

    float m_radius;
    float m_acumTime;
    private RenderTexture m_targetTexture;
    // Use this for initialization
    void Start()
    {
        m_targetTexture = GetComponent<Camera>().targetTexture;
        GetComponent<Camera>().targetTexture = null;
        enabled = false;
        //transform.position = target.position + target.forward * startOffset;
        m_targetPosition = getCenterPosition(target);
        Vector3 direction = m_targetPosition -transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        m_radius = direction.magnitude;
    }

    private Vector3 getCenterPosition(Transform parent)
    {
        Vector3 pos = Vector3.zero;
        foreach (Transform t in parent)
        {
            pos += t.position;
        }
        pos /= parent.childCount;
        return pos;
    }

    public void OnEnable()
    {
        m_acumTime = 0;
    }

    public void StartFocusMove()
    {
        enabled = true;
    }

    public void EndFocusMove()
    {
        enabled = false;
        // Focus the target
        Vector3 newPosition = m_targetPosition;
        newPosition += m_radius * -target.forward;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        Vector3 direction = m_targetPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        // Set Target texture for ui
        GetComponent<Camera>().targetTexture = m_targetTexture;
    }

    // Update is called once per frame
    void Update()
    {
        m_acumTime += Time.deltaTime;
        Vector3 newPosition = transform.position;
        newPosition.x = m_targetPosition.x + m_radius * Mathf.Sin(speed * m_acumTime);
        newPosition.z = m_targetPosition.z + m_radius * Mathf.Cos(speed * m_acumTime);
        transform.position = newPosition;
        Vector3 direction = m_targetPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
