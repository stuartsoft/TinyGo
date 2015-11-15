using UnityEngine;
using System.Collections;

public class Piece {
    public Color color;
    public Vector2 position { get; private set; }

    public Piece(Vector2 pos)
    {
        position = pos;
        color = Constants.CLEARCOLOR;
    }

    public Piece(Vector2 pos, Color c)
    {
        position = pos;
        color = c;
    }
}
