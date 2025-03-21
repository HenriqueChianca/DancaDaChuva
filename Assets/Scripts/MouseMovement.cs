using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private bool isActive = false;
    private float inactivityTimer = 0f;
    public float inactivityThreshold = 3f; // Tempo antes de desativar part�culas

    private Vector2 lastMousePosition;
    private int movementCount = 0;
    public int movementsNeeded = 10; // N�mero de movimentos do mouse para ativar part�culas
    public int movementThreshold = 1000; // Pixels alterados para ativar part�culas

    // Start is called before the first frame update
    void Start()
    {

        if (particleSystem != null)
        {
            particleSystem.Stop();
        }

        lastMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        DetectMouseMovement();
       

        if (isActive)
        {
            inactivityTimer += Time.deltaTime;
            if (inactivityTimer >= inactivityThreshold)
            {
                DeactivateParticles();
            }
        }
    }

    void DetectMouseMovement()
    {
        Vector2 currentMousePosition = Input.mousePosition;
        if (currentMousePosition != lastMousePosition)
        {
            movementCount++;
            lastMousePosition = currentMousePosition;

            if (movementCount >= movementsNeeded)
            {
                ActivateParticles();
            }
        }
    }

    void ActivateParticles()
    {
        if (!isActive)
        {
            isActive = true;
            inactivityTimer = 0f;

            if (particleSystem != null)
            {
                var emission = particleSystem.emission;
                emission.enabled = true;
                particleSystem.Play();
            }
        }
    }

    void DeactivateParticles()
    {
        if (isActive)
        {
            isActive = false;

            if (particleSystem != null)
            {
                var emission = particleSystem.emission;
                emission.enabled = false;
                particleSystem.Stop();
            }
        }
    }

}
