using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameData gameData;
    public Text displayText;

    void Start()
    {
        // Check if gameData is properly assigned
        if (gameData == null)
        {
            Debug.LogWarning("No GameData assigned to GameOverScript. Make sure to assign it in the Unity Editor.");
            return;
        }
        displayText.text = "Default";
        Debug.LogWarning(gameData.touchedByToxic.ToString()+gameData.touchedByThunder.ToString() + gameData.touchedByLevelEnd.ToString()+gameData.touchedByBorder.ToString());
        // Access the GameData from the previous scene
        if (gameData.touchedByToxic)
        {
            displayText.text = "you've now become an evil polluted raindrop muahahaha";
        }
        else if (gameData.touchedByThunder)
        {
            displayText.text = "you got hit by lightning. rain water conducts electricty. you died.";
        }
        else if(gameData.touchedByLevelEnd)
        {
            displayText.text = "good job you've completed the demo! more levels are coming soon";
        }
        else
        {
            displayText.text = "stay in the sky!";
        }
    }
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

}


