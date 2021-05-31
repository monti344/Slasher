using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResult : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PanelDead _panelDead;

    private void OnEnable()
    {
        _player.Dead += OnDead;
    }

    private void OnDisable()
    {
        _player.Dead -= OnDead;
    }

    private void OnDead()
    {
        _panelDead.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
