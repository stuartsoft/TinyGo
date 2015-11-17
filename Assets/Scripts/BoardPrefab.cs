﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BoardPrefab : MonoBehaviour {
    public Board mainBoard;
    public Player Player1;
    public Player Player2;
    public GameObject BoardTilePrefab;
    public GameObject BoardClickPrefab;
    public GameObject PiecePrefab;
    List<List<GameObject>> BoardTiles;
    List<List<GameObject>> BoardClickTiles;
    List<GameObject> BoardPieces;
    GameObject BoardBackground;

    public float tileSpacing = 0.1f;

	// Use this for initialization
	void Start () {
        mainBoard = new Board(Constants.BOARDSIZE);
        Player1 = new Player(Constants.BLACKCOLOR, mainBoard, false);
        Player2 = new Player(Constants.WHITECOLOR, mainBoard, false);

        BoardPieces = new List<GameObject>();

        //create regular board tiles
        BoardTiles = new List<List<GameObject>>();
        float tileSize = 1.0f;
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
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                string GameObjName = hit.transform.gameObject.name;

                string[] delimiters = { ",", " " };
                string[] tok = GameObjName.Split(delimiters, StringSplitOptions.None);

                if (tok[0] == "ClickPanel")
                {
                    Debug.Log(tok[1] + ", " + tok[3]);
                    GameObject tempPiece = Instantiate(PiecePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    tempPiece.transform.parent = transform;
                    tempPiece.transform.position = new Vector3(hit.transform.position.x, 0.16f, hit.transform.position.z);
                    BoardPieces.Add(tempPiece);

                    if (mainBoard.CurrentTurn == 0)
                        mainBoard.PlayPiece(Int32.Parse(tok[1]), Int32.Parse(tok[3]), Constants.BLACKCOLOR);
                    else
                    {
                        mainBoard.PlayPiece(Int32.Parse(tok[1]), Int32.Parse(tok[3]), Constants.WHITECOLOR);
                        Renderer rend = tempPiece.GetComponent<Renderer>();
                        rend.enabled = true;
                        rend.material.color = Constants.WHITECOLOR;
                    }
                }
            }
        }
    }
}