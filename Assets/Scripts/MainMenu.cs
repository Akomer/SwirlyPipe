
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Player player;

    public Text scoreLabel;

    private void Awake()
    {
        if (Application.isMobilePlatform)
        {
            Application.targetFrameRate = 120;
        }
    }

    public void StartGame(int mode)
    {
        player.StartGame(mode);
        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    internal void EndGame(float distanceTraveled)
    {
        scoreLabel.text = ((int)(distanceTraveled * 10f)).ToString();
        Cursor.visible = true;
        gameObject.SetActive(true);
    }
}
