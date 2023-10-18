using UnityEngine;
using TMPro;

public class bananaEndscreen : MonoBehaviour
{
    public TMP_Text canvasText;
    public TMP_Text worldText;
    public string score;
    void Start()
    {
        score = CoinUI.totalScore.ToString();
        canvasText.text = score + " / 5";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
