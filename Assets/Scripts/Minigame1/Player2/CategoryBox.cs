using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryBox : MonoBehaviour
{
    public Classification boxClassification;
    public Material correctMaterial;
    public Material incorrectMaterial;
    public GameObject correctParticles;
    public GameObject incorrectParticles;

    [Header("SFX")]
    public AudioClip correctClip;
    public AudioClip incorrectClip;

    Material baseMaterial;

    M1GameManager gameManager;
    AudioSource audioSource;

    void Start()
    {
        baseMaterial = GetComponent<MeshRenderer>().material;
        gameManager = GameObject.Find("GameManager").GetComponent<M1GameManager>();
        audioSource = GetComponent<AudioSource>();
    }


    public void ReceivePaper(GameObject paper)
    {
        if (paper.GetComponent<Paper>().GetClassification() == boxClassification)
        {
            GetComponent<MeshRenderer>().material = correctMaterial;
            correctParticles.SetActive(true);
            gameManager.AddCorrect();

            audioSource.clip = correctClip;
            audioSource.Play();
        }
        else 
        {
            GetComponent<MeshRenderer>().material = incorrectMaterial;
            incorrectParticles.SetActive(true);
            gameManager.AddIncorrect();

            audioSource.clip = incorrectClip;
            audioSource.Play();
        }

        Invoke(nameof(SwitchBackToDefaultMat), 0.2f);
        Destroy(paper);
    }

    void SwitchBackToDefaultMat()
    {
        GetComponent<MeshRenderer>().material = baseMaterial;
        correctParticles.SetActive(false);
        incorrectParticles.SetActive(false);
    }
}
