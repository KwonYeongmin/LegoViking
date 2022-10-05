using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private Image HP_HUD;
    [SerializeField] private Image HP_Interface;
    [SerializeField] private TextMeshProUGUI HP_Value;
    private int MaxHP;
    private int CurrentHP;

    [Header("Ammo")]
    [SerializeField] private Image Ammo_HUD;
    [SerializeField] private Image Ammo_Interface;
    [SerializeField] private TextMeshProUGUI Ammo_Value;
    private int MaxAmmo;
    private int CurrentAmmo;

    private GameObject player;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera hud_Camera;
    [SerializeField] private RectTransform rectParent;
    [SerializeField] private RectTransform rectHUD;

    [HideInInspector] public Vector3 offset = Vector3.zero;
    [HideInInspector] public Transform targetTransform;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        canvas = GetComponentInParent<Canvas>();
        hud_Camera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHUD = GetComponent<RectTransform>();

        MaxHP = player.GetComponent<Player>().DefaultHP;
        MaxAmmo = player.GetComponent<Player>().defaultAmmo;
        UpdateHP(MaxHP);
        UpdateAmmo(MaxAmmo);
    }

    public void SetUpHUD()
    {
        var screenPos = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, hud_Camera, out localPos);
        rectHUD.localPosition = localPos;
    }

    public void UpdateHP(int hp)
    {
        CurrentHP = hp;
        HP_HUD.fillAmount = (float)CurrentHP / (float)MaxHP;
        HP_Interface.fillAmount = (float)CurrentHP / (float)MaxHP;
        HP_Value.text = CurrentHP.ToString() + "/" + MaxHP.ToString();
    }

    public void UpdateAmmo(int ammo)
    {
        CurrentAmmo = ammo;
        Ammo_HUD.fillAmount = (float)CurrentAmmo / (float)MaxAmmo;
        Ammo_Interface.fillAmount = (float)CurrentAmmo / (float)MaxAmmo;
        Ammo_Value.text = CurrentAmmo.ToString() + "/" + MaxAmmo.ToString();
    }
}
