using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
  public static InfoBox instance;
    public Text InfoText;

    private void Awake()
    {
        instance = this;
        InfoText.text = "";
    }

    public void ShowInfo(string text)
    {
        InfoText.text = text;
    }
}
