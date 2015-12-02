using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class BoardPrefab : MonoBehaviour {
    public Board mainBoard;
    Player Player1;
    Player Player2;
    public GameObject BoardTilePrefab;
    public GameObject BoardClickPrefab;
    public GameObject PiecePrefab;
    List<List<GameObject>> BoardTiles;
    List<List<GameObject>> BoardClickTiles;
    List<GameObject> BoardPieces;
    GameObject BoardBackground;

    public Text txtPlayer1Score;
    public Text txtPlayer2Score;
    public Text txtStatus;

    float tileSize = 1.0f;

    public float tileSpacing = 0.1f;

	// Use this for initialization
	void Start () {
        mainBoard = new Board(Constants.BOARDSIZE);
        Player1 = new Player(Constants.BLACKCOLOR, ref mainBoard, false);
        Player2 = new Player(Constants.WHITECOLOR, ref mainBoard, true);

        BoardPieces = new List<GameObject>();

        //create regular board tiles
        BoardTiles = new List<List<GameObject>>();
        for (int i = 0; i < mainBoard.boardSize-1; i++)
        {
            List<GameObject> row = new List<GameObject>();
            float yprogress = (float)i / (mainBoard.boardSize - 1);

            for (int j = 0; j < mainBoard.boardSize-1; j++)
            {
                GameObject temp = Instantiate(BoardTilePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                temp.transform.SetParent(transform);
                float wid = -((mainBoard.boardSize - 1))*tileSize;
                float xprogress = (float)j / (mainBoard.boardSize - 1);
                float xpos = ((xprogress) * wid) - wid/2.0f;
                xpos -= tileSize / 2;
                xpos *= 1.1f;

                float zpos = ((yprogress) * wid) - wid / 2.0f;
                zpos -= tileSize / 2;
                zpos *= 1.1f;

                temp.transform.position = new Vector3(xpos, 0, zpos);
                temp.name = "Panel " + i.ToString() + ", " + j.ToString();
                row.Add(temp);
            }
            BoardTiles.Add(row);
        }

        //create board background, giving the illusion of lines
        BoardBackground = Instantiate(BoardTilePrefab, Vector3.zero, Quaternion.identity) as GameObject;
        float backgroundSize = (tileSize / 10.0f) * mainBoard.boardSize;
        BoardBackground.transform.position = new Vector3(BoardBackground.transform.position.x, -0.01f, BoardBackground.transform.position.z);
        BoardBackground.transform.localScale = new Vector3(backgroundSize, backgroundSize, backgroundSize);
        Renderer rend = BoardBackground.GetComponent<Renderer>();
        rend.enabled = true;
        rend.material.color = Constants.BLACKCOLOR;
        BoardBackground.transform.parent = transform;

        //now create click panels, offset so that clickable panels will be right on top of the line intersections
        BoardClickTiles = new List<List<GameObject>>();
        for (int i = 0; i < mainBoard.boardSize; i++)
        {
            List<GameObject> row = new List<GameObject>();
            float yprogress = (float)i / (mainBoard.boardSize - 1);

            for (int j = 0; j < mainBoard.boardSize; j++)
            {

                GameObject temp = Instantiate(BoardClickPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                temp.transform.SetParent(transform);
                float wid = -((mainBoard.boardSize - 1)) * tileSize;
                float xprogress = (float)j / (mainBoard.boardSize - 1);
                float xpos = ((xprogress) * wid) - wid / 2.0f;
                //xpos -= tileSize / 2;
                xpos *= 1.1f;

                float zpos = ((yprogress) * wid) - wid / 2.0f;
                //zpos -= tileSize / 2;
                zpos *= 1.1f;

                temp.transform.position = new Vector3(xpos, 0.01f, zpos);
                temp.name = "ClickPanel " + i.ToString() + ", " + j.ToString();
                row.Add(temp);

            }
            BoardClickTiles.Add(row);
        }
    }
	
    void RebuildBoard()
    {
        for (int i = 0; i < BoardPieces.Count; i++)
        {
            Destroy(BoardPieces[i]);
        }
        BoardPieces.Clear();

        for (int i = 0; i < mainBoard.pieceMatrix.Count; i++)
        {
            float yprogress = (float)i / (mainBoard.boardSize - 1);
            for (int j = 0;j<mainBoard.pieceMatrix.Count; j++)
            {
                if (mainBoard.pieceMatrix[i][j].color == Constants.CLEARCOLOR)
                    continue;//skip this piece

                GameObject tempPiece = Instantiate(PiecePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                tempPiece.transform.parent = transform;

                float wid = -((mainBoard.boardSize - 1)) * tileSize;
                float xprogress = (float)j / (mainBoard.boardSize - 1);
                float xpos = ((xprogress) * wid) - wid / 2.0f;
                //xpos -= tileSize / 2;
                xpos *= 1.1f;

                float zpos = ((yprogress) * wid) - wid / 2.0f;
                //zpos -= tileSize / 2;
                zpos *= 1.1f;

                tempPiece.transform.position = new Vector3(xpos, 0.16f, zpos);
                BoardPieces.Add(tempPiece);

                if (mainBoard.pieceMatrix[i][j].color == Constants.WHITECOLOR) {
                    Renderer rend = tempPiece.GetComponent<Renderer>();
                    rend.enabled = true;
                    rend.material.color = Constants.WHITECOLOR;
                }
            }
        }

        Vector2 score = mainBoard.CountPieces();

        txtPlayer1Score.GetComponent<Text>().text = "B Score: " + score.x;
        txtPlayer2Score.GetComponent<Text>().text = "W Score: " + score.y;
    }

	// Update is called once per frame
	/// <summary>
    /// 
    /// </summary>
    void Update () {
        if (mainBoard.needsRefreshModel)
        {
            RebuildBoard();
            mainBoard.needsRefreshModel = false;
        }


        if (mainBoard.PossibleMovesNum == 0)
            txtStatus.GetComponent<Text>().text = "End Game";
        if (mainBoard.CurrentPlayerColor == Constants.BLACKCOLOR)
            txtStatus.GetComponent<Text>().text = "Blacks's Turn";
        else if (mainBoard.CurrentPlayerColor == Constants.WHITECOLOR)
            txtStatus.GetComponent<Text>().text = "White's Turn";

        //if (mainBoard.PossibleMoves().Count == 0)
        //    txtStatus.GetComponent<Text>().text = "End of Game";

        if (Player1.AI && Player2.AI && mainBoard.PossibleMovesNum > 0)
        {
            if (mainBoard.CurrentTurn == 0)
            {
                Player1.playAI();
            }
            else if (mainBoard.CurrentTurn == 1)
            {
                Player2.playAI();
            }
        }
        else if (mainBoard.PossibleMovesNum > 0)//else if both players are not both AI...
        {

            //transform.Rotate(Vector3.up, Time.deltaTime * 20);
            if (Input.GetMouseButtonDown(0) && ((mainBoard.CurrentTurn == 0 && !Player1.AI) || (mainBoard.CurrentTurn == 1 && !Player2.AI)))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    string GameObjName = hit.transform.gameObject.name;
                    //Debug.Log(GameObjName);
                    string[] delimiters = { ",", " " };
                    string[] tok = GameObjName.Split(delimiters, StringSplitOptions.None);

                    if (tok[0] == "ClickPanel")
                    {
                        if (mainBoard.CurrentTurn == 0)
                            mainBoard.PlayPiece(Int32.Parse(tok[1]), Int32.Parse(tok[3]), Constants.BLACKCOLOR);
                        else
                            mainBoard.PlayPiece(Int32.Parse(tok[1]), Int32.Parse(tok[3]), Constants.WHITECOLOR);
                    }
                }

                if (mainBoard.CurrentTurn == 0 && Player1.AI)
                {
                    StartCoroutine(Player1.playAICoroutine());
                }
                else if (mainBoard.CurrentTurn == 1 && Player2.AI)
                {
                    StartCoroutine(Player2.playAICoroutine());
                }
            }
        }
        
    }
}
