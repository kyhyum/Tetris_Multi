using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }

    public TetrisData[] tetrises;
    public NextBlocks[] NextBlocks;

    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10,20);
    List<int> Blocks = new List<int>();

    public RectInt Bounds {
        get {
            Vector2Int position = new Vector2Int(-this.boardSize.x /2,-this.boardSize.y /2);
            return new RectInt(position, this.boardSize);
        } 
    }

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren <Piece>();

        for(int i = 0; i < this.tetrises.Length; i++)
        {
            this.tetrises[i].Initialize();
        }
    }

    private void setRandomBlock()
    {
        int random = UnityEngine.Random.Range(0, this.tetrises.Length);
        Blocks.Add(random);
    }

    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            setRandomBlock();
        }
        BlocksListSet();
    }
    public void BlocksListSet()
    {
        SpawnPiece(Blocks[0]);
        Blocks.RemoveAt(0);
        setRandomBlock();
        for(int i = 0; i < 5; i++)
        {
            NextBlocks[i].set_active(Blocks[i]);
        }
    }

    private void SpawnPiece(int blocknum)
    {
        TetrisData data = this.tetrises[blocknum];

        this.activePiece.Initialize(this,this.spawnPosition, data);

        if (IsValidPosition(this.activePiece, this.spawnPosition))
        {
            Set(this.activePiece);
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        this.tilemap.ClearAllTiles();
    }
    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        for(int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public void ClearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
            }
            else
            {
                row++;
            }
        }
    }

    private bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!this.tilemap.HasTile(position))
            {
                return false;
            }
        }

        return true;
    }

    private void LineClear(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);
            }

            row++;
        }
    }
}
