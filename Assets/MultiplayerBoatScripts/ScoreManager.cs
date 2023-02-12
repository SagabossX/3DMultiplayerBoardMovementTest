using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour //used to manage player score and win or lose ui/conditions.
{
    public static ScoreManager current;

    PhotonView view;

    [SerializeField]
    TMP_Text _scoreText;
    [SerializeField]
    finalScore _finalscore;
    [SerializeField]
    Transform finalScorePanel;
    [SerializeField]
    GameObject _finalScoreCanvas;

    List<finalScore> _finalScoreItemList = new List<finalScore>();
    int[] score=new int[4];
    private void Awake()
    {
        current = this;                             //create an instance
        view = GetComponent<PhotonView>();
        for (int i = 0; i < 4; i++)              //set all the player scores to 0
        {
            score[i] = 0;
        }
    }
    private void OnEnable()
    {
        enemySpawner.current.OnEnemyKilled += IncreaseScoreOnenemyKill; //subscribe to enemykill action to set the score on kill.
    }
    private void OnDisable()
    {
        enemySpawner.current.OnEnemyKilled -= IncreaseScoreOnenemyKill;
    }
    void IncreaseScoreOnenemyKill(Enemy enemy)
    {
        if (enemy._playerThatKilled == 10)  //return if enemy dies by going in base and no player has killed it
        {
            return;
        }
        view.RPC("RPC_IncreaseScoreOnenemyKill", RpcTarget.AllBuffered, enemy._playerThatKilled); //call rpc on all player providing the player id who killed it.
    }
    [PunRPC]
    void RPC_IncreaseScoreOnenemyKill(int playerthatkilled)
    {
            score[playerthatkilled-1] += 10; //increase the score of the player in the array according to the player id provided.
            _scoreText.text = "Score: "+score[PhotonNetwork.LocalPlayer.ActorNumber - 1].ToString(); //set the score text for local player whenever there is a change in score.
    }
    public void DisplayFinalScores()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            view.RPC("RPC_DisplayFinalScores", RpcTarget.AllBuffered); //call the final scoreboard from the master client
        }
    }

    [PunRPC]
    void RPC_DisplayFinalScores()
    {
        _finalScoreCanvas.SetActive(true);
        foreach (finalScore item in _finalScoreItemList) //clear all list of finalscore items.
        {
            Destroy(item.gameObject);
        }
        _finalScoreItemList.Clear();

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            finalScore newFinalScoreItem = Instantiate(_finalscore, finalScorePanel);
            newFinalScoreItem.ShowFinalScore(player.Value.NickName.ToString(),score[player.Value.ActorNumber - 1].ToString()); //foreach player set the name and final score from the array.
            _finalScoreItemList.Add(newFinalScoreItem);
           
        }
    }

    public void onBackToLobbyClicked()
    {
        PhotonNetwork.LeaveRoom();  //on back to lobby clicked leave the room
        SceneManager.LoadScene(1);
    }
}
