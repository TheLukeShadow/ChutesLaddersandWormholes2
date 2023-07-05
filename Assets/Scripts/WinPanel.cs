using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    public Text message;

    public void WinMessage(string winner)
    {
        message.text = winner + " wins!";
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
