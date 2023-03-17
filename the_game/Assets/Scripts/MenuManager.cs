using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MenuManager : MonoBehaviour, IPointerEnterHandler
{
    public int gameStartScene;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
    public void StartGame() 
    {
        SceneManager.LoadScene("SaveRoom");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       anim.SetTrigger("onHover");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("HELLO IMLEAVING THIS FIELD");
        anim.SetTrigger("onHoverExit");
    }
}
