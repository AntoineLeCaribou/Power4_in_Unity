using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private GridSystem grid;
    private int currentPlayerIndex;
    private IPlayer[] players;
    public int turn;

    public Game(IPlayer player1, IPlayer player2, int indexStartingPlayer, int dimx, int dimy)
    {
        this.grid = new GridSystem(dimx, dimy);
        this.players = new IPlayer[2] {player1, player2};
        this.currentPlayerIndex = indexStartingPlayer;

        turn = 1;
    }

    public int GetCurrentPlayerIndex()
    {
        return currentPlayerIndex;
    }

    public void NextPlayer()
    {
        currentPlayerIndex++;

        if (currentPlayerIndex >= 2)
            currentPlayerIndex = 0;

        turn++;
    }

    public Vector2Int TryInput(int x)
    {
        return grid.AddTokenToColumn(x, GetCurrentPlayer().token);
    }

    public bool IsOver()
    {
        return grid.DidWin(players[currentPlayerIndex].token) || grid.IsGridFilled();
    }

    public IPlayer GetCurrentPlayer()
    {
        return players[currentPlayerIndex];
    }

    public GridSystem GetGrid()
    {
        return grid;
    }
}
