using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    [SerializeField] private Animator HeartAnimator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Light")
        {
            HeartAnimator.SetBool("isDamage", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Light")
        {
            HeartAnimator.SetBool("isDamage", false);
        }
    }
}
