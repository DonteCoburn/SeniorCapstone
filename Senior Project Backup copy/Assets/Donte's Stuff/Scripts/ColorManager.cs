using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class ColorManager : MonoBehaviourPunCallbacks
{
    private Dictionary<int, Color> playerColorsByActorNumber = new Dictionary<int, Color>();
    private List<Color> colors;

    void Start()
    {
        InitializeColors();
        AssignColorsBasedOnActorOrder();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        AssignColorsBasedOnActorOrder();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        AssignColorsBasedOnActorOrder();
    }

    void InitializeColors()
    {
        colors = new List<Color>
        {
            HexToColor("#FF8000"), // Player 1: Orange
            HexToColor("#0000FF"), // Player 2: Blue
            HexToColor("#008000"), // Player 3: Green
            HexToColor("#FF0000"), // Player 4: Red
            HexToColor("#800080"), // Player 5: Purple
            HexToColor("#FFFF00"), // Player 6: Yellow
            HexToColor("#00FFFF"), // Player 7: Cyan
            HexToColor("#FF00FF"), // Player 8: Magenta
            HexToColor("#00FF00"), // Player 9: Lime
            HexToColor("#FFC0CB")  // Player 10: Pink
        };
    }

    void AssignColorsBasedOnActorOrder()
    {
        List<int> actorNumbers = new List<int>();
        foreach (var player in PhotonNetwork.PlayerList)
        {
            actorNumbers.Add(player.ActorNumber);
        }
        actorNumbers.Sort();

        for (int i = 0; i < actorNumbers.Count; i++)
        {
            playerColorsByActorNumber[actorNumbers[i]] = colors[i % colors.Count];
        }
    }

    public Color GetPlayerColor(int actorNumber)
    {
        if (playerColorsByActorNumber.TryGetValue(actorNumber, out Color color))
        {
            return color;
        }
        return Color.white; // Default color if not found
    }

    private static Color HexToColor(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);
        return color;
    }
}