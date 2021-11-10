using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
    #region Attributes
        [SerializeField]
        private GameManager m_gameManager;

        private string m_sName;

        // Is the player controlled by A.I.
        [SerializeField]
        private bool m_bIsAI;

        // Camera components
        private Camera m_cam = null;
        private AudioListener m_listner = null;

        // Camera move speed
        [SerializeField, Range(10.0f, 20.0f)]
        private float m_camSpeed = 10.0f;
    #endregion // Attributes

    #region Accessors
        public float CamSpeed { get => m_camSpeed; set => m_camSpeed = value; }

        public bool isAI { get => m_bIsAI; set => m_bIsAI = value; }
    #endregion

    #region Functions
        // Start is called before the first frame update
        void Start()
        {
            // set the default camera settings
            //gameObject.transform.position = new Vector3(-columns / 2 * (size + padding), -rows / 2 * (size + padding), 5);

            m_sName = gameObject.name;

            m_cam = gameObject.GetComponent<Camera>();
            m_cam.orthographicSize = 5;
            m_cam.enabled = false;
            m_listner = gameObject.GetComponent<AudioListener>();
            m_listner.enabled = false;
        }

        // Update is called once per frame
        public void PlayerUpdate()
        {
            CameraInputs();

            // ControlInputs();
        }

        // Enable/Disable Player's Camera
        public void ChangeCamState()
        {
            if (m_cam)
                m_cam.enabled = !m_cam.enabled;
            if (m_listner)
                m_listner.enabled = !m_listner.enabled;
        }


        // Handle start of the player's turn
        public void StartTurn()
        {
            Debug.Log("Beginning of " + m_sName + "'s turn.");

            ChangeCamState();
            m_gameManager.mainCam = m_cam;
        }
        // Handle end of the player's turn
        public void EndTurn()
        {
            ChangeCamState();

            Debug.Log("End of " + m_sName + "'s turn.");
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
            float frustrumSize = m_cam.orthographicSize;

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
            if (Input.GetAxis("Mouse ScrollWheel") > 0) // closer
                m_cam.orthographicSize = Mathf.Max(m_cam.orthographicSize-1, 4);
            if (Input.GetAxis("Mouse ScrollWheel") < 0) // further
                m_cam.orthographicSize = Mathf.Min(m_cam.orthographicSize+1, 8);
        }
    #endregion // Functions
    }
}