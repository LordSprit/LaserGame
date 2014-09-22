using UnityEngine;
using System.Collections;

public class LaserCreator : MonoBehaviour
{
    public Transform mLaserPrefab;
    private Transform mLaser;
    // Update is called once per frame
    void Update()
    {
        if (GameController.sGameStatus == GameController.GameStatus.LASER)
        {
            GameController.sGameStatus = GameController.GameStatus.CHECKING;
            mLaser = Instantiate(mLaserPrefab, this.transform.position, this.transform.rotation) as Transform;
            mLaser.parent = this.transform;
        }
    }
}
