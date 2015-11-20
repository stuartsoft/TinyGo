using UnityEngine;
using System.Collections;

public class Player {
    public bool AI { get; private set; }
    public Color color { get; private set; }
    public Board board;//reference to the actual board

    public Player(Color c, ref Board b)
    {
        AI = false;
        color = c;
        board = b;
    }

    public Player(Color c, ref Board b, bool isAI)
    {
        AI = isAI;
        color = c;
        board = b;
    }

    public Vector2 playAI()
    {
        //run artificial intelligence here
        return new Vector2(0, 0);
    }
}
