using System;
using UnityEngine;


namespace Dialog.Animation
{
    public class CharacterData
    {
        public bool isVisible;
        public float timer;

        public MeshData source;
        public MeshData current;
    }

    public struct MeshData : System.IEquatable<MeshData>
    {
        public Vector3[] positions;
        public Color32[] colors;


        public bool Equals(MeshData other)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if (positions[i] != other.positions[i])
                    return false;
            }

            for (int i = 0; i < colors.Length; i++)
            {
                if (!colors[i].Equals(other.colors[i]))
                    return false;
            }

            return true;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < positions.Length; i++)
            {
                sb.Append(positions[i].ToString());
                sb.Append(" ");
                sb.Append(colors[i].ToString());
                sb.Append(" - ");
            }
            return sb.ToString();
        }
    }
}
