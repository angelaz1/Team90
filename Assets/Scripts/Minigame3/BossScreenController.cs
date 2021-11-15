using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScreenController : MonoBehaviour
{
    public Image bossImage;

    public Sprite happyBoss;
    public Sprite sadBoss;
    public Sprite angryBoss;
    public Sprite ooohBoss;

    public void MakeBossAngry()
    {
        bossImage.sprite = angryBoss;
    }

    public void MakeBossSad()
    {
        bossImage.sprite = sadBoss;
    }

    public void MakeBossOooh()
    {
        bossImage.sprite = ooohBoss;
    }

    public void MakeBossHappy()
    {
        bossImage.sprite = happyBoss;
    }
}
