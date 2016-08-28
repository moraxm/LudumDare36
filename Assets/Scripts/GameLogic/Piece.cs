using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Piece : MonoBehaviour
{
    public Structure.StructureType type;
    Vector3 m_origin;
    public Vector3 origin
    {
        get { return m_origin; }
    }

    public void Update()
    {
        if (Application.isEditor)
        {
            //if (transform
        }
    }



    internal bool SamePosition(Piece pieceOther)
    {
        if (Mathf.Abs(pieceOther.transform.localPosition.x - transform.localPosition.x) > 0.3f) return false;
        if (Mathf.Abs(pieceOther.transform.localPosition.y - transform.localPosition.y) > 0.3f) return false;
        if (Mathf.Abs(pieceOther.transform.localPosition.z - transform.localPosition.z) > 0.3f) return false;
        return true;
    }
}
