using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public GUIText countText;
    public GUIText winText;
    public float speed;
    private int count;
    public int pickupsToWin;


    void Start()
    {
        count = 0;
        updateCountText();
        winText.text = "";
        Material mat = gameObject.GetComponent<Renderer>().material;
        Color c = mat.color;
        mat.color = new Color(c.r, c.b, c.b, 0.5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }

	// This is called at regular intervals
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count++;
            updateCountText();

            if (count == pickupsToWin)
            {
                winText.text = "YOU WIN!";
                Time.timeScale = 0;
                StartCoroutine(waitFiveSecondsAndRestart());
            }
        }

        if (other.gameObject.tag == "Hole")
        {
            winText.text = "YOU LOSE";
            Time.timeScale = 0;
            StartCoroutine(waitFiveSecondsAndRestart());
        }
    }

    void updateCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    IEnumerator waitFiveSecondsAndRestart()
    {
        yield return new WaitForSecondsRealtime(4);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
