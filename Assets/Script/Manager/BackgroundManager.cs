using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("플레이어")]
    public Transform player;
    [Header("배경크기")]
    public float size = 30; //3000pixel
    [Header("각 위치 배경")]
    public Transform[] up;
    public Transform[] down;
    public Transform[] middle_vertical;
    public Transform[] left;
    public Transform[] right;
    public Transform[] middle_horizontal;

    private void Update()
    {
        Vector2 player_position = player.position;
        Vector2 main_vertical = middle_vertical[1].position; // 위아래
        Vector2 main_horizontal = middle_horizontal[1].position; // 옆

        if (main_horizontal.x + (size * ((float)2 /3)) < player_position.x)
        {
            Transform[] temp_left = left;
            Transform[] temp_right = right;
            Transform[] temp_middle = middle_horizontal;

            for (int i = 0; i < left.Length; i++)
            {
                left[i].transform.position = new Vector2(right[i].position.x + size , right[i].position.y);
            }

            right = temp_left;
            left = temp_middle;
            middle_horizontal = temp_right;
        }
        if (main_horizontal.x - (size * ((float)2 / 3)) > player_position.x)
        {
            Transform[] temp_left = left;
            Transform[] temp_right = right;
            Transform[] temp_middle = middle_horizontal;

            for (int i = 0; i < right.Length; i++)
            {
                right[i].transform.position = new Vector2(left[i].position.x - size, left[i].position.y);
            }

            middle_horizontal = temp_left;
            right = temp_middle;
            left = temp_right;
        }
        if (main_vertical.y + (size * ((float)2 / 3)) < player_position.y)
        {
            Transform[] temp_down = down;
            Transform[] temp_up = up;
            Transform[] temp_middle = middle_vertical;

            for (int i = 0; i < down.Length; i++)
            {
                down[i].transform.position = new Vector2(up[i].position.x , up[i].position.y + size);
            }

            up = temp_down;
            down = temp_middle;
            middle_vertical = temp_up;
        }
        if (main_vertical.y - (size * ((float)2 / 3)) > player_position.y)
        {
            Transform[] temp_down = down;
            Transform[] temp_up = up;
            Transform[] temp_middle = middle_vertical;

            for (int i = 0; i < up.Length; i++)
            {
                up[i].transform.position = new Vector2(down[i].position.x , down[i].position.y - size);
            }

            middle_vertical = temp_down;
            up = temp_middle;
            down = temp_up;
        }
    }
}
