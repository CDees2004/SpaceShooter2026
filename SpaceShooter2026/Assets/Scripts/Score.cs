using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI txtScore;
    private float score;

    public float[] scoreHistory; 

    public static Score Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        score = 0.0f;
    }

    void Start()
    {
        txtScore = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        txtScore.text = $"{score}"; // "string interpolation"
    }

    public void HitEnemy()
    {
        score += 1_000;
    }

    public float GetScore()
    {
        return score;
    }

}
