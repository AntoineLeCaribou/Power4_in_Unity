using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    private Game game;
    public GridView gridView;

    public TokenTemplate yellowToken;
    public TokenTemplate redToken;

    private IPlayer currentPlayer = null;
    private Vector2Int newCoords;

    private int xinput;

    public List<GameObject> clickColumnArea;

    public GameObject ghostToken;

    void Awake()
    {
        ghostToken.SetActive(false);
        gridView.NewGame();
    }

    public void NewGame()
    {
        for (int i = 0; i < clickColumnArea.Count; i++)
        {
            clickColumnArea[i].SetActive(true);
        }

        int dimx = 7;
        int dimy = 6;

        IPlayer player1 = new TruePlayer("Jaune", yellowToken);
        IPlayer player2 = new TruePlayer("Rouge", redToken);

        game = new Game(player1, player2, 0, dimx, dimy);

        gridView.UpdateNames(player1, player2);

        //set next player active
        game.NextPlayer();

        TryToPlayNextPlayer();
    }

    public void GhostInputEnter(int x)
    {
        if (game.GetGrid().IsAFullColumn(x))
        {
            return;
        }
        ghostToken.GetComponent<SpriteRenderer>().sprite = currentPlayer.token.sprite;
        gridView.GhostInput(x, ghostToken);
        ghostToken.SetActive(true);
    }

    public void GhostInputExit()
    {
        ghostToken.SetActive(false);
    }

    public void SetInput(int x)
    {
        if (game.GetGrid().gameState != GridSystem.GameState.EnCours)
            return;

        if (currentPlayer.playerType == IPlayer.PlayerType.TruePlayer)
        {
            print("SetInput " + x);
            this.xinput = x;

            newCoords = game.TryInput(xinput);
            if (newCoords.x == -1)
            {
                print("bad input !");
                return;
            }
            ghostToken.SetActive(false);
            gridView.AddNewToken(newCoords.x, newCoords.y, currentPlayer.token);

            if (game.GetGrid().IsAFullColumn(x))
            {
                clickColumnArea[x].SetActive(false);
            }

            if (game.IsOver())
            {
                DisplayEnd();
                return;
            }
            
            TryToPlayNextPlayer();
        }
        else
            print("not a true player");
    }

    private void TryToPlayNextPlayer()
    {
        if (game.GetGrid().gameState != GridSystem.GameState.EnCours)
            return;

        //set next player active
        game.NextPlayer();

        //gather the active player
        currentPlayer = game.GetCurrentPlayer();

        GhostInputEnter(newCoords.x);
        gridView.UpdatePlayerTokenOrder(game.GetCurrentPlayerIndex());

        //print new turn
        print(currentPlayer.name + "'s turn");

        //play the bot
        if (currentPlayer.playerType == IPlayer.PlayerType.Bot)
        {
            while (true)
            {
                xinput = currentPlayer.GetColumnInput();

                newCoords = game.TryInput(xinput);
                if (newCoords.x != -1)
                    break;
            }

            gridView.AddNewToken(newCoords.x, newCoords.y, currentPlayer.token);

            if (game.GetGrid().IsAFullColumn(xinput))
            {
                clickColumnArea[xinput].SetActive(false);
            }

            if (game.IsOver())
            {
                DisplayEnd();
                return;
            }
           
            TryToPlayNextPlayer();
        }
    }

    private void DisplayEnd()
    {
        print("Fin de la partie !");
        if (game.GetGrid().gameState == GridSystem.GameState.Draw)
        {
            print("Egalité !!!");
            gridView.SetFinish("Egalité :)");
        }
        else if (game.GetGrid().gameState == GridSystem.GameState.PlayerWon)
        {
            print(currentPlayer.name + " a gagné la partie !!");
            gridView.SetFinish(currentPlayer.name + " a gagné la partie !");
        }
        else
        {
            print("bad end occured ...");
            gridView.SetFinish("Il y a un méchant bogue ...");
        }
    }
}
