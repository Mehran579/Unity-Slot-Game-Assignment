using UnityEngine;

public class SlotReel : MonoBehaviour
{
    [Tooltip("Rotation speed of the reel")]
    public float speed = 5f;
    private float symbolHeight = 1.6f;

    [Tooltip("The symbol index to stop on")]
    //public int targetSymbol { get; private set; }
    public int targetSymbol;

    [Tooltip("The time for which the reel spins before checking the symbol to stop at")]
    public float spinTime = 3f;                    //The time at which the reel should stop spinning 
    float stopTime;                                //works as a manual timer to check when the reel should stop spinning

    private bool spinning = false;              //Checks whether the reel have to spin or not

    public event System.Action OnStopped;         // tells the master slot machine that the spinning has stopped and the button can be pressed again

    private float totalDistanceMoved = 0f;
    private float[] baseYPositions; 

    void Awake()
    {
        baseYPositions = new float[transform.childCount];
        int i = 0;
        foreach (Transform symbol in transform)
            baseYPositions[i++] = symbol.localPosition.y;
    }
    void Update()
    {
        if (!spinning) return;

        totalDistanceMoved = Mathf.Repeat(totalDistanceMoved + speed * Time.deltaTime, symbolHeight * 4);

        int i = 0;
        foreach (Transform symbol in transform)
        {
            float wrappedY = Mathf.Repeat(baseYPositions[i] - totalDistanceMoved + symbolHeight * 2, symbolHeight * 4) - symbolHeight * 2;
            Vector3 pos = symbol.localPosition;
            pos.y = wrappedY;
            symbol.localPosition = pos;
            i++;
        }

        if (Time.time >= stopTime)
            checkForTarget();
    }

    private void checkForTarget()
    {
        Transform Target = transform.GetChild(targetSymbol);

        if (Mathf.Abs(Target.localPosition.y) < speed * Time.deltaTime)
        {
            float correction = -Target.localPosition.y; // however far off the target actually was

            foreach (Transform symbol in transform)
            {
                Vector3 pos = symbol.localPosition;
                pos.y += correction;
                symbol.localPosition = pos;
            }

            spinning = false;
            OnStopped?.Invoke();
        }
    }
    public void Spin()                          //starts the spinning of the reel and it wired with the button
    {
        if(spinning)
            return;
        spinning = true;
        stopTime = Time.time + spinTime;
        targetSymbol = SlotRandomNumberGenerator.range(0, transform.childCount);          //randomly selects a symbol to stop at, the number of symbools can be increased by adding more children to the parent object 
    }
}
