using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridView : MonoBehaviour
{
    public GameObject prefab_Token;

    public Transform transform_origin;
    private Vector3 position_origin;
    private Transform parent;

    public Vector2 offset;
    public Vector2 token_size;

    public GameObject endMenu;
    public TMP_Text title_endMenu;
    public GameObject PP_blurry;

    public GameLoopManager gameLoopManager;

    public TMP_Text txt_player1;
    public TMP_Text txt_player2;

    public Image sprite_player1;
    public Image sprite_player2;


    private void Awake()
    {
        position_origin = transform_origin.position;
        parent = transform;
    }

    public void UpdateTokens(GridSystem grid)
    {
        DestroyAllGraphicalToken();

        int dimx = grid.GetDim().x;
        int dimy = grid.GetDim().y;

        for (int x = 0; x < dimx; x++)
        {
            for (int y = 0; y < dimy; y++)
            {
                TokenTemplate tokenData = grid.GetTokenWithCoords(x, y);
                
                if (tokenData == null)
                {
                    continue;
                }

                Vector3 position = new Vector3(position_origin.x + x * offset.x + token_size.x, position_origin.y + y * offset.y + token_size.y, 0);
                GameObject newToken = Instantiate(prefab_Token, position, Quaternion.identity, parent);

                newToken.transform.name = "[" + x + ", " + y + "] " + tokenData.name;
                newToken.GetComponent<SpriteRenderer>().sprite = tokenData.sprite;
            }
        }
    }

    public void AddNewToken(int x, int y, TokenTemplate token)
    {
        if (token == null)
        {
            print("token null ?!");
            return;
        }

        Vector3 position = new Vector3(position_origin.x + x * offset.x + token_size.x, position_origin.y + y * offset.y + token_size.y, 0);
        GameObject newToken = Instantiate(prefab_Token, position, Quaternion.identity, parent);

        newToken.transform.name = "[" + x + ", " + y + "] " + token.name;
        newToken.GetComponent<SpriteRenderer>().sprite = token.sprite;
    }

    public void GhostInput(int x, GameObject gameObject)
    {
        gameObject.transform.position = new Vector3(position_origin.x + x * offset.x + token_size.x, gameObject.transform.position.y, 0);
    }

    public void UpdateNames(IPlayer player1, IPlayer player2)
    {
        txt_player1.SetText(player1.name);
        txt_player2.SetText(player2.name);
    }

    public void UpdatePlayerTokenOrder(int currentPlayerIndex)
    {
        if (currentPlayerIndex == 0)
        {
            txt_player1.color = new Color32(255, 255, 255, 200);
            txt_player2.color = new Color32(255, 255, 255, 50);

            sprite_player1.color = new Color32(255, 255, 255, 200);
            sprite_player2.color = new Color32(255, 255, 255, 50);
        }
        else if (currentPlayerIndex == 1)
        {
            txt_player1.color = new Color32(255, 255, 255, 50);
            txt_player2.color = new Color32(255, 255, 255, 200);

            sprite_player1.color = new Color32(255, 255, 255, 50);
            sprite_player2.color = new Color32(255, 255, 255, 200);
        }
    }

    private void DestroyAllGraphicalToken()
    {
        for (int i = transform.childCount-1; i > 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void SetFinish(string message)
    {
        endMenu.SetActive(true);
        PP_blurry.SetActive(true);
        title_endMenu.SetText(message);
    }

    public void NewGame()
    {
        endMenu.SetActive(false);
        PP_blurry.SetActive(false);
        gameLoopManager.NewGame();
        DestroyAllGraphicalToken();
    }

    public void Quit()
    {
        Application.Quit();
    }
}