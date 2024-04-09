using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawnController : MonoBehaviour
{
    public static ReSpawnController Instance;

    private Vector3 respawnPoint;
    public float waitToRespawn;
    private GameObject playerTF;
    public GameObject deathEffect;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTF = PlayerHealthController.Instance.gameObject;
        respawnPoint = playerTF.transform.position;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        playerTF.SetActive(false);
        if (deathEffect != null)
        {
            Instantiate(deathEffect, playerTF.transform.position, playerTF.transform.rotation);
        }
        yield return new WaitForSeconds(waitToRespawn);
        playerTF.transform.position = respawnPoint;
        playerTF.SetActive(true);
        PlayerHealthController.Instance.FillHealth();
    }

    public void SetSpawnPoint(Transform checkpoint)
    {
        respawnPoint = checkpoint.position;
    }
}

