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

    Material baseMaterial;

    GameManager gameManager;

    void Start()
    {
        baseMaterial = GetComponent<MeshRenderer>().material;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void ReceivePaper(GameObject paper)
    {
        if (paper.GetComponent<Paper>().GetClassification() == boxClassification)
        {
            GetComponent<MeshRenderer>().material = correctMaterial;
            correctParticles.SetActive(true);
            gameManager.AddCorrect();
        }
        else 
        {
            GetComponent<MeshRenderer>().material = incorrectMaterial;
            incorrectParticles.SetActive(true);
            gameManager.AddIncorrect();
        }

        Invoke(nameof(SwitchBackToDefaultMat), 0.5f);
        Destroy(paper);
    }

    void SwitchBackToDefaultMat()
    {
        GetComponent<MeshRenderer>().material = baseMaterial;
        correctParticles.SetActive(false);
        incorrectParticles.SetActive(false);
    }
}
