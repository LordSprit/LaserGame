using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameStatus sGameStatus;
    public enum GameStatus
    {
        PAUSE,
        MOVING,
        LASER,
        CHECKING,
        LEVEL_COMPLETED
    }

    // Use this for initialization
    void Start()
    {
        sGameStatus = GameStatus.MOVING;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Game Status = " + sGameStatus);
        switch (sGameStatus)
        {
            case GameStatus.PAUSE:
                break;
            case GameStatus.MOVING:
                break;
            case GameStatus.LASER:
                break;
            case GameStatus.CHECKING:
                break;
            case GameStatus.LEVEL_COMPLETED:
                break;
        }
    }
}
