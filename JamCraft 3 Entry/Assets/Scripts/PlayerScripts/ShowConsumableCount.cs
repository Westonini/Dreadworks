using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowConsumableCount : MonoBehaviour
{
    public GameObject pipebombImage;
    public TextMeshProUGUI pipebombCountText;
    public GameObject gauzeImage;
    public TextMeshProUGUI gauzeCountText;
    public GameObject keyImage;
    public TextMeshProUGUI keyCountText;

    private bool pipebombCraftedOnce = false;
    private bool gauzeCraftedOnce = false;
    private bool keyCraftedOnce = false;

    Inventory inv;

    // Start is called before the first frame update
    void Awake()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateConsumableCountText(inv.pipebombCount, ref pipebombCraftedOnce, pipebombImage, pipebombCountText);
        UpdateConsumableCountText(inv.gauzeCount, ref gauzeCraftedOnce, gauzeImage, gauzeCountText);
        UpdateConsumableCountText(inv.keys, ref keyCraftedOnce, keyImage, keyCountText);
    }

    void UpdateConsumableCountText(int count, ref bool craftedOnce, GameObject image, TextMeshProUGUI countText)
    {
        if (count > 0 || craftedOnce)
        {
            craftedOnce = true;
            image.SetActive(true);
            countText.text = count.ToString();
        }
    }
}
