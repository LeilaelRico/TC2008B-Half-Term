using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnF : MonoBehaviour
{
    public float number_of_columns;
    public float speed;
    public Sprite texture;
    public Color color;
    public float lifetime;
    public float firerate;
    public float size;
    private float angle;
    public Material material;

    public float spin_speed;
    public float time;

    public ParticleSystem system;
    

    void Start()
    {
        system = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Balas contadas: " + system.particleCount);
        Debug.Log("Balas renderizadas: " + GetAliveParticles());
    }

    int GetAliveParticles()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[system.particleCount];
        return system.GetParticles(particles);
    }


    private void Awake()
    {
        Summon();
    }

    private void FixedUpdate()
    {

        time += Time.fixedDeltaTime;

        transform.rotation = Quaternion.Euler(0, time * spin_speed, 0);

    }

    void Summon()
    {
        angle = Mathf.Abs(Mathf.Sin(number_of_columns * Mathf.PI / 360f)) * .5f;

        for (int i = 0; i < number_of_columns; i++)
        {
            // A simple particle material with no texture.
            Material particleMaterial = material;

            // Create a green Particle System.
            var go = new GameObject("Particle System");
            go.transform.Rotate(0, angle * i, 90); // Rotate so the system emits upwards.
            go.transform.parent = this.transform;
            go.transform.position = this.transform.position;
            system = go.AddComponent<ParticleSystem>();
            go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            var mainModule = system.main;

            mainModule.startSpeed = speed;
            mainModule.maxParticles = 10000;
            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;

            var emission = system.emission;
            emission.enabled = false;

            var shape = system.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Sprite;
            shape.sprite = null;

            var text = system.textureSheetAnimation;
            text.mode = ParticleSystemAnimationMode.Sprites;
            text.AddSprite(texture);
            text.enabled = true;

        }

        // Every 2 secs we will emit.
        InvokeRepeating("DoEmit", 0f, firerate);
    }

    void DoEmit()
    {

        foreach (Transform child in transform)

        {
            system = child.GetComponent<ParticleSystem>();
            // Any parameters we assign in emitParams will override the current system's when we call Emit.
            // Here we will override the start color and size.
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startColor = color;
            emitParams.startSize = size;
            emitParams.startLifetime = lifetime;
            system.Emit(emitParams, 10);

        }
    }
}