using UnityEngine;

public class GridSystem
{
    private int height;
    private int length;

    public enum GameState { EnCours, PlayerWon, Draw };

    public GameState gameState;

    private TokenTemplate[,] grid;

    public GridSystem(int n, int m)
    {
        this.length = n;
        this.height = m;
        this.grid = new TokenTemplate[n,m];
        this.gameState = GameState.EnCours;
    }

    public TokenTemplate GetTokenWithCoords(int x, int y)
    {
        if (OutOfBound(x, y))
            return null;

        return this.grid[x, y];
    }

    public Vector2Int GetDim()
    {
        return new Vector2Int(length, height);
    }

    public Vector2Int AddTokenToColumn(int x, TokenTemplate token)
    {
        if (OutOfBound(x, 0))
        {
            Debug.Log("out of bound !");
            return new Vector2Int(-1, -1);
        }

        if (IsAFullColumn(x))
        {
            Debug.Log("column full !");
            return new Vector2Int(-1, -1);
        }

        if (token == null)
        {
            Debug.Log("empty token !");
        }

        for (int y = 0; y < height; y++)
        {
            if (GetTokenWithCoords(x, y) == null)
            {
                SetTokenWithCoords(x, y, token);
                return new Vector2Int(x, y);
            }
        }

        Debug.Log("inpossible ...");

        return new Vector2Int(-1, -1);
    }

    public void SetTokenWithCoords(int x, int y, TokenTemplate token)
    {
        grid[x, y] = token;
    }

    public bool IsGridFilled()
    {
        for (int x = 0; x < length; x++)
        {
            if (IsAFullColumn(x) == false)
                return false;
        }
        this.gameState = GameState.Draw;
        return true;
    }

    public bool IsAFullColumn(int x)
    {
        return GetTokenWithCoords(x, height - 1) != null;
    }

    private bool OutOfBound(int x, int y)
    {
        return x < 0 || y < 0 || x >= length || y >= height;
    }

    public bool DidWin(TokenTemplate token)
    {
        int playerId = token.id;

        //horizontal
        for (int x = 0; x < this.length - 4; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                if (GetTokenWithCoords(x, y) != null && GetTokenWithCoords(x + 1, y) != null && GetTokenWithCoords(x + 2, y) != null && GetTokenWithCoords(x + 3, y) != null)
                {
                    if (GetTokenWithCoords(x, y).id == playerId && GetTokenWithCoords(x + 1, y).id == playerId && GetTokenWithCoords(x + 2, y).id == playerId && GetTokenWithCoords(x + 3, y).id == playerId)
                    {
                        this.gameState = GameState.PlayerWon;
                        return true;
                    }
                }
            }
        }

        //vertical
        for (int x = 0; x < this.length; x++)
        {
            for (int y = 0; y < this.height - 4; y++)
            {
                if (GetTokenWithCoords(x, y) != null && GetTokenWithCoords(x, y + 1) != null && GetTokenWithCoords(x, y + 2) != null && GetTokenWithCoords(x, y + 3) != null)
                {
                    if (GetTokenWithCoords(x, y).id == playerId && GetTokenWithCoords(x, y + 1).id == playerId && GetTokenWithCoords(x, y + 2).id == playerId && GetTokenWithCoords(x, y + 3).id == playerId)
                    {
                        this.gameState = GameState.PlayerWon;
                        return true;
                    }
                }
            }
        }

        //increasing diagonal
        for (int x = 0; x < this.length - 4; x++)
        {
            for (int y = 0; y < this.height - 4; y++)
            {
                if (GetTokenWithCoords(x, y) != null && GetTokenWithCoords(x + 1, y + 1) != null && GetTokenWithCoords(x + 2, y + 2) != null && GetTokenWithCoords(x + 3, y + 3) != null)
                {
                    if (GetTokenWithCoords(x, y).id == playerId && GetTokenWithCoords(x + 1, y + 1).id == playerId && GetTokenWithCoords(x + 2, y + 2).id == playerId && GetTokenWithCoords(x + 3, y + 3).id == playerId)
                    {
                        this.gameState = GameState.PlayerWon;
                        return true;
                    }
                }
            }
        }

        //decreasing diagonal
        for (int x = 3; x < this.length - 1; x++)
        {
            for (int y = 3; y < this.height - 1; y++)
            {
                if (GetTokenWithCoords(x, y) != null && GetTokenWithCoords(x - 1, y - 1) != null && GetTokenWithCoords(x - 2, y - 2) != null && GetTokenWithCoords(x - 3, y - 3) != null)
                {
                    if (GetTokenWithCoords(x, y).id == playerId && GetTokenWithCoords(x - 1, y - 1).id == playerId && GetTokenWithCoords(x - 2, y - 2).id == playerId && GetTokenWithCoords(x - 3, y - 3).id == playerId)
                    {
                        this.gameState = GameState.PlayerWon;
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
