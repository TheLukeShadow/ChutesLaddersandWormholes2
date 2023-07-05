using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public Toggle RedCPU, RedHuman;
    public Toggle BlueCPU, BlueHuman;
    public Toggle YellowCPU, YellowHuman;
    public Toggle GreenCPU, GreenHuman;

    private void ReadToggles()
    {
        
        if (RedCPU.isOn) SaveSettings.Players[0] = "CPU";
        else if (RedHuman.isOn) SaveSettings.Players[0] = "Human";

        if (BlueCPU.isOn) SaveSettings.Players[1] = "CPU";
        else if (BlueHuman.isOn) SaveSettings.Players[1] = "Human";

        if (YellowCPU.isOn) SaveSettings.Players[2] = "CPU";
        else if (YellowHuman.isOn) SaveSettings.Players[2] = "Human";

        if (GreenCPU.isOn) SaveSettings.Players[3] = "CPU";
        else if (GreenHuman.isOn) SaveSettings.Players[3] = "Human";
    }

    public void StartGame()
    {
        ReadToggles();
        SceneManager.LoadScene("GameScene");
    }
}


public static class SaveSettings
{
    public static string[] Players = new string[4]; /*red 0, blue 1, yellow 2, green 3*/
}
