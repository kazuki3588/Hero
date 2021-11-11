using UnityEngine;

//地面判定を管理するクラス
public class CheckGround : MonoBehaviour
{
    bool groundCheck = false;
    public bool GroundCheck
    {
        get { return groundCheck; }
        set { groundCheck = value; }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            groundCheck = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundCheck = false;
        }
    }
}