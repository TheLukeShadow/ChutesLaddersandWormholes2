using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    int activePlayer;
    int diceNumber;
    public Dice TheDice;
    public WinPanel TheWinPanel;

    

    [System.Serializable]
    public class Player
    {
        public string playerName;
        public Token token;
        public GameObject RollDiceButton;

        public enum PlayerType
        {
            CPU,
            Human
        }

        public PlayerType ThePlayerType;
    }

    public List<Player> PlayerList = new List<Player>();

    public enum States
    {
        Waiting,
        Rolling,
        SwitchPlayer
    }
    public States State;

    private void Awake()
    {
        
        Instance = this;

        for (int i = 0; i < PlayerList.Count; i++) 
        {
            if (SaveSettings.Players[i] == "Human") PlayerList[i].ThePlayerType = Player.PlayerType.Human;
            if (SaveSettings.Players[i] == "CPU") PlayerList[i].ThePlayerType = Player.PlayerType.CPU;
        }
    }

    private void Start()
    {
        //DiceButtonToggle(false);
        DeactivateAllButtons();
        TheWinPanel.gameObject.SetActive(false);

        activePlayer = Random.Range(0, PlayerList.Count);
        InfoBox.instance.ShowInfo(PlayerList[activePlayer].playerName + " is taking a turn ");
    }

    private void Update()
    {
        if (PlayerList[activePlayer].ThePlayerType == Player.PlayerType.CPU)
        {
            switch(State)
            {
                case States.Waiting:
                    //Idle
                    break;
                case States.Rolling:
                    StartCoroutine(RollDiceDelay());
                    State = States.Waiting;
                    break;
                case States.SwitchPlayer:
                    activePlayer++;
                    activePlayer %= PlayerList.Count;

                    InfoBox.instance.ShowInfo(PlayerList[activePlayer].playerName + " is taking a turn ");

                    State = States.Rolling;
                    break;
            }
        }
        if (PlayerList[activePlayer].ThePlayerType == Player.PlayerType.Human)
        {
            switch (State)
            {
                case States.Waiting:
                    //Idle
                    break;
                case States.Rolling:
                    ActivatePlayerButton(true);
                    State = States.Waiting;
                    break;
                case States.SwitchPlayer:
                    activePlayer++;
                    activePlayer %= PlayerList.Count;

                    InfoBox.instance.ShowInfo(PlayerList[activePlayer].playerName + " is taking a turn " );

                    State = States.Rolling;
                    break;
            }
        }
    }

    IEnumerator RollDiceDelay()
    {
        yield return new WaitForSeconds(0.2f);
        //diceNumber = Random.Range(1, 7);

        //roll dice
        TheDice.RollDice();




    }

    //called from the dice
    public void RolledNumber(int theDiceNumber)
    {
        diceNumber = theDiceNumber;

        InfoBox.instance.ShowInfo(PlayerList[activePlayer].playerName + " has rolled a " + diceNumber);

        //Take Turn
        PlayerList[activePlayer].token.Turn(diceNumber);
    }

    //private void DiceButtonToggle(bool on)
    //{
    //    RollDiceButton.SetActive(on);
    //}


    public void HumanRoll()
    {
        //DiceButtonToggle(false);
        ActivatePlayerButton(false);
        StartCoroutine(RollDiceDelay());
    }

    void ActivatePlayerButton(bool on)
    {
        PlayerList[activePlayer].RollDiceButton.SetActive(on);
    }

    void DeactivateAllButtons()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            PlayerList[i].RollDiceButton.SetActive(false);
        }
    }

    //execute anything that needs to happen after a player wins
    public void ReportWinner()
    {
        TheWinPanel.gameObject.SetActive(true);
        TheWinPanel.WinMessage(PlayerList[activePlayer].playerName);
        //Debug.Log(PlayerList[activePlayer].playerName + " has won!");
    }
}
