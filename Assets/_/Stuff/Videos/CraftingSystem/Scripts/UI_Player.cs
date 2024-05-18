using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Player : MonoBehaviour {

    private TextMeshProUGUI goldText;
    private TextMeshProUGUI healthPotionText;

    private void Awake() {
        goldText = transform.Find("goldText").GetComponent<TextMeshProUGUI>();
        healthPotionText = transform.Find("healthPotionText").GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        UpdateText();

        Piratz.Instance.OnGoldAmountChanged += Instance_OnGoldAmountChanged;
        Piratz.Instance.OnHealthPotionAmountChanged += Instance_OnHealthPotionAmountChanged;
    }

    private void Instance_OnHealthPotionAmountChanged(object sender, System.EventArgs e) {
        UpdateText();
    }

    private void Instance_OnGoldAmountChanged(object sender, System.EventArgs e) {
        UpdateText();
    }

    private void UpdateText() {
        goldText.text = Piratz.Instance.GetGoldAmount().ToString();
        healthPotionText.text = Piratz.Instance.GetHealthPotionAmount().ToString();
    }

}
