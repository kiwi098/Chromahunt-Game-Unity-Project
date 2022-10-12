using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffect : MonoBehaviour
{

    public static AudioClip GiannisSoundAttack, LancetSoundAttack, SigneSoundAttack, EnemyHitSound, GoblinAttackSound;
    static AudioSource audioSrc;

    void Start()
    {
        GiannisSoundAttack = Resources.Load<AudioClip>("GiannisAttack");
        LancetSoundAttack = Resources.Load<AudioClip>("LancetAttack");
        SigneSoundAttack = Resources.Load<AudioClip>("SigneAttack");
        EnemyHitSound = Resources.Load<AudioClip>("EnemyHit");
        GoblinAttackSound = Resources.Load<AudioClip>("GoblinAttack");

        audioSrc = GetComponent<AudioSource>();

    }
   void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "GiannisAttack":
                audioSrc.PlayOneShot(GiannisSoundAttack);
                break;
            case "LancetAttack":
                audioSrc.PlayOneShot(LancetSoundAttack);
                break;
            case "SigneAttack":
                audioSrc.PlayOneShot(SigneSoundAttack);
                break;
            case "EnemyHit":
                audioSrc.PlayOneShot(EnemyHitSound);
                break;
            case "GoblinAttack":
                audioSrc.PlayOneShot(GoblinAttackSound);
                break;

        }



    }
}
