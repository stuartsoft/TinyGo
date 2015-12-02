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

    public void playAI()
    {
        int playableSpotsCount = board.PossibleMoves().Count;
        if (playableSpotsCount == 0)
            return;//there are no playable spots

        Node choice;
        choice.move = Vector2.zero;

        if(board.CountPieces().x + board.CountPieces().y < 3)
        {
            for (int i = 0; i < board.StartingMoves.Count; i++)
            {
                if (board.pieceMatrix[(int)board.StartingMoves[i].x][(int)board.StartingMoves[i].y].color == Constants.CLEARCOLOR)
                {
                    //this starting move hasn't been made yet. Take it!
                    choice.move = board.StartingMoves[i];
                }    
            }
        }
        else//do normal ai
        {
            if (color == Constants.WHITECOLOR)
                choice = alphaBetaMin(board, Int32.MinValue, Int32.MaxValue, Constants.MAXDEPTH);
            else//black
                choice = alphaBetaMax(board, Int32.MinValue, Int32.MaxValue, Constants.MAXDEPTH);
        }

        board.PlayPiece((int)choice.move.x, (int)choice.move.y, color);

        return;
    }

    public IEnumerator playAICoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        playAI();

    }


    Node alphaBetaMax(Board B, int alpha, int beta, int depth)//black
    {
        List<Vector2> possible = B.PossibleMoves();

        if (depth == 0 || possible.Count == 0)
        {//only score the Board at the last iteration
            Node LeafNode;
            LeafNode.score = B.Score();
            LeafNode.move = new Vector2(0, 0);
            return LeafNode;
        }

        Node n;
        n.move = new Vector2(0, 0);
        n.score = 0;

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
        List<Vector2> possible = B.PossibleMoves();

        if (depth == 0 || possible.Count == 0)//only score the Board at the last iteration
        {//only score the Board at the last iteration
            Node LeafNode;
            LeafNode.score = B.Score();
            LeafNode.move = new Vector2(0, 0);
            return LeafNode;
        }

        Node n;
        n.move = new Vector2(0, 0);
        n.score = 0;

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
