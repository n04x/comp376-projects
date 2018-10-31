using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMap
{
    private int m_width;
    private int m_height;
    private Room[] m_map;
    public RoomMap(int width, int height)
    {
        m_width = width;
        m_height = height;
        m_map = new Room[width * height];
    }

    public Room GetRoom(int x, int y)
    {
        if (x >= m_width || y >= m_height || x < 0 || y < 0)
        {
            Debug.LogWarning("Coordinates " + x + ", " + y + " out of bounds");
            return null;
        }
        return m_map[x + m_width * y];
    }

    public void AddRoom(int x, int y, Room room)
    {
        if (x >= m_width || y >= m_height || x < 0 || y < 0)
        {
            Debug.LogWarning("Coordinates " + x + ", " + y + " out of bounds");
            return;
        }
        m_map[x + m_width * y] = room;
    }

    public void ForEach(Action<Room, int, int> udpateLogic)
    {
        for (int i = 0; i < m_width * m_height; ++i)
        {
            int x = i % m_width;
            int y = (i - x) / m_width;
            if (m_map[i] != null)
            {
                udpateLogic(m_map[i], x, y);
            }
        }
    }
}
