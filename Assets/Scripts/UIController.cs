using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text PointsText;
    [SerializeField]
    private Text GoalText;

    [SerializeField]
    private GameObject WinAnnounce;

    [SerializeField]
    private GameObject LoseAnnounce;

    private float Points;

    private float PointsToWin;

    void Start()
    {
        CursorState(false);
    }

    private void CursorState(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = state ? 0 : 1;
        Cursor.visible = state;
    }

    public void AddPoints()
    {
        Points++;
        PointsText.text = "Killed Enemies: " + Points.ToString();

        if(Points == PointsToWin)
            Win();
    }

    public void Win()
    {
        WinAnnounce.SetActive(true);
        CursorState(true);
    }

    public void Lose()
    {
        LoseAnnounce.SetActive(true);
        CursorState(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetGoal(float goal)
    {
        PointsToWin = goal;
        GoalText.text = "Goal: " + goal.ToString();
    }
}
