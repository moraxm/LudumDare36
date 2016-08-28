using UnityEngine;
using System.Collections;

public class DragableWithMouse : MonoBehaviour
{
    public LayerMask layerMask;
    Rigidbody m_rigidBody;
    Quaternion m_originalRotation;
    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
        m_rigidBody = GetComponent<Rigidbody>();
        m_originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        
    }

    public void OnMouseUp()
    {
        m_rigidBody.isKinematic = false;
        m_rigidBody.GetComponent<Collider>().enabled = true;
    }

    public void OnMouseDrag()
    {
        m_rigidBody.isKinematic = true;
        m_rigidBody.GetComponent<Collider>().enabled = false;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 3f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
            transform.position = hit.point;
            transform.rotation = m_originalRotation;
            Debug.Log("Mouse position:" + mousePos);
        }
        
        //Debug.Log("Mouse position:" + mousePos);
        //Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
        //Debug.Log(worldPoint);
        //Vector3 newPosition = transform.position;
        //newPosition.x = worldPoint.x;
        //newPosition.y = worldPoint.y;
        //transform.position = newPosition;
    }


 
}
