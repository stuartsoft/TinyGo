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
    public Board Board;//reference to the actual Board

    public Player(Color c, ref Board b)
    {
        AI = false;
        color = c;
        Board = b;
    }

    public Player(Color c, ref Board b, bool isAI)
    {
        AI = isAI;
        color = c;
        Board = b;
    }

    public Vector2 playAI()
    {
        //run artificial intelligence here
        
        //alphaBetaMin/Max call
        //use returned node.move as the position to play

        //call mainBoard.PlayPiece(node.move);
        

        return new Vector2(0, 0);
    }

    Node alphaBetaMax(Board B, int alpha, int beta, int depth)
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
            Board temp = B.clone();
            temp.PlayPiece(x, y, color);
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

    Node alphaBetaMin(Board B, int alpha, int beta, int depth)
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
            Board temp = B.clone();
            temp.PlayPiece(x, y, color);
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
