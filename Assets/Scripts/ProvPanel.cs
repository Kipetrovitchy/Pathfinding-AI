using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProvPanel : MonoBehaviour
{
    public Game.GameManager gameManager;

    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();

        if (m_animator != null)
            m_animator.SetBool("Open", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.selected != null)
        {
            if (m_animator != null)
                m_animator.SetBool("Open", true);

            UpdatePanel(gameManager.selected);
        }
        else
        {
            if (m_animator != null)
                m_animator.SetBool("Open", false);
        }
    }

    // Update the province panel with the information of the last selected province
    public void UpdatePanel(Game.Cell selectedCell)
    {
        // Update name of the province
        transform.GetChild(0).GetComponent<Text>().text = selectedCell.ProvData.Name;
        // Update the type of terrain of the province
        transform.GetChild(1).GetComponent<Image>().sprite = selectedCell.ProvData.Terrain.TypeSprite;
        // Update combat and movement modifiers of the province
        transform.GetChild(2).GetChild(1).GetComponent<Text>().text = selectedCell.ProvData.Terrain.AtkMod.ToString();
        transform.GetChild(3).GetChild(1).GetComponent<Text>().text = selectedCell.ProvData.Terrain.DefMod.ToString();
        transform.GetChild(4).GetChild(1).GetComponent<Text>().text = selectedCell.ProvData.Terrain.MovMod.ToString();
    }
}
