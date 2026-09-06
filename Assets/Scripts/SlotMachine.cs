using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour           //Serves as the UI manager As well as the centrailized controller for the Slot sybmols and result caluclation
{
    [Header("UI Text")]                         //all the ui text
    public TMP_Text totalPoints;
    public TMP_Text betText;
    public TMP_Text winText;
    public TMP_Text wonAmountText;

    [Header("UI BUttons")]                         //all the ui buttons
    public Button spinButton;
    public Button[] allUIbuttons;

    //Integers holding the real values;
    [Header("real variables wholding ther party ka")]
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
        totalPoints.text = totalPointsValue.ToString();
        betText.text = betPointsValue.ToString();                                         //maps the texts to their respective values
        wonAmountText.text = "Won $: \n" + wonAmountValue.ToString();
        reel3.OnStopped += reEnableSpin;                                    //assigning the event to the last reel so that when it stops spinning the button can be re - enabled
        reel3.OnStopped += checkWin;
    }


    public void SpinReels()
    {
        if (!CanSpin())                                                //REMOVES THE ABILITY TO SPIN AGAIN WHEN THE PLAYER HAS NO POINTS LEFT
            return;

        SoundManager.instance.PlayMusic(SoundManager.instance.spinSound, true);

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
        SoundManager.instance.PlayMusic(SoundManager.instance.spinSound, false);               //stops the spinning sound when the last reel stops spinning
        foreach (Button button in allUIbuttons)
        {
            button.interactable = true;
        }
        spinButton.interactable = CanSpin();
    }
    void checkWin()                //checks if the player wins or not
    {
        if (reel1.targetSymbol == reel2.targetSymbol && reel2.targetSymbol == reel3.targetSymbol)              
        {
            wonAmountValue = betPointsValue * jackpotMultiplier[reel1.targetSymbol];
            totalPointsValue += wonAmountValue;
            totalPoints.text = totalPointsValue.ToString();
            timesWon++;
            winText.text = "WINS: \n" + timesWon.ToString();
            if (reel1.targetSymbol == 3)
                SoundManager.instance.PlaySFX(SoundManager.instance.jackpotSoundfx);
            else
                SoundManager.instance.PlaySFX(SoundManager.instance.winSoundfx);
        }
        else
        {
            wonAmountValue = 0;
        }
        wonAmountText.text = "Won $: \n" + wonAmountValue.ToString();
    }
    public void IncreaseBet()
    {
        if (betPointsValue + 10 <= totalPointsValue)
        {
            betPointsValue += 10;
            betText.text = betPointsValue.ToString();
            spinButton.interactable = CanSpin();
        }
    }
    public void DecreaseBet()               
    {
        if (betPointsValue > 0)
        {
            betPointsValue -= 10;
            betText.text = betPointsValue.ToString();
            spinButton.interactable = CanSpin();
        }
    }
    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);               //restarts the game with the button
    }
    bool CanSpin()               //checks if the palyer have enoguht points to PLAY
    {
        return totalPointsValue >= betPointsValue;
    }
}
