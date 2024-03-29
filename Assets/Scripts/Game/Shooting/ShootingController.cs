using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public static ShootingController instance;

    public Transform bushSkillPrefab;
    public Transform bulletPrefab;
    
    
    private bool isDragging = false; 
    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 direction;
    private Vector2 force;
    private float distance;
    
    [Header("Trajectory")]
    [SerializeField] private int dotsNumber;
    [SerializeField] private GameObject dotsParent;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private float dotSpacing;
    private Transform[] dotsList;
    private Vector2 pos;
    private float timeStamp;
    private Transform lastDot;
    [SerializeField] [Range (0.01f, 0.3f)] float dotMinScale;
    [SerializeField] [Range (0.3f, 1f)] float dotMaxScale;
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        Hide();
        PrepareDots();
    }

    private void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if (isDragging)
        {
            OnDrag();
        }
    }

    public void Show ()
    {
        dotsParent.SetActive (true);
    }

    public void Hide ()
    {
        dotsParent.SetActive (false);
    }

    void PrepareDots ()
    {
        dotsList = new Transform[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

        float scale = dotMaxScale;
        float scaleFactor = scale / dotsNumber;
        
        for (int i = 0; i < dotsNumber; i++) {
            dotsList [i] = Instantiate (dotPrefab, null).transform;
            dotsList [i].parent = dotsParent.transform;

            dotsList [i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
                scale -= scaleFactor;
        }
        
        lastDot = dotsList[dotsNumber-1];
        
    }

    public void UpdateDots (Vector3 ballPos, Vector2 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++) {
            pos.x = (ballPos.x + forceApplied.x * timeStamp);
            pos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;
		
            //you can simlify this 2 lines at the top by:
            //pos = (ballPos+force*time)-((-Physics2D.gravity*time*time)/2f);
            //but make sure to turn "pos" in Ball.cs to Vector2 instead of Vector3	
			
            dotsList [i].position = pos;
            timeStamp += dotSpacing;
        }
        
        lastDot = dotsList[dotsNumber-1];
    }

    private void OnDragStart()
    {
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Show();
    }
    
    private void OnDrag()
    {
        endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * 4f;

        
        Debug.DrawLine(startPoint, endPoint);
        
        UpdateDots(transform.position, force);
    }
    
    private void OnDragEnd()
    {
        switch (GameController.instance.currentSkill.type)
        {
            case SkillType.Ignis:
                ShootBullet();
                break;
            case SkillType.Aqua:
                Instantiate(bushSkillPrefab, lastDot.position, Quaternion.identity);
                break;
        }
        Hide();
    }

    private void ShootBullet()
    {
        bulletPrefab = GameController.instance.currentSkillPrefab;
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.localPosition = new Vector2(transform.localPosition.x + 0.75f, transform.localPosition.y + 0.75f) ;   
        var bulletRg = bullet.AddComponent<Rigidbody2D>();
        bulletRg.AddForce(force, ForceMode2D.Impulse);
    }
}
