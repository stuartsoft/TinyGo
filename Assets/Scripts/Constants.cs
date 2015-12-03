/*
Constants.cs
This is where constants are stored.
*/

using UnityEngine;
using System.Collections;

public class Constants {
    public static Color BLACKCOLOR { get { return Color.black; } }
    public static Color WHITECOLOR { get { return Color.white; } }
    public static Color CLEARCOLOR { get { return Color.clear; } }
    public static int BOARDSIZE = 5;//5 x 5 board size
    public static int MAXDEPTH = 3;//minimax search depth
}
