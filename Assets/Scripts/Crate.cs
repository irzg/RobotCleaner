using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Crate : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer = null;
    private Collider2D _collider = null;
    private Rigidbody2D _body = null;

    [SerializeField]
    private Sprite _redBox = null;

    [SerializeField]
    private Sprite _blueBox = null;

    [SerializeField]
    private CrateColor _color = CrateColor.Blue;

    [SerializeField]
    private Transform _ikTarget = null;

    public Transform IKTarget
    {
        get { return _ikTarget; }
    }
    
    public CrateColor Color
    {
        get { return _color; }  
        set
        {
            _color = value;           
        }
    }

    public Rigidbody2D Body
    {
        get { return _body; }
    }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _body = GetComponent<Rigidbody2D>();

        SetSprite();
    }

    void Update()
    {        
    }

    private void SetSprite()
    {
        switch (_color)
        {
            case CrateColor.Red:
                _spriteRenderer.sprite = _redBox;
                break;

            case CrateColor.Blue:
                _spriteRenderer.sprite = _blueBox;
                break;
        }
    }

}
