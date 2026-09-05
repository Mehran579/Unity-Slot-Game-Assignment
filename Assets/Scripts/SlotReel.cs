using UnityEngine;

public class SlotReel : MonoBehaviour
{
    [Tooltip("Rotation speed of the reel")]
    public float speed = 5f;
    private float symbolHeight = 1.6f;

    [Tooltip("The symbol index to stop on")]
    //[SerializeField] private int targetSymbol;
    public int targetSymbol { get; private set; }


    [Tooltip("The time for which the reel spins before checking the symbol to stop at")]
    public float spinTime = 3f;
    float stopTime;

    private bool spinning = false;              //Checks whether the reel have to spin or not

    public event System.Action OnStopped;

    void Update()
    {
        if (!spinning)
            return;

        foreach (Transform symbol in transform)
        {
            symbol.localPosition += Vector3.down * speed * Time.deltaTime;

            if (symbol.localPosition.y < -symbolHeight * 2)                                   //moves the symbol back to the top of the reel when it goes below the reel
            {
                symbol.localPosition += Vector3.up * symbolHeight * 4;
            }
        }

        if(Time.time >= stopTime)
            checkForTarget();
    }

    private void checkForTarget()
    {
        Transform Target = transform.GetChild(targetSymbol);

        // Check if the target is at the center
        if (Mathf.Abs(Target.localPosition.y) < speed * Time.deltaTime)
        {
            // Align it perfectly
            Vector3 position = Target.localPosition;
            position.y = 0f;
            Target.localPosition = position;

            spinning = false;                                                //stops the spinning if the target is reached
            OnStopped?.Invoke();                                             //tells the slot machine that the spinning has stopped and the button can be pressed again
        }
    }
    public void Spin()                          //starts the spinning of the reel and it wired with the button
    {
        if(spinning)
            return;
        spinning = true;
        stopTime = Time.time + spinTime;
        targetSymbol = SlotRandomNumberGenerator.range(0, transform.childCount);          //randomly selects a symbol to stop at, the number of symbools can be increased by adding more children to the parent object 
        Debug.Log($"childCount={transform.childCount}, targetSymbol={targetSymbol}");
    }
}
