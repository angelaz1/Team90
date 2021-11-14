using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapersController : MonoBehaviour
{
    public GameObject paperPrefab;
    public CategoryBox animalBox;
    public CategoryBox humanBox;

    public M1PikachuController pikachuController;

    public GameObject currentPaperPosition;
    public GameObject otherPaperPosition;

    List<GameObject> papers = new List<GameObject>(); // Current paper is the one in index 0
    string subjectsFilePath = "Subjects";
    Subject[] subjects;
    bool onCooldown = false;
    bool gameFinished = false;

    void Start()
    {
        TextAsset subjectsTextAsset = Resources.Load<TextAsset>(subjectsFilePath);
        subjects = JsonUtility.FromJson<Subjects>(subjectsTextAsset.text).subjects;

        //StartCoroutine(SpawnPapers()); // DEBUG to spawn papers in for testing
    }

    //IEnumerator SpawnPapers()
    //{
    //    while (true) 
    //    {
    //        SpawnPaper();
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    public void SpawnPaper()
    {
        int index = Random.Range(0, subjects.Length);
        GameObject newPaper = Instantiate(paperPrefab, otherPaperPosition.transform.position, Quaternion.identity, transform);
        newPaper.GetComponent<Paper>().InitializePaper(subjects[index]);
        newPaper.transform.localRotation = Quaternion.identity;
        if (papers.Count == 0) newPaper.GetComponent<Paper>().MoveToPosition(currentPaperPosition.transform.position);
        if (papers.Count > 1) newPaper.SetActive(false);
        papers.Add(newPaper);
    }

    private void Update()
    {
        if (!gameFinished)
        {
            if (papers.Count > 0 && !onCooldown)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    pikachuController.PassPaperLeft();
                    bool success = animalBox.ReceivePaper(papers[0]);
                    pikachuController.SetPassCorrectness(success);

                    StartCoroutine(ShufflePagePositions());
                }
                else if (Input.GetKeyDown(KeyCode.G))
                {
                    pikachuController.PassPaperRight();
                    bool success = humanBox.ReceivePaper(papers[0]);
                    pikachuController.SetPassCorrectness(success);

                    StartCoroutine(ShufflePagePositions());
                }
            }
        }
    }

    void StopCooldown()
    {
        onCooldown = false;
    }

    IEnumerator ShufflePagePositions()
    {
        papers.RemoveAt(0);
        onCooldown = true;
        Invoke(nameof(StopCooldown), 0.1f);
        if (papers.Count > 0) papers[0].GetComponent<Paper>().MoveToPosition(currentPaperPosition.transform.position);
        yield return new WaitForSeconds(0.5f);
        if (papers.Count > 1)
        {
            papers[1].SetActive(true);
        } 
    }

    public void WinGame()
    {
        gameFinished = true;
        pikachuController.Success();
    }
}
