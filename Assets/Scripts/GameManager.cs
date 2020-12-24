using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
    #region Attributes
        public GameObject mainCam;
        
        [SerializeField, Range(10.0f, 20.0f)]
        private float m_camSpeed = 10.0f;

        private Grid m_map;

        public GameObject cellPrefab;

        public int columns, rows;
        public float padding;
        public float size;

        public Cell selected = null;
    #endregion
    #region Accessors
        public float CamSpeed { get => m_camSpeed; set => m_camSpeed = value; }
    #endregion
        // Start is called before the first frame update
        void Start()
        {
            m_map = new Grid(columns, rows, size, padding);

            m_map.Draw(ref cellPrefab);

            // set the default camera settings
            mainCam.transform.position = new Vector3(-columns / 2 * (size + padding), -rows / 2 * (size + padding), 5);
            Camera cam = mainCam.GetComponent<Camera>();
            cam.orthographicSize = 12;
            
            /* Test adjacent system
            m_map.Print();

            m_map.Cells[0,0].AddAdjacentCell(m_map.Cells[0,1], 10);
            m_map.Cells[0,0].AddAdjacentCell(m_map.Cells[1,0], 20);
            
            m_map.Print();
            */
        }

        // Update is called once per frame
        void Update()
        {
            MoveCam();
            Zoom();

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = mainCam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                selected = m_map.GetCell(pos);
            }
        }

        // Handle the movement of the camera
        void MoveCam()
        {
            Vector3 pos = mainCam.transform.position;
            float frustrumSize = mainCam.GetComponent<Camera>().orthographicSize;

            if (Input.GetKey("up"))
                pos.y = Mathf.Min((pos.y + m_camSpeed * Time.deltaTime), 0);
            else if (Input.GetKey("down"))
                pos.y = Mathf.Max((pos.y - m_camSpeed * Time.deltaTime), -(m_map.Size * m_map.Height));

            if (Input.GetKey("left"))
                pos.x = Mathf.Min((pos.x + m_camSpeed * Time.deltaTime), 0);
            else if (Input.GetKey("right"))
                pos.x = Mathf.Max((pos.x - m_camSpeed * Time.deltaTime), -(m_map.Size * m_map.Width));

            mainCam.transform.position = pos;
        }
        // Handle the zoom of the camera
        void Zoom()
        {
            Camera cam = mainCam.GetComponent<Camera>();
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // closer
                cam.orthographicSize = Mathf.Max(cam.orthographicSize-1, 1);
            if (Input.GetAxis("Mouse ScrollWheel") < 0) // further
                cam.orthographicSize = Mathf.Min(cam.orthographicSize+1, 20);
        }
    }
}
