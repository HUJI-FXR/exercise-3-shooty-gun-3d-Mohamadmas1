using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement & Look")]
    [SerializeField] private Movement movement;
    [SerializeField] private float sensitivity;

    [Header("Health")]
    [SerializeField] private float health;
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text healthText;
    private float initialHealth;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initialHealth = health;
        healthBar.fillAmount = 1;
        healthText.text = "100%";
    }

    void Update()
    {
        // Toggle cursor lock
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        movement.Rotate(mouseY, mouseX);

        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");
        bool inputJump = Input.GetKey(KeyCode.Space);

        movement.Move(inputHorizontal, inputVertical);
        if (inputJump) movement.Jump();
    }

    void FixedUpdate()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / initialHealth;
        healthText.text = Mathf.RoundToInt(health / initialHealth * 100) + "%";
        if (health <= 0)
        {
            GameManager.instance.KillPlayer();
        }
    }
}
