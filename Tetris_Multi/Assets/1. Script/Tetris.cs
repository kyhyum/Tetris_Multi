using UnityEngine.Tilemaps;
using UnityEngine;
public enum Tetris
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z,
}

[System.Serializable]
public struct TetrisData
{
    public Tetris tetris;
    public Tile tile;
    public Vector2Int[] cells { get; private set; }
    public Vector2Int[,] wallKicks { get; private set; }

    public void Initialize()
    {
        this.cells = Data.Cells[this.tetris];
        this.wallKicks = Data.WallKicks[this.tetris];
    }
}

