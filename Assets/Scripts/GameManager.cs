using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class GameManager : MonoBehaviour {
    public TextMeshProUGUI score;

    void Update() {
        score.text = "" + Fireball.score;
    }
}
