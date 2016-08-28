using UnityEngine;
using System.Collections;

public class StructureDetector : MonoBehaviour {

    Structure m_structure;
    Structure m_structure2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Scan();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Compare();
        }

	}

    private void Compare()
    {
        Debug.Log("Comparing...");
        if (m_structure != null && m_structure2 != null)
        {
            bool equals = m_structure.Equals(m_structure2);
            Debug.Log(equals);
        }
    }

    void Scan()
    {
        Debug.Log("Scanning...");
        Vector3 boxSize = transform.localScale * 0.5f;
        Collider[] pieces = Physics.OverlapBox(transform.position, boxSize, Quaternion.identity/*, LayerMask.NameToLayer("Pieces")*/);
        Structure structure = new Structure();
        structure.setPieces(ref pieces);
        if (m_structure == null)
        {
            Debug.Log("Structure 1 detected");
            m_structure = structure;
        }
        else
        {
            Debug.Log("Structure 2 detected");
            m_structure2 = structure;
        }

    }

}
