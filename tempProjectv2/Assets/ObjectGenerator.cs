using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject pyramid;

    public GameObject sphere;

    public Camera camera;

    public TextMeshProUGUI textMeshPro;

    public int Life = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            Vector3 worldPosition = camera.ScreenToWorldPoint(mousePosition);

            Instantiate(pyramid, worldPosition + new Vector3(5f, worldPosition.y, worldPosition.z), Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Input.mousePosition;

            Vector3 worldPosition = camera.ScreenToWorldPoint(mousePosition);

            Instantiate(sphere, worldPosition + new Vector3(5f, worldPosition.y, worldPosition.z), Quaternion.identity);
        }

        textMeshPro.text = $"Колиечтсво жизней: {Life}";
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Life"))
        {
            Life++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Life--;
            Destroy(other.gameObject);
        }

    }
}
