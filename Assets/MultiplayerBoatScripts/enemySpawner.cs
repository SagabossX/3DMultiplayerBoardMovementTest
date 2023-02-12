using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System;
public class enemySpawner : MonoBehaviour
{
    public static enemySpawner current; //make an instance so there is only 1 spawner running at any time.

    [SerializeField]
    TMP_Text _countdownTxt;
    [SerializeField]
    TMP_Text _waveTxt;
    [SerializeField]
    List<Transform> _spawnLocations;

    public int wave = 1;
    int _enemycount = 0;
    bool _waveisLaunched=false;
    bool win = false;
    PhotonView view;

    public event Action<Enemy> OnEnemyKilled; //action that is called whenever an anemy dies.

    private void Awake()
    {
        current = this;
    }
    private void OnEnable()
    {
        OnEnemyKilled += EnemyHasBeenKilled; //subscribe to the action
    }
    private void OnDisable()
    {
        OnEnemyKilled -= EnemyHasBeenKilled;
    }

    public void EnemyKilled(Enemy enm)
    {
        if (OnEnemyKilled != null)
        {
            OnEnemyKilled(enm);
        }
    }
    void Start()
    {
        view = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient && view.IsMine)
        {
            view.RPC("CountDownInitiater", RpcTarget.AllBuffered);     //start the countdown from the master client and pass it to all other players.
        }
    }

    [PunRPC]
    void CountDownInitiater()
    {
        StartCoroutine(CountDown());
    }
    
    IEnumerator CountDown()
    {
        int x = 3;
        while (x > 0)
        {
            yield return new WaitForSeconds(1f);
            _countdownTxt.text = x.ToString();
            x--;
        }
        _countdownTxt.text = "";
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(SpawnWave(wave));           //once countdown ends start spawning the wave
        }
        _waveTxt.gameObject.SetActive(true);
        _waveTxt.text ="Wave: "+ wave.ToString();
        wave++;
        
    }

    IEnumerator SpawnWave(int wavenumber)
    {
        if (PhotonNetwork.IsMasterClient)  //wave spawning is only done on the master client
        {
            for (int i = 0; i < wavenumber*PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                GameObject enemy = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Enemy"), _spawnLocations[UnityEngine.Random.Range(0, _spawnLocations.Count - 1)].position,transform.rotation) as GameObject; //spawn enemies every 0.5 seconds on a random location from the list.
                _enemycount++;
                yield return new WaitForSeconds(0.5f);
            }
            _waveisLaunched = true;
        }
    }

    void EnemyHasBeenKilled(Enemy _enemy)
    {
        _enemycount--;                //on enemy death decrease enemy count
    }
    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && view.IsMine)
        {
            if (_waveisLaunched && _enemycount == 0 && wave <= 5)   //check if the enemy count is 0 then all enemies have been killed prepare the next wave.
            {


                view.RPC("CountDownInitiater", RpcTarget.AllBuffered);
                _waveisLaunched = false;

            }
            else if (_waveisLaunched && _enemycount == 0 && wave == 6 && win == false)//if it is the final wave set win ui and display the scores.
            {
                win = true;

                ScoreManager.current.DisplayFinalScores();


            }
        }
    }
}
