using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Grid
    {
    #region Attributes
        private int m_width;
        private int m_height;

        private float m_size;
        private float m_padding;

        private Cell[,] m_cells;
    #endregion // Attributes

    #region Constructors
        // Constructor by value
        public Grid(int width, int height, float size, float padding)
        {
            m_width = width;
            m_height = height;

            m_size = size;
            m_padding = padding;

            m_cells = new Cell[m_width, m_height];

            for (int y = 0; y < m_height; ++y)
            {
                for (int x = 0; x < m_width; ++x)
                {
                    m_cells[x, y] = new Cell();
                }
            }
        }
        // Constructor by copy
        public Grid(Grid model)
        {
            m_width = model.Width;
            m_height = model.Height;

            m_size = model.Size;
            m_padding = model.Padding;

            m_cells = model.Cells;
        }
    #endregion // Constructors

    #region Accessors
        public int Width { get => m_width; set => m_width = value; }
        public int Height { get => m_height; set => m_height = value; }
        public float Size { get => m_size; set => m_size = value; }
        public float Padding { get => m_padding; set => m_padding = value; }
        public Cell[,] Cells { get => m_cells; set => m_cells = value; }
    #endregion // Accessors

    #region Functions
        public void Draw(ref GameObject cellPrefab)
        {
            float x, y;
            for (int j = 0; j < m_height; ++j)
            {
                for (int i = 0; i < m_width; ++i)
                {
                    x = - (m_size/2) - (m_size + m_padding) * i;
                    y = - (m_size/2) - (m_size + m_padding) * j;

                    GameObject obj = GameObject.Instantiate(cellPrefab, new Vector3(x, y), cellPrefab.transform.rotation);
                    obj.transform.localScale = new Vector3(m_size, 0.1f, m_size);
                    obj.GetComponent<MeshRenderer>().material = m_cells[i,j].ProvData.Terrain.Color;
                }
            }
        }

        // Get a Cell by a world position
        public Cell GetCell(Vector3 worldPos)
        {
            if (worldPos.x > 0 || worldPos.y > 0)
                return null;

            int x = Mathf.FloorToInt(Mathf.Abs(worldPos.x) / (m_size + m_padding));
            int y = Mathf.FloorToInt(Mathf.Abs(worldPos.y) / (m_size + m_padding));

            if (x < m_width && y < m_height)
                return m_cells[x, y];
            
            return null;
        }
        // Get a Cell by its id
        public Cell GetCell(int id)
        {
            if (id < 0)
                return null;

            foreach (Cell cell in m_cells)
            {
                if (cell.Id == id)
                    return cell;
            }

            return null;
        }

        public void Print()
        {
            for (int x = 0; x < m_width; ++x)
            {
                for (int y = 0; y < m_height; ++y)
                {
                    Debug.Log("Cell " + m_cells[x, y].Id + ":");
                    Debug.Log("\t-Value :" + m_cells[x, y].ProvData);
                    Debug.Log("\t-Adjacent Cells :");

                    if (m_cells[x, y].AdjacentCells.Count != 0)
                    {
                        foreach (KeyValuePair<Cell, float> pair in m_cells[x, y].AdjacentCells)
                            Debug.Log("\t\tCell " + pair.Key.Id + ", distance: " + pair.Value);
                    }
                }
            }
        }
    #endregion // Functions
    } // Grid
}
