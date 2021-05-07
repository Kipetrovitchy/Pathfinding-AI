using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
    #region Attributes
        [SerializeField, Range(10.0f, 20.0f)]
        private float m_camSpeed = 10.0f;

        [SerializeField]
        private GameManager m_gameManager;
    #endregion // Attributes

    #region Accessors
        public float CamSpeed { get => m_camSpeed; set => m_camSpeed = value; }
    #endregion

    #region Functions
        // Start is called before the first frame update
        void Start()
        {
            // set the default camera settings
            //gameObject.transform.position = new Vector3(-columns / 2 * (size + padding), -rows / 2 * (size + padding), 5);
            gameObject.GetComponent<Camera>().orthographicSize = 5;
        }

        // Update is called once per frame
        void Update()
        {
            CameraInputs();

            ControlInputs();
        }

        // Handle Camera controls
        void CameraInputs()
        {
            MoveCam();
            Zoom();
        }

        // Handle the movement of the camera
        void MoveCam()
        {
            Vector3 pos = gameObject.transform.position;
            float frustrumSize = gameObject.GetComponent<Camera>().orthographicSize;

            if (Input.GetKey("up"))
                pos.y = Mathf.Min((pos.y + m_camSpeed * Time.deltaTime), 0);
            else if (Input.GetKey("down"))
                pos.y = Mathf.Max((pos.y - m_camSpeed * Time.deltaTime), -(m_gameManager.Map.CellSize * m_gameManager.Map.Height));

            if (Input.GetKey("left"))
                pos.x = Mathf.Min((pos.x + m_camSpeed * Time.deltaTime), 0);
            else if (Input.GetKey("right"))
                pos.x = Mathf.Max((pos.x - m_camSpeed * Time.deltaTime), -(m_gameManager.Map.CellSize * m_gameManager.Map.Width));

            gameObject.transform.position = pos;
        }
        // Handle the zoom of the camera
        void Zoom()
        {
            Camera cam = gameObject.GetComponent<Camera>();
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // closer
                cam.orthographicSize = Mathf.Max(cam.orthographicSize-1, 4);
            if (Input.GetAxis("Mouse ScrollWheel") < 0) // further
                cam.orthographicSize = Mathf.Min(cam.orthographicSize+1, 8);
        }
    
        void ControlInputs()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000, m_gameManager.layer))
                {
                    m_gameManager.selectedEntity = hit.collider.gameObject;
                }
                else
                {
                    Vector3 pos = m_gameManager.mainCam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                    m_gameManager.selectedCell = m_gameManager.Map.GetCell(pos);
                }
            }

            if (m_gameManager.selectedEntity != null && Input.GetMouseButtonDown(1))
            {
                Vector3 pos = m_gameManager.mainCam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                
                Unit unit = m_gameManager.selectedEntity.GetComponent<Unit>();
                if (unit)
                {
                    Cell target = m_gameManager.Map.GetCell(pos);
                    if (unit.MoveTo(target))
                    {
                        m_gameManager.Map.GetCell(unit.CellId).occupant = null;
                        unit.CellId = target.Id;
                    }
                }
            }
        }
    #endregion // Functions
    }
}