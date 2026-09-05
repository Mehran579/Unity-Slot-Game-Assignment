using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour           //Serves as the UI manager As well as the centrailized controller for the Slot sybmols and result caluclation
{
    [Header("UI Text")]
    public TMP_Text totalPoints;
    public TMP_Text betText;
    public TMP_Text winText;
    public TMP_Text wonAmountText;

    [Header("UI BUttons")]
    public Button spinButton;
    public Button[] allUIbuttons;

    public int totalPointsValue = 1000;
    public int betPointsValue;
    public int timesWon;
    public int wonAmountValue;

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
        wonAmountText.text = "Won $: \n" + wonAmountValue.ToString();
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
        totalPointsValue -= betPointsValue;
        totalPoints.text = totalPointsValue.ToString();
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
        if (reel1.targetSymbol == reel2.targetSymbol && reel2.targetSymbol == reel3.targetSymbol)
        {
            wonAmountValue = betPointsValue * jackpotMultiplier[reel1.targetSymbol];
            totalPointsValue += wonAmountValue;
            totalPoints.text = totalPointsValue.ToString();
            timesWon++;
            winText.text = "WINS: \n" + timesWon.ToString();
        }
        else
        {
            wonAmountValue = 0;
        }
        wonAmountText.text = "Won $: \n" + wonAmountValue.ToString();
    }
    public void IncreaseBet()
    {
        if(totalPointsValue > betPointsValue)
        {
            betPointsValue += 10;
            betText.text = betPointsValue.ToString();
        }
    }
    public void DecreaseBet()
    {
        if(betPointsValue > 0)
        {
            betPointsValue -= 10;
            betText.text = betPointsValue.ToString();
        }
    }
}
