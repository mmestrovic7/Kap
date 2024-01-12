using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KapScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float kapJump;
    public float kapSpeed;
    public float snowSpeedDivider;
    public float acceleration;

    private SpriteRenderer spriteRenderer;
    public GameData gameData;
    private bool isSnowflakeSlowed = false;
    public bool stopTheCount = false;


    private const string playerTag = "Player";
    private const string sunTag = "Sun";
    private const string snowTag = "Snow";
    private const string toxTag = "Toxic";
    private const string thunTag = "Thunder";
    private const string levTag = "LevelComplete";
    private const string bordTag = "Border";
    private void Start()
    {
        myRigidbody.freezeRotation = true;
        gameObject.tag = playerTag;
        gameObject.name = "Kap";

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on the object.");
        }
    }

    private void Update()
    {
        MovePlayer();
        Jump();
    }

    private void MovePlayer()
    {
        transform.position += Vector3.right * kapSpeed * Time.deltaTime;
        kapSpeed = kapSpeed / acceleration;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * kapJump;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colTag = collision.gameObject.tag;

        Debug.Log("Collision tag: " + colTag);

        switch (colTag)
        {
            case sunTag:
                ChangePlayerSprite("rainbow");
                stopTheCount = true;
                Destroy(collision.gameObject);
                break;

            case snowTag:
                ChangePlayerSprite("snowflake");
                SlowDownSnowflakeForDuration(10f);
                Destroy(collision.gameObject);
                break;

            case toxTag:
            case thunTag:
                HandleToxicOrThunderCollision(colTag);
                break;

            case levTag:
                UpdateGameData(colTag);
                LoadGameOverScene();
                break;

            case bordTag:
                UpdateGameData(colTag);
                LoadGameOverScene();
                break;
            default:
                Debug.LogError("Error with collision");
                break;
        }
    }
    private void ChangePlayerSprite(string sprite)
    {
        if (!string.Equals(sprite, gameObject.name))
        {
            string path = "Models/" + sprite;
            Sprite newSprite = Resources.Load<Sprite>(path);

            if (newSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = newSprite;
                gameObject.name = sprite;
                Debug.Log("Sprite changed successfully to: " + sprite);
            }
            else
            {
                LogSpriteLoadError(path);
            }
        }
    }
    private void SlowDownSnowflakeForDuration(float duration)
    {
        if (!isSnowflakeSlowed)
        {
            isSnowflakeSlowed = true;
            StartCoroutine(SlowDownSnowflakeCoroutine(duration));
        }
    }

    private IEnumerator SlowDownSnowflakeCoroutine(float duration)
    {
        stopTheCount = false;
        kapSpeed /= snowSpeedDivider; // Slow down the snowflake speed

        float elapsedTime = 0f;

        while (elapsedTime < duration && !stopTheCount)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        // Reset the snowflake speed to its original value
        if(!stopTheCount)
            ChangePlayerSprite("kap");
        kapSpeed +=snowSpeedDivider;
        isSnowflakeSlowed = false;
        
    }
    private void HandleToxicOrThunderCollision(string colTag)
    {

        if (string.Equals(gameObject.name, "rainbow"))
            ChangePlayerSprite("kap");
        else
        {
            UpdateGameData(colTag);
            LoadGameOverScene();
        }
        
    }

    private void UpdateGameData(string colTag)
    {
        gameData.touchedByToxic = false;
        gameData.touchedByThunder = false;
        gameData.touchedByLevelEnd = false;
        gameData.touchedByBorder = false;
        if (string.Equals(colTag, toxTag))
        {
            gameData.touchedByToxic = true;
        }
        else if (string.Equals(colTag, thunTag))
        {
            gameData.touchedByThunder = true;
        }
        else if (string.Equals(colTag, levTag))
        {
            gameData.touchedByLevelEnd = true;
        }
        else if(string.Equals(colTag, bordTag))
        {
            gameData.touchedByBorder = true;
        }
        else
            Debug.LogError("Error with collision");



    }


    private void LogSpriteLoadError(string path)
    {
        Debug.LogError("Failed to load the new sprite or SpriteRenderer is not initialized.");
        Debug.LogError("Path: " + path);
        Debug.LogError("SpriteRenderer: " + spriteRenderer);
    }


    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
