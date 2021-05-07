using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuIntroduction : MonoBehaviour
{
    private Animator animator;

    private MeshRenderer meshRenderer;

    [SerializeField] private float waitTime;

    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject player;

    [Header("Sound")]
    [SerializeField] AudioClip teleportSound;
    [SerializeField] AudioClip gearSound;
    AudioSource aSource;

    private bool doneAnimation;

    private void Start()
    {
        animator = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
        aSource = GetComponent<AudioSource>();

        doneAnimation = false;
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Landing") && !doneAnimation)
        {
            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                meshRenderer.enabled = false;
                GameObject particlesS = Instantiate(particles, transform);
                particlesS.GetComponentsInChildren<ParticleSystem>().Initialize();
                aSource.clip = teleportSound;
                aSource.Play();
                player.SetActive(true);

                doneAnimation = true;
            }
        }
    }
}
