using UnityEngine;
using System.Collections.Generic;

public class Structure
{
    public enum StructureType
    {
        CUBE_2X1X2,
        CUBE_1X1X2,
        CUBE_3X1X1,
        ARC_LEFT,
        ARC_RIGHT,

    }
    Dictionary<StructureType, List<Piece>> m_pieces;
    int m_size;
    public int size
    {
        get { return m_size; }
    }

    public void setPieces(ref Collider[] colliders)
    {
        m_pieces = new Dictionary<StructureType, List<Piece>>();
        foreach (Collider c in colliders)
        {
            Piece p = c.GetComponent<Piece>();
            if (p == null)
            {
                Debug.LogWarning("No Piece componente in the collider" + c.name);
            }
            else
            {
                if (!m_pieces.ContainsKey(p.type))
                    m_pieces.Add(p.type,new List<Piece>());

                m_pieces[p.type].Add(p);
                ++m_size;
            }
        }
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Structure)) return false;
        Structure other = (Structure) obj;
        if (other.size != size) return false;

        foreach (var v in other.m_pieces)
        {
            List<Piece> pieces = m_pieces[v.Key]; // Colliders of this class
            HashSet<Piece> detectedPieces = new HashSet<Piece>(); // Pieces that already have been detected
            if (pieces.Count != v.Value.Count) return false;
            bool ok = false;
            foreach (Piece pieceThis in pieces)
            {
                foreach (Piece pieceOther in v.Value)
                {
                    if (detectedPieces.Contains(pieceOther)) continue; // This piece has already been detected

                    if (pieceThis.SamePosition(pieceOther))
                    {
                        detectedPieces.Add(pieceOther); // mark this piece as detected
                        ok = true;
                        break;
                    }
                }
                if (ok) ok = false;
                else return false; // One piece hs no relative piece in the other structure
            }
        }

        return true;
    }

}
