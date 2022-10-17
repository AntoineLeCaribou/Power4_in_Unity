using UnityEngine;

public class TruePlayer : IPlayer
{
    public TruePlayer(string name, TokenTemplate token) : base(name, PlayerType.TruePlayer, token)
    {
        Debug.Log("Création du vrai joueur " + name + " terminée !");
    }

    public override int GetColumnInput()
    {
        return -1;
    }
}
