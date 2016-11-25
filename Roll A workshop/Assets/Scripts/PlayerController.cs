using UnityEngine;
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
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(0);
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
                winText.text = "YOU WIN!";
        }

        if (other.gameObject.tag == "Hole")
        {
            winText.text = "YOU LOSE";
            Time.timeScale = 0;
            StartCoroutine(waitFiveSeconds());
        }
    }

    void updateCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    IEnumerator waitFiveSeconds()
    {
        yield return new WaitForSecondsRealtime(4);
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }
}
