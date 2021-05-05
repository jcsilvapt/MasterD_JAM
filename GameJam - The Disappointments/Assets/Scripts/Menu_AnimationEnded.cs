using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_AnimationEnded : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;

    private void AnimationEnded()
    {
        menuManager.AnimationEnded();
    }
}
