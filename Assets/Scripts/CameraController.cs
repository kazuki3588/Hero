using UnityEngine;

//カメラを管理するクラス
class CameraController : MonoBehaviour
{
    GameObject player;//プレイヤーのインスタンス
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;
        Vector3 cameraPosi = new Vector3(player.transform.position.x, player.transform.position.y,-1);
        if (cameraPosi.x < -3)
        {
            cameraPosi.x = -3;
        }
        transform.position = cameraPosi;
        
    }
}
