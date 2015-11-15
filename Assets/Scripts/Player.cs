using UnityEngine;
using System.Collections;

public class Player {
    public bool AI { get; private set; }
    public Color color { get; private set; }
    public Board board;

    public Player(Color c, Board b)
    {
        AI = false;
        color = c;
    }

    public Player(Color c, Board b, bool isAI)
    {
        AI = isAI;
        color = c;
    }

    public void play()
    {

    }
}
