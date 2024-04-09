using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    // palm

    [SerializeField]
    private PalmController palmPrefabs;

    [SerializeField]
    private KamehaController KamehaPrefabs;

    [SerializeField]
    private Palm2Controller palm2Prefabs;

    [SerializeField]
    private Transform palmPoint;

    [SerializeField]
    private PlayerController playerController;

    // transform1
    public float waitToSuper1;
    private float Super1Counter;

    //transform 2
    public float waitToSuper2;
    private float Super2Counter;

    // super 1 palm
    private float ExertCounter;

    // skill 3 Exert
    private float Exert1Counter;

    // Update is called once per frame
    void Update()
    {
        // punch

        if (Input.GetButtonDown("Fire1")&& playerController.abilities.CanAttack)
        {
            playerController.playerStandingAnim.SetTrigger("Punch");
            playerController.playerSuper1Anim.SetTrigger("Punch1");
        }

        // Palm
        if (playerController.standingState.activeSelf && playerController.abilities.CanSkill2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Instantiate(palmPrefabs, palmPoint.position, palmPoint.rotation)
                    .SetMoveDirection(new Vector2(transform.localScale.x, 0f));
                playerController.playerStandingAnim.SetTrigger("Palm"); // gọi hiệu ứng chưởng
            }
        }
        else if (playerController.Super1State.activeSelf && playerController.abilities.CanSkill2)
        {
            if (Input.GetKey(KeyCode.Alpha2))
            {
                //count time +=time.deltatime
                //play animation gong
                //neu ma count time > 2 thì sẽ chưởng lun, và set count time về lại = 0
                //play animation chuong va sinh ra prefab chuong

                ExertCounter += Time.deltaTime;
                playerController.playerSuper1Anim.SetTrigger("Exert");
                if (ExertCounter > 1)
                {
                    playerController.playerSuper1Anim.Play("PlayerS1_Exert", 0, 1f);
                    return;
                }
            }

            // nếu mà thả ra thì gọi animation gồng và sinh ra chưởng
            if (Input.GetKeyUp(KeyCode.Alpha2)&& playerController.abilities.CanSkill2)
            {
                if (ExertCounter > 1)
                {
                    ExertCounter = 0;
                    playerController.playerSuper1Anim.ResetTrigger("Exert"); // đặt về trạng thái chưa kích hoạt
                    playerController.playerSuper1Anim.SetTrigger("Palm1");
                    Instantiate(KamehaPrefabs, palmPoint.position, palmPoint.rotation)
                        .SetMoveDirection(new Vector2(transform.localScale.x, 0f));
                }
                else
                {
                    playerController.playerSuper1Anim.ResetTrigger("Exert");
                }
            }
        else if (playerController.Super2State.activeSelf && playerController.abilities.CanSkill2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Instantiate(palm2Prefabs, palmPoint.position, palmPoint.rotation)
                .SetMoveDirection(new Vector2(transform.localScale.x, 0f));
                playerController.playerSuper2Anim.SetTrigger("Palm2"); // gọi hiệu ứng chưởng
            }
        }
    }
        // Exert skill 3
        if (playerController.Super2State.activeSelf && Input.GetKeyDown(KeyCode.Alpha3) && playerController.abilities.CanSkill3)
        {
            Exert1Counter += Time.deltaTime;
            playerController.playerSuper2Anim.SetTrigger("Exert2");

            if (Exert1Counter > 0.15f)
            {
                playerController.playerSuper2Anim.Play("PlayerS2_Exert", 0, 0.1f);
            }
            else if (Exert1Counter > 2f)
            {
                Exert1Counter = 0f;
                return;
            }
        }
    

        // Transform into super saiyan 1
        if (!playerController.Super1State.activeSelf && !playerController.Super2State.activeSelf)
    {

        if (Input.GetKeyDown(KeyCode.Alpha4) && playerController.abilities.CanBecomeS1)
        {
            Super1Counter -= Time.deltaTime;
            StartCoroutine(ChangePlayerState(1.5f, true, false));
        }
        else
        {
            Super1Counter = waitToSuper1;
        }
    }
    else if (playerController.Super1State.activeSelf && !playerController.Super2State.activeSelf)
    {
        // transform to Super 2
        if (Input.GetKeyDown(KeyCode.Alpha4) && playerController.abilities.CanBecomeS2)
        {
            if (Super1Counter > 0)
            {
                Super2Counter -= Time.deltaTime;
                StartCoroutine(ChangePlayerState(1.5f, false, true));
            }
            else
            {
                Super1Counter = waitToSuper1;
                playerController.Super2State.SetActive(false);
                playerController.Super1State.SetActive(false);
                playerController.standingState.SetActive(true);
            }
        }
        else
        {
            Super2Counter = waitToSuper2;
        }
    }
    else if (!playerController.Super1State.activeSelf && playerController.Super2State.activeSelf)
    {
        // return to standing state from Super 2
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Super1Counter -= Time.deltaTime;
            playerController.Super2State.SetActive(false);
            playerController.Super1State.SetActive(false);
            playerController.standingState.SetActive(true);
        }
        else
        {
            Super1Counter = waitToSuper1;
        }
    }
}
    

    private IEnumerator ChangePlayerState(float time, bool isSuper1State, bool isSuper2State) // hàm transform
{
    if (isSuper1State)
    {
        playerController.playerStandingAnim.SetTrigger("Transform"); // gọi hiệu ứng biến hình 1

    }
    if (isSuper2State)
    {
        playerController.playerSuper1Anim.SetTrigger("Transform1"); // gọi hiệu ứng biến hình 2
    }

    yield return new WaitForSeconds(time);

    if (isSuper1State)
    {

        playerController.Super1State.SetActive(true);
        playerController.standingState.SetActive(false);
        playerController.Super2State.SetActive(false);
    }
    else if (isSuper2State)
    {
        playerController.Super1State.SetActive(false);
        playerController.standingState.SetActive(false);
        playerController.Super2State.SetActive(true);
    }
    else
    {
        playerController.standingState.SetActive(true);
        playerController.Super1State.SetActive(false);
        playerController.Super2State.SetActive(false);
    }
}



//public void AttackSkill1()
//{
//    // viết hàm deal damge enemy
//    //Debug.Log("Skill 1");
//}

}
