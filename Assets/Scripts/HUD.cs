using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Text distanceLabel, velocityLabel;

    public int Distance
    {
        get { return int.Parse(distanceLabel.text); }
        set { distanceLabel.text = value.ToString(); }
    }

    public int Velocity
    {
        get { return int.Parse(velocityLabel.text); }
        set { velocityLabel.text = value.ToString(); }
    }
}
