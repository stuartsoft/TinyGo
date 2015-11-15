using UnityEngine;
using System.Collections;

public class GameSession : MonoBehaviour {
    public Player Player1;
    public Player Player2;
    public Board mainBoard;

    int PlayerTurn;

    void Start()
    {
        mainBoard = new Board(Constants.BOARDSIZE);
        Player1 = new Player(Constants.BLACKCOLOR, mainBoard, false);
        Player2 = new Player(Constants.WHITECOLOR, mainBoard, false);

        PlayerTurn = 1;
    }

    void Update()
    {

    }

}
