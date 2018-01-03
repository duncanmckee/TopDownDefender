using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRandomizer : MonoBehaviour {

    public Material red;
    public Material yellow;
    public Material green;
    public Material cyan;
    public Material blue;
    public Material purple;
    public Material pink;

    public string upgradeName = "";
    private int upgrade = 0;

	// Use this for initialization
	void Start () {
        upgrade = Random.Range(0, 7);
        if (upgrade == 0)
        {
            this.GetComponent<Renderer>().material = red;
            upgradeName = "Fireball";
        }
        else if(upgrade == 1)
        {
            this.GetComponent<Renderer>().material = yellow;
            upgradeName = "EggCannon";
        }
        else if (upgrade == 2)
        {
            this.GetComponent<Renderer>().material = green;
            upgradeName = "Peashooter";
        }
        else if (upgrade == 3)
        {
            this.GetComponent<Renderer>().material = cyan;
            upgradeName = "PlasmaRifle";
        }
        else if (upgrade == 4)
        {
            this.GetComponent<Renderer>().material = blue;
            upgradeName = "Crossbow";
        }
        else if (upgrade == 5)
        {
            this.GetComponent<Renderer>().material = purple;
            upgradeName = "Crossbow";
        }
        else if (upgrade == 6)
        {
            this.GetComponent<Renderer>().material = pink;
            upgradeName = "Crossbow";
        }
    }
}
