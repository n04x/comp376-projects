using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private int m_width;
    private int m_height;
    private MiniMapRoom[] rooms;

    public GameObject RoomGraphicPrefab;
    
    public void Init(int width, int height)
    {
        m_width = width;
        m_height = height;
        rooms = new MiniMapRoom[m_width * m_height];
    }    

    public void CreateRoom(int x, int y)
    {
        rooms[x + y * m_width] = new MiniMapRoom(RoomGraphicPrefab, x, y, transform.position, this.gameObject);
    }

    public void PlayerExit(int x, int y)
    {
        rooms[x + y * m_width].PlayerExit();
    }
    
    public void PlayerEnter(int x, int y)
    {
        rooms[x + y * m_width].PlayerEnter();
        if (y - 1 >= 0 && rooms[x + (y - 1) * m_width] != null)
        {
            rooms[x + (y - 1) * m_width].Reveal();
        }
        if (y + 1 < m_height && rooms[x + (y + 1) * m_width] != null)
        {
            rooms[x + (y + 1) * m_width].Reveal();
        }
        if (x - 1 >= 0 && rooms[(x - 1) + y * m_width] != null)
        {
            rooms[(x - 1) + y * m_width].Reveal();
        }
        if (x + 1 < m_width && rooms[(x + 1) + y * m_width] != null)
        {
            rooms[(x + 1) + y * m_width].Reveal();
        }
    }
}
