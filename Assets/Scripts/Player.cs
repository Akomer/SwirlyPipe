using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PipeSystem pipeSystem;
    public MainMenu mainMenu;
    public HUD hud;

    public float rotationVelocity;
    public float startVelocity;
    public float[] accelerations;

    private float velocity;
    private float acceleration;
    private Pipe currentPipe;
    private float distanceTraveled;
    private float deltaToRotation;
    private float systemRotation;
    private Transform world, rotater;
    private float worldRotation, avatarRotation;

    private void Awake()
    {
        world = pipeSystem.transform.parent;
        rotater = transform.GetChild(0);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        var delta = velocity * Time.deltaTime;
        velocity += acceleration * Time.deltaTime;
        distanceTraveled += delta;
        systemRotation += delta * deltaToRotation;
        SetHudValues();

        if (systemRotation >= currentPipe.CurveAngle)
        {
            delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
            currentPipe = pipeSystem.SetupNextPipe();
            SetupCurrentPipe();
            systemRotation = delta * deltaToRotation;
        }

        pipeSystem.transform.localRotation = Quaternion.Euler(0f, 0f, systemRotation);
        UpdateAvatarRotationOnInput();
    }

    public void Die()
    {
        mainMenu.EndGame(distanceTraveled);
        gameObject.SetActive(false);
    }

    public void StartGame(int accelerationMode)
    {
        velocity = startVelocity;
        acceleration = accelerations[accelerationMode];
        distanceTraveled = 0f;
        avatarRotation = 0f;
        systemRotation = 0f;
        worldRotation = 0f;
        currentPipe = pipeSystem.SetupFirstPipe();
        SetupCurrentPipe();
        UpdateAvatarRotation();
        SetHudValues();
        gameObject.SetActive(true);
    }

    private void SetHudValues()
    {
        hud.Distance = (int)(distanceTraveled * 10f);
        hud.Velocity = (int)(velocity * 10f);
    }

    private void UpdateAvatarRotationOnInput()
    {
        var rotationInput = 0f;
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {
                rotationInput = HandleTouch();
            }
        }
        else
        {
            if (Input.anyKey)
            {
                rotationInput = Input.GetAxis("Horizontal");
            }
        }

        if (rotationInput != 0f)
        {
            UpdateAvatarRotation(rotationInput);
        }
    }

    private float HandleTouch()
    {
        if (Input.GetTouch(0).position.x < Screen.width * 0.5f)
        {
            return -1f;
        }
        else
        {
            return 1f;
        }
    }

    private void UpdateAvatarRotation(float rotationInput = 0f)
    {
        avatarRotation += rotationVelocity * Time.deltaTime * rotationInput;
        avatarRotation = PutAngleBetween0And360(avatarRotation);
        rotater.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);
    }

    private void SetupCurrentPipe()
    {
        deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
        worldRotation += currentPipe.RelativeRotation;
        worldRotation = PutAngleBetween0And360(worldRotation);
        world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
    }

    private static float PutAngleBetween0And360(float angle)
    {
        if (angle < 0f)
        {
            angle += 360;
        }
        else if (angle >= 360)
        {
            angle -= 360;
        }
        return angle;
    }
}
