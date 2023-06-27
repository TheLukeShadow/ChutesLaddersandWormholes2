using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    int activePlayer;
    int diceNumber;

    

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
        if (Instance == null)
        Instance = this;
    }

    private void Start()
    {
        //DiceButtonToggle(false);
        DeactivateAllButtons();
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

                    State = States.Rolling;
                    break;
            }
        }
    }

    IEnumerator RollDiceDelay()
    {
        yield return new WaitForSeconds(1);
        diceNumber = Random.Range(1, 7);

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
}
