using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

namespace Game
{
    public class Grid
    {
    #region Attributes
        private int m_width;
        private int m_height;

        private float m_cellSize;
        private float m_padding;

        private Cell[,] m_cells;
    #endregion // Attributes

    #region Constructors
        // Constructor by value
        public Grid(int width, int height, float cellSize, float padding)
        {
            m_width = (width >= 1 ? width : 1);
            m_height = (height >= 1 ? height : 1);

            m_cellSize = cellSize;
            m_padding = padding;

            m_cells = new Cell[m_width, m_height];

            for (int y = 0; y < m_height; ++y)
            {
                for (int x = 0; x < m_width; ++x)
                {
                    m_cells[x, y] = new Cell();
                }
            }

            InitDistanceBetweenCells();
        }
        // Constructor by copy
        public Grid(Grid model)
        {
            m_width = model.Width;
            m_height = model.Height;

            m_cellSize = model.CellSize;
            m_padding = model.Padding;

            m_cells = model.Cells;
        }
        // Constructor by JSON
        public Grid(string path)
        {
            // Read the JSON file
            string jsonString = File.ReadAllText (path);
            JSONNode data = JSON.Parse(jsonString);
            
            // Set the Grid variable
            m_width = data["Grid"]["width"].AsInt;
            m_height = data["Grid"]["height"].AsInt;
            
            m_cellSize = data["Grid"]["cellSize"].AsFloat;
            m_padding = data["Grid"]["padding"].AsFloat;

            if (data["Grid"]["Cells"] != null)
            {
                m_cells = new Cell[m_width, m_height];

                if (m_width > 0 && m_height > 0)
                {
                    // Create each Cell
                    foreach (JSONNode cell in data["Grid"]["Cells"])
                    {
                        int id = cell["id"].AsInt;
                        string name = cell["name"];
                        Terrain.Type type = (Terrain.Type)cell["type"].AsInt;

                        int x = id % m_width, y = id / m_height;
                        
                        if (y >= m_height)
                            break;
                        
                        m_cells[x, y] = new Cell(id, name, type);
                    }

                    InitDistanceBetweenCells();
                }
            }
            else
            {
                m_cells = null;
            }
        }
    #endregion // Constructors

    #region Accessors
        public int Width { get => m_width; set => m_width = value; }
        public int Height { get => m_height; set => m_height = value; }
        public float CellSize { get => m_cellSize; set => m_cellSize = value; }
        public float Padding { get => m_padding; set => m_padding = value; }
        public Cell[,] Cells { get => m_cells; set => m_cells = value; }
    #endregion // Accessors

    #region Functions
        // Initialize the distance between each adjacent cells
        private void InitDistanceBetweenCells()
        {
            if (m_width == 1 && m_height == 1)
                return;

            Cell curr;
            for (int y = 0; y < m_height; ++y)
            {
                for (int x = 0; x < m_width; ++x)
                {
                    curr = m_cells[x, y];
                    // If more than one column
                    if (m_width > 1)
                    {
                        // Check right Cell
                        if (x < m_width - 1)
                            curr.AddAdjacentCell(m_cells[x + 1, y]);
                        // Check left Cell
                        if (x > 0)
                            curr.AddAdjacentCell(m_cells[x - 1, y]);
                    }
                    // If more than one row
                    if (m_height > 1)
                    {
                        // Check upper Cell
                        if (y < m_height - 1)
                            curr.AddAdjacentCell(m_cells[x, y + 1]);
                        // Check lower Cell
                        if (y > 0)
                            curr.AddAdjacentCell(m_cells[x, y - 1]);
                    }
                }
            }
        }

        // Instantiate map tiles
        public void Draw(ref GameObject cellPrefab)
        {
            float x, y;
            for (int j = 0; j < m_height; ++j)
            {
                for (int i = 0; i < m_width; ++i)
                {
                    x = - (m_cellSize/2) - (m_cellSize + m_padding) * i;
                    y = - (m_cellSize/2) - (m_cellSize + m_padding) * j;

                    GameObject obj = GameObject.Instantiate(cellPrefab, new Vector3(x, y), cellPrefab.transform.rotation);
                    obj.transform.localScale = new Vector3(m_cellSize, m_cellSize, 0.1f);
                    obj.GetComponent<MeshRenderer>().material = m_cells[i,j].ProvData.Terrain.Color;

                    m_cells[i,j].m_object = obj;
                }
            }
        }

        public void UpdateVisuals()
        {
            for (int j = 0; j < m_height; ++j)
            {
                for (int i = 0; i < m_width; ++i)
                {
                    m_cells[i,j].m_object.GetComponent<MeshRenderer>().material = m_cells[i,j].ProvData.Terrain.Color;
                }
            }
        }

        // Get a Cell by a world position
        public Cell GetCell(Vector3 worldPos)
        {
            if (worldPos.x > 0 || worldPos.y > 0)
                return null;

            int x = Mathf.FloorToInt(Mathf.Abs(worldPos.x) / (m_cellSize + m_padding));
            int y = Mathf.FloorToInt(Mathf.Abs(worldPos.y) / (m_cellSize + m_padding));

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

        // Get the Cell position in the world
        public Vector3 GetCellPos(Cell cell)
        {
            float x = ((cell.Id % m_width) * (m_cellSize + m_padding)) + m_cellSize / 2;
            float y = ((cell.Id / m_width) * (m_cellSize + m_padding)) + m_cellSize / 2;

            return new Vector3(-x, -y, 0.1f);
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
