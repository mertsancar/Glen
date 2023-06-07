using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Bandit : Character {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;

    private Vector2 nearestSpot;
    public bool isRight = true;

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

        
    }

    private void FixedUpdate()
    {
        if (!GameController.instance.gameOver)
        {
            Patrolling();
        }
        
    }

    // Update is called once per frame
    //void Update () {
    //       //Check if character just landed on the ground
    //       if (!m_grounded && m_groundSensor.State()) {
    //           m_grounded = true;
    //           m_animator.SetBool("Grounded", m_grounded);
    //       }

    //       //Check if character just started falling
    //       if(m_grounded && !m_groundSensor.State()) {
    //           m_grounded = false;
    //           m_animator.SetBool("Grounded", m_grounded);
    //       }


    //       //Set AirSpeed in animator
    //       m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

    //       // -- Handle Animations --
    //       //Death
    //       if (Input.GetKeyDown("e")) {
    //           if(!m_isDead)
    //               m_animator.SetTrigger("Death");
    //           else
    //               m_animator.SetTrigger("Recover");

    //           m_isDead = !m_isDead;
    //       }

    //       //Hurt
    //       else if (Input.GetKeyDown("q"))
    //           m_animator.SetTrigger("Hurt");

    //       //Attack
    //       else if(Input.GetMouseButtonDown(0)) {
    //           m_animator.SetTrigger("Attack");
    //       }

    //       //Change between idle and combat idle
    //       else if (Input.GetKeyDown("f"))
    //           m_combatIdle = !m_combatIdle;

    //       //Jump
    //       else if (Input.GetKeyDown("space") && m_grounded) {
    //           m_animator.SetTrigger("Jump");
    //           m_grounded = false;
    //           m_animator.SetBool("Grounded", m_grounded);
    //           m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
    //           m_groundSensor.Disable(0.2f);
    //       }

    //       //Run
    //       else if (Mathf.Abs(inputX) > Mathf.Epsilon)
    //           m_animator.SetInteger("AnimState", 2);

    //       //Combat Idle
    //       else if (m_combatIdle)
    //           m_animator.SetInteger("AnimState", 1);

    //       //Idle
    //       else
    //           m_animator.SetInteger("AnimState", 0);
    //   }

    private void Patrolling()
    {
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        var distance = Vector2.Distance(playerObject.transform.position, transform.position);

        
        if (distance <= 1f)
        {
            Debug.Log("Attacking");
            // m_animator.SetInteger("AnimState", 1);
            m_animator.SetBool("Attacking", true);
        }
        else if (distance > 1f && distance < 5f) // && !playerObject.GetComponent<Player>().isStealth
        {
            m_animator.SetBool("Attacking", false);
            m_animator.SetInteger("AnimState", 2);
            transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, 3f * Time.deltaTime);
        }
        else if (distance >= 5f)
        {
            m_animator.SetBool("Attacking", false);
            m_animator.SetInteger("AnimState", 1);
        }

    }

    private void AttackPlayer()
    {
        if (!GameController.instance.gameOver)
        {
            GameObject.FindWithTag("Player").GetComponent<Character>().TakeDamage(1);
        }
    }
    
}
