using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;
    private int highScore = 0;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    
    void Start()
    {
        // 게임 시작 시 최고 점수 불러오기
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            highScore = 0;
        }
        highScoreText.text = "High Score" + highScore.ToString();
        Debug.Log("저장된 최고 점수: " + highScore);
    }
    
    // 점수 추가 메서드
    public void AddScore(int points)
    {
        currentScore += points;
        
        currentScoreText.text = "Current Score" + currentScore.ToString();
        // 현재 점수가 최고 점수를 넘으면 갱신
        if (currentScore > highScore)
        {
            highScore = currentScore;
            highScoreText.text = "High Score" + highScore.ToString();
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();  // 즉시 저장
            Debug.Log("신기록! 최고 점수: " + highScore);
        }
    }
    
    // 최고 점수 초기화
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore = 0;
        Debug.Log("최고 점수가 초기화되었습니다.");
    }
}