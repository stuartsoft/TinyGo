using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardPrefab : MonoBehaviour {
    public Board mainBoard;
    public Player Player1;
    public Player Player2;
    public GameObject BoardTilePrefab;
    List<List<GameObject>> BoardTiles;

    public float tileSpacing = 0.1f;

	// Use this for initialization
	void Start () {
        mainBoard = new Board(Constants.BOARDSIZE);
        Player1 = new Player(Constants.BLACKCOLOR, mainBoard, false);
        Player2 = new Player(Constants.WHITECOLOR, mainBoard, false);

        BoardTiles = new List<List<GameObject>>();
        float tileSize = 1.0f;
        //Debug.Log(tileScale);
        for (int i = 0; i < mainBoard.boardSize-1; i++)
        {
            List<GameObject> row = new List<GameObject>();
            for (int j = 0; j < mainBoard.boardSize-1; j++)
            {
                GameObject temp = Instantiate(BoardTilePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                temp.transform.SetParent(transform);
                float wid = -((mainBoard.boardSize - 1))*tileSize;
                float progress = (float)j / (mainBoard.boardSize - 1);
                float pos = ((progress) * wid) - wid/2.0f;
                pos -= tileSize / 2;
                pos *= 1.1f;
                
                temp.transform.position = new Vector3(pos, 0, 0);
                row.Add(temp);
            }
            BoardTiles.Add(row);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
