public abstract class IPlayer
{
    public enum PlayerType { TruePlayer, Bot };

    public string name;
    public PlayerType playerType;
    public TokenTemplate token;

    public IPlayer(string name, PlayerType type, TokenTemplate token)
    {
        this.name = name;
        this.playerType = type;
        this.token = token;
    }

    public bool IsBot()
    {
        return playerType == PlayerType.Bot;
    }

    public abstract int GetColumnInput();
}
