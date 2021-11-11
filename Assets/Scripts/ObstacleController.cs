using UnityEngine;

//障害物を動かすクラス
class ObstacleController : MonoBehaviour
{
    float rotateSpeed = 5;
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);
    }
}
