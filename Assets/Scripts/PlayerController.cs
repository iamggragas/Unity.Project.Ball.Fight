using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 150.0f;
    private Rigidbody playerRb;
    public float verticalInput;
    private GameObject focalPoint;
    private bool powerUp;
    private float powerUpStrength = 15.0f;
    public GameObject powerUpIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * Time.deltaTime * speed);
        powerUpIndicator.transform.position = transform.position + new Vector3(0, 0.5f, 0);
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PowerUp"))
		{
            powerUp = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
		}
	}

    IEnumerator PowerUpCountDownRoutine()
	{
        yield return new WaitForSeconds(7);
        powerUp = false;
        powerUpIndicator.gameObject.SetActive(false);
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy") && powerUp)
		{
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromThePlayer = enemyRb.transform.position - transform.position;
            Debug.Log("Collided with " + collision.gameObject.name + " and now the player powerUp is set to " + powerUp);
            enemyRb.AddForce(awayFromThePlayer * powerUpStrength, ForceMode.Impulse);
		}
	}
}
