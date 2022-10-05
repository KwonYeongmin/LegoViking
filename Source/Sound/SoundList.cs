using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : MonoBehaviour
{
    [Header("Stage BGM")]
    public AudioClip stage1;
    public AudioClip stage1_Boss;
    public AudioClip stage2;
    public AudioClip stage2_Boss;
    public AudioClip stage3;
    public AudioClip stage3_Boss;
    public AudioClip stage4;
    public AudioClip stage4_Boss;

    [Header("Player Action Sound")]
    public AudioClip sound_walk_1;
    public AudioClip sound_walk_2;
    public AudioClip sound_roll;
    public AudioClip sound_shoot;
    public AudioClip sound_lack;
    public AudioClip sound_grenade;

    [Header("Item Sound")]
    public AudioClip sound_acquisition_item;
    public AudioClip sound_acquisition_bullet;
    public AudioClip sound_acquisition_heal;

    [Header("Enemy Attack")]
    public AudioClip sound_missile_hit;
    public AudioClip sound_missile_explosion;
    public AudioClip sound_arrow_hit;
    public AudioClip sound_arrow_stick;
    public AudioClip sound_dagger_hit;
    public AudioClip sound_dagger_move;

    [Header("Enemy Sound")]
    public AudioClip sound_monster_death;
    public AudioClip sound_monster_hit;

    [Header("Viking Sound")]
    public AudioClip sound_viking;
    public AudioClip sound_motor;

    [Header("UI Sound")]
    public AudioClip sound_button;
    public AudioClip sound_countdown;
    public AudioClip sound_win;
    public AudioClip sound_lose;

    // BGM
    public static AudioClip Stage1 { get; private set; }
    public static AudioClip Stage1_Boss { get; private set; }
    public static AudioClip Stage2 { get; private set; }
    public static AudioClip Stage2_Boss { get; private set; }
    public static AudioClip Stage3 { get; private set; }
    public static AudioClip Stage3_Boss { get; private set; }
    public static AudioClip Stage4 { get; private set; }
    public static AudioClip Stage4_Boss { get; private set; }
    //Player
    public static AudioClip Sound_walk_1 { get; private set; }
    public static AudioClip Sound_walk_2 { get; private set; }
    public static AudioClip Sound_roll { get; private set; }
    public static AudioClip Sound_shoot { get; private set; }
    public static AudioClip Sound_lack { get; private set; }
    public static AudioClip Sound_grenade { get; private set; }
    //Item
    public static AudioClip Sound_acquisition_item { get; private set; }
    public static AudioClip Sound_acquisition_bullet { get; private set; }
    public static AudioClip Sound_acquisition_heal { get; private set; }
    //Attack
    public static AudioClip Sound_missile_hit { get; private set; }
    public static AudioClip Sound_missile_explosion { get; private set; }
    public static AudioClip Sound_arrow_hit { get; private set; }
    public static AudioClip Sound_arrow_stick { get; private set; }
    public static AudioClip Sound_dagger_hit { get; private set; }
    public static AudioClip Sound_dagger_move { get; private set; }
    //Monster
    public static AudioClip Sound_monster_death { get; private set; }
    public static AudioClip Sound_monster_hit { get; private set; }

    // Viking
    public static AudioClip Sound_viking { get; private set; }
    public static AudioClip Sound_motor { get; private set; }
    public static AudioClip Sound_button { get; private set; }
    public static AudioClip Sound_countdown { get; private set; }
    public static AudioClip Sound_win { get; private set; }
    public static AudioClip Sound_lose { get; private set; }

    private void Awake()
    {
        Stage1 = stage1;
        Stage1_Boss = stage1_Boss;
        Stage2 = stage2;
        Stage2_Boss = stage2_Boss;
        Stage3 = stage3;
        Stage3_Boss = stage3_Boss;
        Stage4 = stage4;
        Stage4_Boss = stage4_Boss;
        Sound_walk_1 = sound_walk_1;
        Sound_walk_2 = sound_walk_2;
        Sound_roll = sound_roll;
        Sound_shoot = sound_shoot;
        Sound_lack = sound_lack;
        Sound_grenade = sound_grenade;
        Sound_acquisition_item = sound_acquisition_item;
        Sound_acquisition_bullet = sound_acquisition_bullet;
        Sound_acquisition_heal = sound_acquisition_heal;
        Sound_missile_hit = sound_missile_hit;
        Sound_missile_explosion = sound_missile_explosion;
        Sound_arrow_hit = sound_arrow_hit;
        Sound_arrow_stick = sound_arrow_stick;
        Sound_dagger_hit = sound_dagger_hit;
        Sound_dagger_move = sound_dagger_move;
        Sound_monster_death = sound_monster_death;
        Sound_monster_hit = sound_monster_hit;
        Sound_viking = sound_viking;
        Sound_motor = sound_motor;
        Sound_button = sound_button;
        Sound_countdown = sound_countdown;
        Sound_win = sound_win;
        Sound_lose = sound_lose;
    }
}
