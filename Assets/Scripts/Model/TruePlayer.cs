using UnityEngine;

public class TruePlayer : IPlayer
{
    public TruePlayer(string name, TokenTemplate token) : base(name, PlayerType.TruePlayer, token)
    {
        Debug.Log("Cr�ation du vrai joueur " + name + " termin�e !");
    }

    public override int GetColumnInput()
    {
        return -1;
    }
}
