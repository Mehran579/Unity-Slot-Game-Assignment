using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour           //Serves as the UI manager As well as the centrailized controller for the Slot sybmols and result caluclation
{
    [Header("UI Text")]
    public TMP_Text totalPoints;
    public TMP_Text betText;
    public TMP_Text winText;

    [Header("UI BUttons")]
    public Button spinButton;
    public Button[] allUIbuttons;



    public int totalPointsValue = 1000;
    public int betPointsValue;
    public int timesWon;

    [Header("Reels")]
    public SlotReel reel1;
    public SlotReel reel2;
    public SlotReel reel3;

    [Header("Jack Pot Multiplier")]
    public int[] jackpotMultiplier;              //mapped in sequence of the symbols;

    private void Awake()
    {
        Debug.Log("jackpotMultiplier.Length = " + jackpotMultiplier.Length);
        totalPoints.text = totalPointsValue.ToString();
        betText.text = betPointsValue.ToString();
        reel3.OnStopped += reEnableSpin;
        reel3.OnStopped += checkWin;
        //spinButton.interactable = false;
        winText.text = "Win: \n" + timesWon.ToString();
    }


    public void SpinReels()
    {
        foreach (Button button in allUIbuttons)
        {
            button.interactable = false;
        }
        reel1.Spin();
        reel2.Spin();
        reel3.Spin();
        //spinButton.interactable = false;
        totalPointsValue -= betPointsValue;
        totalPoints.text = totalPointsValue.ToString();
        //if (reel1.targetSymbol == reel2.targetSymbol && reel2.targetSymbol == reel3.targetSymbol)
        //{
        //    totalPointsValue += betPointsValue * jackpotMultiplier[reel1.targetSymbol];
        //    timesWon++;
        //    winText.text = "Win: \n" + timesWon.ToString();
        //    //if (reel1.targetSymbol != 7)
        //    //    winText.text = "You won " + (betPointsValue * jackpotMultiplier[reel1.targetSymbol]).ToString() + " points!";
        //    //else
        //    //    winText.text = "You won the Jackpot!";
        //}
    }
    void reEnableSpin()
    {
        spinButton.interactable = true;
        foreach (Button button in allUIbuttons)
        {
            button.interactable = true;
        }
    }
    void checkWin()
    {
        Debug.Log("checkWin fired");
        if (reel1.targetSymbol == reel2.targetSymbol && reel2.targetSymbol == reel3.targetSymbol)
        {
            totalPointsValue += betPointsValue * jackpotMultiplier[reel1.targetSymbol];
            totalPoints.text = totalPointsValue.ToString();
            timesWon++;
            winText.text = "Win: \n" + timesWon.ToString();
        }
    }
    public void IncreaseBet()
    {
        if(totalPointsValue > betPointsValue)
        {
            //totalPointsValue -= 10;
            betPointsValue += 10;
            betText.text = betPointsValue.ToString();
            //totalPoints.text = totalPointsValue.ToString();
        }
    }
    public void DecreaseBet()
    {
        if(betPointsValue > 0)
        {
            //totalPointsValue += 10;
            betPointsValue -= 10;
            betText.text = betPointsValue.ToString();
            //totalPoints.text = totalPointsValue.ToString();
        }
    }
}
