using UnityEngine;

public class MenuController : MonoBehaviour
{
    public Animator menuAnimator;
    private bool isMenuOpen = false;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuAnimator.SetBool("isMenuOpen", isMenuOpen);
    }
}
