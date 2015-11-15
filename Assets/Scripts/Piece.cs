using UnityEngine;
using System.Collections;

public class Piece {
    public Color color;

    public Piece()
    {
        color = Constants.CLEARCOLOR;
    }

    public Piece(Color c)
    {
        color = c;
    }
}
