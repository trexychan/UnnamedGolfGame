using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    public GameObject camera_obj;
    public GameObject rotator;
    public GameObject player;
    public GameObject golf_club;
    public GameObject ball;
    public GameObject pointer;
    public float strength = 0.0f;
    public float default_pointer_len = 1e-05f;
    public float left_mouse_x = 0;
    public float left_mouse_y = 0;
    public float right_mouse_x = 0;
    public float right_mouse_y = 0;
    public PLAY_STATE play_state = PLAY_STATE.waiting_for_player;


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

    public enum PLAY_STATE
    {
        waiting_for_player,
        hitting_ball,
        ball_rolling
    }

    // Start is called before the first frame update
    void Start()
    {
        play_state = PLAY_STATE.waiting_for_player;
    }

    // Update is called once per frame
    void Update()
    {
        switch (play_state)
        {
            case PLAY_STATE.waiting_for_player:
                _handle_left_click();
                _handle_right_click();
                _handle_arrow_keys();
                _handle_ws();
                _handle_ad();
                break;
            case PLAY_STATE.hitting_ball:
                break;
            case PLAY_STATE.ball_rolling:
                break;
        }
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
                rotator.transform.Rotate(0, 1, 0);
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
                rotator.transform.Rotate(0, -1, 0);
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
            left_mouse_clicked = false;
            left_mouse_x = 0;
            left_mouse_y = 0;
            last_strength = 0f;
        }
        else if (left_mouse_clicked)
        {
            float strength = Math.Min(Math.Abs(Input.mousePosition.y - left_mouse_y) / 15, 30);
            pointer.transform.localScale = new Vector3(default_pointer_len, 1, strength * default_pointer_len);

            pointer.transform.localPosition = new Vector3(0, 0, pointer.transform.localScale.z * 5);
            golf_club.transform.Rotate(0, strength - last_strength, 0);

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
            camera_obj.transform.Rotate(curr_y_angle - last_camera_angle_y, curr_x_angle - last_camera_angle_x, 0);

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
                camera_obj.transform.Translate(2 * Vector3.forward * Time.deltaTime);
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
                camera_obj.transform.Translate(-2f * Vector3.forward * Time.deltaTime);
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
                camera_obj.transform.Translate(-2 * Vector3.right * Time.deltaTime);
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
                camera_obj.transform.Translate(2 * Vector3.right * Time.deltaTime);
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