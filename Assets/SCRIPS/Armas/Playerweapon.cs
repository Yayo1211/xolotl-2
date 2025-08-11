using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public new Camera camera;
    public Transform spawner;
    public GameObject bulletPrefab;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        RotateTowardsMouse();
        CheckFiring(); // esta sirve para ver si un jugador esta disparando o no 
    }

    private void RotateTowardsMouse()
    {
        float angle = GetAngleTowardsMouse();

        transform.rotation = Quaternion.Euler(0, 0, angle);
        spriteRenderer.flipY = angle >= 90 && angle <= 270;
    }

    private float GetAngleTowardsMouse()
    {
        Vector3 mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mouseDirection = mouseWorldPosition - transform.position;
        mouseDirection.z = 0;

        float angle = (Vector3.SignedAngle(Vector3.right, mouseDirection, Vector3.forward) + 360) % 360;

        return angle;
    }

    private void CheckFiring()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(bulletPrefab); // si el jugador presiona click izquierdo crea una nueva bala utilizando bulletprefab
            bullet.transform.position = spawner.position;
            bullet.transform.rotation = transform.rotation; // estos es la misma debido a que tiene que salir desde la ubicacion de la arma
            Destroy(bullet, 2f); // esta es para que se destruya en 2 segundos 
        }
    }
}