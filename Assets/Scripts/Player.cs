using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Node
{
    public int score;
    public Vector2 move;
}

public class Player {
    public bool AI { get; private set; }
    public Color color { get; private set; }
    public Board board;//reference to the actual Board

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

    public IEnumerator playAI()
    {
        yield return new WaitForSeconds(0.1f);
        //run artificial intelligence here

        //alphaBetaMin/Max call
        //use returned node.move as the position to play

        //call mainBoard.PlayPiece(node.move);
        Node choice;

        if (color == Constants.WHITECOLOR)
            choice = alphaBetaMin(board, Int32.MinValue, Int32.MaxValue, Constants.MAXDEPTH);
        else//black
            choice = alphaBetaMax(board, Int32.MinValue, Int32.MaxValue, Constants.MAXDEPTH);
        Debug.Log(choice.move);
        board.PlayPiece((int)choice.move.x, (int)choice.move.y, color);
        
    }

    Node alphaBetaMax(Board B, int alpha, int beta, int depth)//black
    {
        if (depth == 0)
        {//only score the Board at the last iteration
            Node LeafNode;
            LeafNode.score = B.Score();
            LeafNode.move = new Vector2(0, 0);
            return LeafNode;
        }

        Node n;
        n.move = new Vector2(0, 0);
        n.score = 0;

        List<Vector2> possible = B.PossibleMoves();

        for (int i = 0; i < possible.Count; i++)
        {
            int x = (int)possible[i].x;
            int y = (int)possible[i].y;
            Board temp = B.cloneBoard();
            temp.PlayPiece(x, y, Constants.BLACKCOLOR);
            int score = alphaBetaMin(temp, alpha, beta, depth - 1).score;
            if (score >= beta)
            {
                n.score = beta;
                n.move = new Vector2(x, y);
                return n;//pruning, don't use this branch
            }
            if (score > alpha)//update alpha for this branch
            {
                alpha = score;
                n.move = new Vector2(x, y);
            }
        }
        n.score = alpha;
        return n;
    }

    Node alphaBetaMin(Board B, int alpha, int beta, int depth)//white
    {
        if (depth == 0)//only score the Board at the last iteration
        {//only score the Board at the last iteration
            Node LeafNode;
            LeafNode.score = -1* B.Score();
            LeafNode.move = new Vector2(0, 0);
            return LeafNode;
        }

        Node n;
        n.move = new Vector2(0, 0);
        n.score = 0;

        List<Vector2> possible = B.PossibleMoves();

        for (int i = 0; i < possible.Count; i++)
        {
            int x = (int)possible[i].x;
            int y = (int)possible[i].y;
            Board temp = B.cloneBoard();
            temp.PlayPiece(x, y, Constants.WHITECOLOR);
            int score = alphaBetaMax(temp, alpha, beta, depth - 1).score;
            if (score <= alpha)
            {
                n.score = alpha;
                n.move = new Vector2(x, y);
                return n;
            }
            if (score < beta)
            {
                beta = score;
                n.move = new Vector2(x, y);
            }
        }
        n.score = beta;
        return n;
    }


}
