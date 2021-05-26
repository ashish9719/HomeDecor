using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject furniture;
    private Button btn;
    [SerializeField]private RawImage buttonImage;

    private int itemid;
    private Sprite buttonTexture;

    

    public int ItemId
    {
        set => itemid = value;
    }
    public Sprite ButtonTexture
    {
        set
        {
            buttonTexture = value;
            buttonImage.texture = buttonTexture.texture;
        }
    }

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SelectObject);
        DOTween.SetTweensCapacity(500,50);
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.onEntered(gameObject))
        {
            transform.DOScale(Vector3.one * 2, 0.3f);
            //transform.localScale = Vector3.one * 2;
        }
        else
        {
            transform.DOScale(Vector3.one , 0.3f);
            //transform.localScale = Vector3.one;
        }
    }

    void SelectObject()
    {
        // DataHandler.Instance.furniture = furniture;

        DataHandler.Instance.SetFurniture(itemid);
    }
}
