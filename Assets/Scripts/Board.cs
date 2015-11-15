using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Board {
    public List<List<Piece>> pieceMatrix;

    public Board(int d)
    {
        pieceMatrix = new List<List<Piece>>();
        for (int i = 0; i < d; i++)
        {
            List<Piece> temp = new List<Piece>();
            for (int j = 0; j < d; j++)
            {
                Piece tempPiece = new Piece();
                temp.Add(tempPiece);
            }
            pieceMatrix.Add(temp);
        }
    }

    public List<Piece> AdjacentPieces(int r, int c)
    {
        Color targetColor = pieceMatrix[r][c].color;

        List<Piece> adj = new List<Piece>();
        if (r - 1 > 0 && pieceMatrix[r-1][c].color == targetColor )
            adj.Add(pieceMatrix[r - 1][c]);
        if (c - 1 > 0 && pieceMatrix[r][c-1].color == targetColor)
            adj.Add(pieceMatrix[r][c - 1]);
        if (r + 1 > 0 && pieceMatrix[r + 1][c].color == targetColor)
            adj.Add(pieceMatrix[r + 1][c]);
        if (c + 1 > 0 && pieceMatrix[r][c+1].color == targetColor)
            adj.Add(pieceMatrix[r][c + 1]);

        return adj;
    }
}
