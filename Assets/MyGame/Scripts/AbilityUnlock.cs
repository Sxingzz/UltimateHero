using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUnlock : MonoBehaviour
{
    public bool UnlockSkill2;
    public bool UnlockSkill3;
    public bool UnlockAttack;
    public bool UnlockBecomeS1;
    public bool UnlockBecomeS2;
    public GameObject PickupEffect;
    public string UnlockMessage;
    public TMP_Text UnlockText;
    public GameObject platformObject;
    public float textTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_PICKUP_GEM);
            }

            if (platformObject != null)
            {
                platformObject.SetActive(true);
            }
            PlayerAbilityTracker playerAbilityTracker = collision.GetComponentInParent<PlayerAbilityTracker>();

            if (UnlockSkill2)
            {
                playerAbilityTracker.CanSkill2 = true;
            }
            if (UnlockSkill3)
            {
                playerAbilityTracker.CanSkill3 = true;
            }
            if (UnlockAttack)
            {
                playerAbilityTracker.CanAttack = true;
            }
            if (UnlockBecomeS1)
            {
                playerAbilityTracker.CanBecomeS1 = true;
            }
            if (UnlockBecomeS2)
            {
                playerAbilityTracker.CanBecomeS2 = true;
            }

            Instantiate(PickupEffect, transform.position, transform.rotation);

            UnlockText.transform.parent.SetParent(null); // lấy text ra khỏi Ability Pickup để khỏi bị destroy liền
            UnlockText.transform.parent.position = transform.position; // set lại vị trí 
            UnlockText.text = UnlockMessage; // đổi text
            UnlockText.gameObject.SetActive(true);

            Destroy(UnlockText.transform.parent.gameObject, textTime);


            Destroy(gameObject);
        }
    }


}
