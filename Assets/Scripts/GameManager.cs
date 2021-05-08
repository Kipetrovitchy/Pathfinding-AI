using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
    #region Attributes
        public Camera mainCam;

        private Grid m_map;
        private PathfinderAStar m_pathfinder;

        public GameObject cellPrefab;
        public GameObject unitPrefab;

        public int columns, rows;
        public float padding;
        public float size;

        public Cell selectedCell = null;
        public GameObject selectedEntity = null;

        public LayerMask layer;
    #endregion
    #region Accessors
        public Grid Map { get => m_map; }
        public PathfinderAStar Pathfinder { get => m_pathfinder; }
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            // m_map = new Grid("Assets/Resources/Maps/Test.json");
            m_map = new Grid(columns, rows, size, padding);

            if (m_map.Cells != null)
                m_map.Draw(ref cellPrefab);

            m_pathfinder = new PathfinderAStar();
            
            // Place the camera above the first cell
            Vector3 pos = m_map.Cells[0, 0].Object.transform.position;
            pos.z += 5;
            mainCam.transform.position = pos;

            CreateUnit(m_map.GetCell(19));
            
            // Test adjacent system
            //m_map.Print();
        }

        // Update is called once per frame
        void Update()
        {
            m_map.UpdateVisuals();
        }

        public bool CreateUnit(Cell cell, string name = "", Game.Entity.EntityType eType = 0, Sprite skin = null, Game.Unit.UnitType uType = 0, int movR = 2, int atkR = 1, int visR = 1)
        {
            Vector3 pos = m_map.GetCellPos(cell);

            GameObject obj = GameObject.Instantiate(unitPrefab, pos, unitPrefab.transform.rotation);
            obj.transform.localScale = new Vector3(m_map.CellSize * 0.8f, m_map.CellSize * 0.8f, 0.1f);

            Unit unit = obj.GetComponent<Unit>();
            if (unit)
            {
                unit.Name = name;
                unit.SetMainType(eType);
                unit.CellId = cell.Id;
                unit.Skin = skin;
                unit.SetUnitType(uType);
                unit.MovRange = movR;
                unit.AtkRange = atkR;
                unit.VisRange = visR;
            }

            cell.Occupant = unit;
            return false;
        }
    }
}
