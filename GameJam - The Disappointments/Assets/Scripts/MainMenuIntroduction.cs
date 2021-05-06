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

    private bool doneAnimation;

    private void Start()
    {
        animator = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();

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
                player.SetActive(true);

                doneAnimation = true;
            }
        }
    }
}
