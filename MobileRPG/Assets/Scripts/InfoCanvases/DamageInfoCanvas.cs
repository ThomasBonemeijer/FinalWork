using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageInfoCanvas : MonoBehaviour
{
    public TMP_Text theText;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        // animator = GetComponent<Animator>();
    }

    void Update() {
        // transform.localScale = new Vector3(Mathf.Abs(1), 1, 1);
    }

    public void ShowDamageNumbers(int damage) {
        theText.text = "-" + damage.ToString();
        GetComponent<Animator>().SetTrigger("Show");
    }
}
