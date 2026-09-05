using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public SlotReel reel1;
    public SlotReel reel2;
    public SlotReel reel3;

    public Button spinButton;

    private void Awake()
    {
        reel3.OnStopped += reEnableSpin;
    }
    public void SpinReels()
    {
        reel1.Spin();
        reel2.Spin();
        reel3.Spin();
        spinButton.interactable = false;
    }
    void reEnableSpin()
    {
        spinButton.interactable = true;
    }
}
