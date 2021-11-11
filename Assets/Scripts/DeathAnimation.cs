using UnityEngine;

//死亡アニメーションを管理するクラス
class DeathAnimation : MonoBehaviour
{
    public void OnCompleteAnimation()
    {
        Destroy(gameObject);
    }
}
