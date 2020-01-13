using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    public GameObject CAMERA_OBJ;
    public GameObject ROTATOR;
    public GameObject PLAYER;
    public GameObject GOLF_CLUB;
    public GameObject BALL;
    public GameObject POINTER;
    public GameObject PLANE;

    public float POINTER_LENGTH = 2.5e-03f;
    public float HIT_TIMER = .05f;
    public float BALL_STOPPING_SPEED = .5f;
    public PLAY_STATE play_state = PLAY_STATE.waiting_for_player;
  
    private float strength = 0.0f;
    private float left_mouse_x = 0;
    private float left_mouse_y = 0;
    private float right_mouse_x = 0;
    private float right_mouse_y = 0;
    private float timer = 1.0f;
    private float last_strength = 0.0f;
    private float last_camera_angle_x = 0.0f;
    private float last_camera_angle_y = 0.0f;
    private bool left_mouse_clicked = false;
    private bool right_mouse_clicked = false;
    private bool left_arrow_clicked = false;
    private bool right_arrow_clicked = false;
    private bool w_clicked = false;
    private bool a_clicked = false;
    private bool s_clicked = false;
    private bool d_clicked = false;
    private bool moving_club = false;
    private Rigidbody ball_rb;

    public enum PLAY_STATE
    {
        waiting_for_player,
        hitting_ball,
        ball_rolling
    }

    // Start is called before the first frame update
    void Start()
    {
        ball_rb = BALL.GetComponent<Rigidbody>();
        PLANE.transform.localScale = new Vector3( 5* POINTER_LENGTH, 1, POINTER_LENGTH );
        PLANE.transform.localPosition = new Vector3(PLANE.transform.localScale.x * 5, BALL.transform.localScale.y / -2.1f, 0);
        _next_turn();
    }

    // Update is called once per frame
    void Update()
    {
        switch (play_state)
        {
            case PLAY_STATE.waiting_for_player:
                ball_rb.velocity = Vector3.zero;
                _handle_left_click();
                _handle_right_click();
                _handle_arrow_keys();
                _handle_ws();
                _handle_ad();
                break;
            case PLAY_STATE.hitting_ball:
                if ( timer > 0)
                {
                    GOLF_CLUB.transform.Rotate(0, -1 * last_strength * Time.deltaTime / HIT_TIMER, 0);
                    timer = timer - Time.deltaTime;
                }
                else
                {
                    float thrust = last_strength * 30;
                    ball_rb.AddForce( ROTATOR.transform.right * thrust );
                    play_state = PLAY_STATE.ball_rolling;
                    moving_club = true;
                }
                break;
            case PLAY_STATE.ball_rolling:
                if (timer > -1 * HIT_TIMER)
                {
                    GOLF_CLUB.transform.Rotate(0, -1 * last_strength * Time.deltaTime / HIT_TIMER, 0);
                    timer = timer - Time.deltaTime;
                }
                else if (timer > -4 * HIT_TIMER)
                {
                    timer = timer - Time.deltaTime;
                }
                else if (moving_club)
                {
                    moving_club = false;
                    GOLF_CLUB.transform.Rotate(0, last_strength, 0);
                    ROTATOR.SetActive(false);
                }
                else if (ball_rb.velocity.magnitude < BALL_STOPPING_SPEED )
                {
                    ball_rb.velocity = Vector3.zero;
                    _next_turn();
                }
                break;
        }
    }

    private void _next_turn()
    {
        ROTATOR.SetActive(true);
        PLAYER.transform.position = BALL.transform.position;
        play_state = PLAY_STATE.waiting_for_player;
    }

    private void _handle_arrow_keys()
    {
        if (left_arrow_clicked)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                left_arrow_clicked = false;
            }
            else
            {
                ROTATOR.transform.Rotate(0, 1, 0);
            }
        }
        else if (right_arrow_clicked)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                right_arrow_clicked = false;
            }
            else
            {
                ROTATOR.transform.Rotate(0, -1, 0);
            }
        }
        if (!right_arrow_clicked && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left_arrow_clicked = true;
        }

        if (!left_arrow_clicked && Input.GetKeyDown(KeyCode.RightArrow))
        {
            right_arrow_clicked = true;
        }
    }

    private void _handle_left_click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            left_mouse_clicked = true;
            left_mouse_x = Input.mousePosition.x;
            left_mouse_y = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0) && left_mouse_clicked)
        {
            play_state = PLAY_STATE.hitting_ball;
            timer = HIT_TIMER;
            left_mouse_clicked = false;
            left_mouse_x = 0;
            left_mouse_y = 0;
        }
        else if (left_mouse_clicked)
        {
            float strength = Math.Min(Math.Abs(Input.mousePosition.y - left_mouse_y) / 15, 30);
            PLANE.transform.localScale = new Vector3( strength * POINTER_LENGTH, 1, POINTER_LENGTH );
            PLANE.transform.localPosition = new Vector3( PLANE.transform.localScale.x * 5, BALL.transform.localScale.y/-2.1f, 0 );
            GOLF_CLUB.transform.Rotate( 0, strength - last_strength, 0 );

            last_strength = strength;
        }
    }

    private void _handle_right_click()
    {
        if (Input.GetMouseButtonDown(1))
        {
            right_mouse_clicked = true;
            right_mouse_x = Input.mousePosition.x;
            right_mouse_y = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(1) && right_mouse_clicked)
        {
            right_mouse_clicked = false;
            right_mouse_x = 0;
            right_mouse_y = 0;
            last_camera_angle_x = 0f;
            last_camera_angle_y = 0f;
        }
        else if (right_mouse_clicked)
        {
            float curr_x_angle = (Input.mousePosition.x - right_mouse_x) / 10;
            float curr_y_angle = (Input.mousePosition.y - right_mouse_y) / -10;
            CAMERA_OBJ.transform.Rotate(curr_y_angle - last_camera_angle_y, curr_x_angle - last_camera_angle_x, 0);

            last_camera_angle_x = curr_x_angle;
            last_camera_angle_y = curr_y_angle;
        }
    }

    private void _handle_ws()
    {
        if (w_clicked)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                w_clicked = false;
            }
            else
            {
                CAMERA_OBJ.transform.Translate(2 * Vector3.forward * Time.deltaTime);
            }
        }
        else if (s_clicked)
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                s_clicked = false;
            }
            else
            {
                CAMERA_OBJ.transform.Translate(-2f * Vector3.forward * Time.deltaTime);
            }
        }
        if (!s_clicked && Input.GetKeyDown(KeyCode.W))
        {
            w_clicked = true;
        }

        if (!w_clicked && Input.GetKeyDown(KeyCode.S))
        {
            s_clicked = true;
        }
    }

    private void _handle_ad()
    {
        if (a_clicked)
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                a_clicked = false;
            }
            else
            {
                CAMERA_OBJ.transform.Translate(-2 * Vector3.right * Time.deltaTime);
            }
        }
        else if (d_clicked)
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                d_clicked = false;
            }
            else
            {
                CAMERA_OBJ.transform.Translate(2 * Vector3.right * Time.deltaTime);
            }
        }
        if (!d_clicked && Input.GetKeyDown(KeyCode.A))
        {
            a_clicked = true;
        }

        if (!a_clicked && Input.GetKeyDown(KeyCode.D))
        {
            d_clicked = true;
        }
    }
}