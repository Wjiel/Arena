using DG.Tweening;
using System.Collections;
using UnityEngine;
public class GameScript : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private SpawnerScript SpawnerScr;
    [SerializeField] private SaveGame saveGame;

    [Header("UI")]
    [SerializeField] private GameObject TextStartGame;

    [Header("other")]
    [SerializeField] private Animator AnimatorGame;

    [SerializeField] private GameObject PanelShop;

    [SerializeField] private GameObject PausePanel;

    [SerializeField] private GameObject RecordPanel;

    private bool isGame;
    private bool isStop;
    private bool hideRecord;
    private void Start()
    {
        AnimatorGame.SetBool("StartGame", true);
        PanelShop.transform.DOMoveX(480, 1);
        RecordPanel.transform.DOMoveX(1770, 1);
        isGame = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isGame == false)
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isStop == false)
            {
                isStop = true;
                Time.timeScale = 0;
                PausePanel.SetActive(true);
            }
            else
            {
                isStop = false;
                Time.timeScale = 1;
                PausePanel.SetActive(false);
            }
        }
    }
    public void StartGame()
    {
        isGame = true;
        Cursor.visible = false;
        StartCoroutine(HidePanel());
        TextStartGame.SetActive(false);
        SpawnerScr.StartGame();
    }
    public void DeathPlaer()
    {
        saveGame.MySave();
        AnimatorGame.SetBool("Death", true);
    }
    private IEnumerator HidePanel()
    {
        PanelShop.transform.DOMoveX(-510, 1);
        RecordPanel.transform.DOMoveX(2070, 1);
        yield return new WaitForSeconds(1.5f);
        PanelShop.SetActive(false);
        RecordPanel.SetActive(false);
    }
    public void HidePanelRecord()
    {
        if (hideRecord == false)
        {
            RecordPanel.transform.DOMoveX(2070, 1);
            hideRecord = true;
        }
        else
        {
            RecordPanel.transform.DOMoveX(1770, 1);
            hideRecord = false;
        }
    }
}
